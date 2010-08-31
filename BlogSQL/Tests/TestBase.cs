using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors.SqlServer;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Generators;
using FluentMigrator.Runner.Announcers;
using FluentMigrator;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace BlogSQL.Tests
{
	//testing git branching
    public class TestBase
    {
        private ISession _session = null; //CreateSessionFactory().OpenSession();
        
        public TestBase()
        {
            _session = CreateSessionFactory().OpenSession();
			EnsureMigration();
        }

        ~TestBase()
        {
            _session.Flush();
            _session.Close();
        }

        protected ISession DataSession
        {
            get
            {
                return _session;
            }
        }

		private string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=NoSQLInto1;Integrated Security=True;Pooling=False";
        private ISessionFactory CreateSessionFactory()
        {   
            return Fluently.Configure()
                .Database(
                    FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(c => c.Is(connectionString))
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BlogSQL.Models.Post>())
                .BuildSessionFactory();
        }

		private void EnsureMigration()
		{
			var connection = new System.Data.SqlClient.SqlConnection(connectionString);
			connection.Open();
			var processor = new SqlServerProcessor(connection, new SqlServer2000Generator(),
				new TextWriterAnnouncer(System.Console.Out), new ProcessorOptions());
			var conventions = new MigrationConventions();
			var versionRunner = new FluentMigrator.Runner.MigrationVersionRunner(conventions, processor,
				new MigrationLoader(conventions), new NullAnnouncer());
			//var runner = new MigrationRunner(conventions, processor, new TextWriterAnnouncer(System.Console.Out), new StopWatch());
			//runner.Up(new TestCreateAndDropTableMigration());
			versionRunner.MigrateUp();

			versionRunner = null;
			connection = null;
		}
    }
}