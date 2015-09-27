using System;
using System.Reflection;
using Dacha.DataModel.Domain;
using Dacha.DataModel.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace DataModelTest
{
    [TestClass]
    public class BasicDataTest
    {
        [TestMethod]
        public void TestConnection()
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

            /* Create a session and execute a query: */
            using (ISessionFactory factory = cfg.BuildSessionFactory())
            using (ISession session = factory.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                var car = session.Get<Cars>((long)1);
                //session.Save()

                tx.Commit();
            }
        }
    }
}
