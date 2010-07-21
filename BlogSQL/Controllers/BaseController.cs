using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using BlogSQL.Models;

namespace BlogSQL.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        protected ISession DataSession
        {
            get
            {
                //don't use this verbatim; figure out a better exception for yourself..
                if (!HttpContext.Items.Contains(MvcApplication.SESSION_KEY) || HttpContext.Items[MvcApplication.SESSION_KEY] == null)
                    throw new Exception("No ISession found int HttpContext");
                return HttpContext.Items[MvcApplication.SESSION_KEY] as ISession;
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
