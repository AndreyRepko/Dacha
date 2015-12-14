using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStructures.DataStructures;
using BasicDataStructures.Interfaces;
using Dacha.DataModel.Domain;
using Dacha.DataModel.NHibernate;
using Dacha.DataModel.NHibernate.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using Npgsql;

namespace Dacha.DataModel
{
    public class Database
    {
        private Configuration _config;

        public Database()
        {
            var cfg = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.ConnectionString = "Server=127.0.0.1;Database=troyanda;Uid=postgres;Pwd=qwerty;";
                    db.Dialect<PostgreSQL94Dialect>();
                    db.SchemaAction = SchemaAutoAction.Validate;
                });

            var types = typeof (Cars).Assembly.GetExportedTypes();
            /* Add the mapping we defined: */
            var mapper = new ModelMapper();
            mapper.AddMappings(types);

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);

            _config = cfg;
            
            if (!ProtocolVersion.Version3.HasFlag(ProtocolVersion.Version3))
                throw new Exception("Error loading Npgsql");

        }

        public delegate void DatabaseWorker<T>(Func<List<T>> refresher, Action<T> saveOrUpdate, Action<T> deleter, IWorkerServices services);

        /// <summary>
        /// NHibernate worker that do all work:
        /// 1. Func<List<T>> - is about getting table content
        /// 2. Action<T> - about 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="worker"></param>
        public void WorkWithList<T>(DatabaseWorker<T> worker)
        {
            /* Create a session and execute a query: */
            using (ISessionFactory factory = _config.BuildSessionFactory())
            using (ISession session = factory.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                Func<List<T>> refresher = () => session.Query<T>().ToList();
                var services = new WorkerServices(_config, session);
                worker(refresher, (t) => session.SaveOrUpdate(t), (t) => session.Delete(t), services);

                tx.Commit();
            }
        }

        public void DoWorkWithServices(Action<IWorkerServices> worker)
        {
            using (ISessionFactory factory = _config.BuildSessionFactory())
            using (ISession session = factory.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                var services = new WorkerServices(_config, session);
                worker(services);

                tx.Commit();
            }
        }
    }

    public class WorkerServices: IWorkerServices
    {
        private readonly Configuration _config;
        private readonly ISession _session;

        public WorkerServices(Configuration config, ISession session)
        {
            _config = config;
            _session = session;
        }

        public List<ItemPresenterViewModel> GetItemsPresenterForEntity(Type entity)
        {
            var tableMaping = _config.ClassMappings.FirstOrDefault(
                pers => pers.ClassName == entity.AssemblyQualifiedName);
            if (tableMaping == null)
                throw new ArgumentException($"No maping for entity: {entity.Name}");
            var id = ((SimpleValue) tableMaping.Identifier).ColumnIterator.Cast<Column>().First().CanonicalName;
            var name = "Name";
            var queryString =
                $"SELECT {id} as id, {name} as name FROM {tableMaping.Table.Name} ORDER BY {name}";
            var query = _session.CreateSQLQuery(queryString);
            var resultObject = query.List();
            var result = new List<ItemPresenterViewModel>();
            foreach (var raw in resultObject)
            {
                var rawAsArray = (object[]) raw;
                result.Add(new ItemPresenterViewModel((long)rawAsArray[0], (string)rawAsArray[1]));
            }
            return result;
        }

        public object GetItemByTypeAndId(Type entity, long? id)
        {
            return _session.Get(entity, id);
        }
    }
}
