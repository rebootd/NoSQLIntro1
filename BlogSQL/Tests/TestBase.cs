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
    public class TestBase
    {
        private ISession _session = null; //CreateSessionFactory().OpenSession();
        
        public TestBase()
        {
            _session = CreateSessionFactory().OpenSession();
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

        private ISessionFactory CreateSessionFactory()
        {
            string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=NoSQLInto1;Integrated Security=True;Pooling=False";
            return Fluently.Configure()
                .Database(
                    FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(c => c.Is(connectionString))
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BlogSQL.Models.Post>())
                .BuildSessionFactory();
        }
    }
}