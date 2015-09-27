using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dacha.DataModel.Domain;
using Dacha.DataModel.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
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

        public void WorkWithList<T>(Action<List<T>> work)
        {
            /* Create a session and execute a query: */
            using (ISessionFactory factory = _config.BuildSessionFactory())
            using (ISession session = factory.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                var list = session.Query<T>().ToList();
                work(list);

                tx.Commit();
            }

        }
    }
}
