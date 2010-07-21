using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Database;
using Raven.Client;
using BlogRavenDB.Models;

namespace BlogRavenDB.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        public IDocumentSession DocumentSession
        {
            get
            {
                return MvcApplication.CurrentSession;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return (CurrentAuthor == null);
            }
        }

        public Author CurrentAuthor
        {
            get
            {
                return Session["author"] as Author;
            }
            protected set
            {
                Session["author"] = value;
            }
        }

        protected void LogoffAuthor()
        {
            Session.Clear();
        }
    }
}
