using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Norm;

namespace BlogMongoDB
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
		private static Mongo _mongo;
		private const string SessionKey = "Mongo.Session";
		private static string _connectionStringHost = null; 

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "TagShow", // Route name
                "tags/show/{name}", // URL with parameters
                new { controller = "Tags", action = "Show"}
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

		public static string GetConnectionString()
		{
			var authentication = string.Empty;
			var host = string.IsNullOrEmpty(_connectionStringHost) ? "localhost" : _connectionStringHost;
			string database = "NormTests";
			return string.Format("mongodb://{0}{1}/{2}{3}", authentication, host, database, string.Empty);
		}

		public static Mongo CurrentSession
		{
			get { return (Mongo)HttpContext.Current.Items[SessionKey]; }
		}

		public MvcApplication()
		{
			_connectionStringHost = ConfigurationManager.AppSettings["connectionStringHost"];
			BeginRequest += (sender, args) => HttpContext.Current.Items[SessionKey] = Mongo.Create(GetConnectionString());
			EndRequest += (o, eventArgs) =>
			{
				var disposable = HttpContext.Current.Items[SessionKey] as IDisposable;
				if (disposable != null)
					disposable.Dispose();
			};
		}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}