using System;
using System.Text;
using System.Collections.Generic;
using Dacha.DataModel;
using Dacha.DataModel.NHibernate;
using Dacha.DataModel.NHibernate.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace DataModelTest
{
    /// <summary>
    /// Summary description for WorkerTest
    /// </summary>
    [TestClass]
    public class WorkerTest
    {
        public WorkerTest()
        {
            var cfg = new Configuration()
                    .DataBaseIntegration(db =>
                    {
                        db.ConnectionString = "Server=127.0.0.1;Database=troyanda;Uid=postgres;Pwd=qwerty;";
                        db.Dialect<PostgreSQL94Dialect>();
                        db.SchemaAction = SchemaAutoAction.Validate;
                    });

            var types = typeof(Cars).Assembly.GetExportedTypes();
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

                var worker = new WorkerServices(cfg, session);

                var result = worker.SearchForCarNumber(new[] {"45"});
                //session.Save()

                tx.Rollback();
            }
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
