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
    }
}