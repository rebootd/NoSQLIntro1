using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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

namespace BlogSQL
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ShowTag", // Route name
                "tags/show/{name}", // URL with parameters
                new { controller = "Tags", action = "Show"} // Parameter defaults
            );

            routes.MapRoute(
                "ShowHashedPost", // Route name
                "{year}/{month}/{hash}", // URL with parameters
                new { controller = "Home", action = "ShowHashed" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );           

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            //ensure migration - schema ...
            //not recommended for general practice because it will affect your application start time
            //instead, either manually execute migration with nant or some other script, or have 
            //the app redirect to a secured admin page to have a user perform the migration
            EnsureMigration();
        }

        public const string SESSION_KEY = "sessionscope";
        public void Application_BeginRequest()
        {
            if (HttpContext.Current.Items.Contains(SESSION_KEY) && HttpContext.Current.Items[SESSION_KEY] != null)
                return;
            ISession session = CreateSessionFactory().OpenSession();
            HttpContext.Current.Items.Add(SESSION_KEY, session);
        }

        public void Application_EndRequest()
        {
            try
            {
                if (!HttpContext.Current.Items.Contains(SESSION_KEY) || HttpContext.Current.Items[SESSION_KEY] == null)
                    return;

                ISession session = (ISession)HttpContext.Current.Items[SESSION_KEY];
                if (session != null)
                {
                    session.Flush();
                    session.Dispose();
                }
                session = null;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Warn("Error", "EndRequest: " + ex.Message, ex);
            }
        }

        private void EnsureMigration()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            var connection = new System.Data.SqlClient.SqlConnection(connectionString);
			connection.Open();
			var processor = new SqlServerProcessor(connection, new SqlServer2000Generator(), 
                new TextWriterAnnouncer(System.Console.Out), new ProcessorOptions());
            var conventions = new MigrationConventions();
            var versionRunner = new FluentMigrator.Runner.MigrationVersionRunner(conventions, processor,
                new MigrationLoader(conventions) , new NullAnnouncer());
            //var runner = new MigrationRunner(conventions, processor, new TextWriterAnnouncer(System.Console.Out), new StopWatch());
            //runner.Up(new TestCreateAndDropTableMigration());
            versionRunner.MigrateUp();

            versionRunner = null;
            connection = null;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            return Fluently.Configure()
                .Database(
                    FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(c => c.Is(connectionString))
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BlogSQL.Models.Post>())
                .BuildSessionFactory();

            //return Fluently.Configure()
            //  .Database(
            //    FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(connectionString)
            //  )
            //  .Mappings(m =>
            //    m.FluentMappings.AddFromAssemblyOf<BlogSQL.Models.Post>())
            //  .BuildSessionFactory();
        }
    }
}