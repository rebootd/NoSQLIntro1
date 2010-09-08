using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.Reflection;
using Raven.Database;
using Raven.Client;
using Raven.Client.Document;
using Raven.Database.Indexing;

namespace BlogRavenDB
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static DocumentStore _documentStore;
        private const string RavenSessionKey = "Raven.Session";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ShowTag", // Route name
                "tags/show/{name}", // URL with parameters
                new { controller = "Tags", action = "Show" }
            );

            routes.MapRoute(
                "ShowHashedPost", // Route name
                "{year}/{month}/{hash}", // URL with parameters
                new { controller = "Home", action = "ShowHashed" }
            );

            // urls with raven's id, which include /
            routes.MapRoute(
                "WithParam",                                              // Route name
                "{controller}/{action}/{*id}"                         // URL with parameters
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        public static IDocumentSession CurrentSession
        {
            get { return (IDocumentSession)HttpContext.Current.Items[RavenSessionKey]; }
        }

        public MvcApplication()
        {
            BeginRequest += (sender, args) => HttpContext.Current.Items[RavenSessionKey] = _documentStore.OpenSession();
            EndRequest += (o, eventArgs) =>
            {
                var disposable = HttpContext.Current.Items[RavenSessionKey] as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            };
        }

        protected void Application_Start()
        {
            _documentStore = new DocumentStore { Url = "http://localhost:8080/" };
            _documentStore.Initialize();

            //create indexes
            Raven.Client.Indexes.IndexCreation.CreateIndexes(typeof(BlogRavenDB.Indexes.PostsByPublished).Assembly, _documentStore);

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}