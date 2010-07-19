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

            //ensure indexes exist
            //EnsureIndexed();
        }

        public const string SESSION_KEY = "sessionscope";

        protected DocumentStore newDocumentStore()
        {
            string path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(MvcApplication)).CodeBase);
            path = Path.Combine(path, "BlogDB").Substring(6);
            var documentStore = new DocumentStore
            {
                Configuration =
                {
                    DataDirectory = path,
                    RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true
                }
            };
            //documentStore.DataDirectory = path;
            //documentStore.Conventions.FindIdentityProperty = q => q.Name == "Id";
            documentStore.Initialize();
            return documentStore;
        }

        public void Application_BeginRequest()
        {
            if (HttpContext.Current.Items.Contains(SESSION_KEY) && HttpContext.Current.Items[SESSION_KEY] != null)
                return;
            IDocumentSession session = newDocumentStore().OpenSession();
            HttpContext.Current.Items.Add(SESSION_KEY, session);
        }

        public void Application_EndRequest()
        {
            try
            {
                if (!HttpContext.Current.Items.Contains(SESSION_KEY) || HttpContext.Current.Items[SESSION_KEY] == null)
                    return;

                IDocumentSession session = (IDocumentSession)HttpContext.Current.Items[SESSION_KEY];
                if (session != null)
                {
                    session.SaveChanges();
                    session.Dispose();
                }
                session = null;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Warn("Error", "EndRequest: " + ex.Message, ex);
            }
        }

        public void EnsureIndexed()
        {
            DocumentStore documentStore = newDocumentStore();
            if (!IndexExists(documentStore, "PostsByPublishedDate"))
            {
                documentStore.DatabaseCommands.PutIndex("PostsByPublishedDate",
                    new IndexDefinition<BlogRavenDB.Models.Post, BlogRavenDB.Models.Post>()
                    {
                        Map = docs => from doc in docs
                            select new
                            {
                                Published = doc.Published
                            },
                        Stores = { { x => x.Published, FieldStorage.Yes } }
                    });
            }
            documentStore.Dispose();
            documentStore = null;
        }

        private bool IndexExists(DocumentStore documentStore, string index)
        {
            try
            {
                if (documentStore.DatabaseCommands.GetIndex(index) != null)
                    return true;
            }
            catch { }
            return false;
        }
    }
}