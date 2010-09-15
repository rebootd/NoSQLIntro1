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

        public ActionResult TagCloud()
        {
            return View("TagCloud", GetUniqueTags());
        }

        public List<string> GetUniqueTags()
        {
			var alltags = DocumentSession.LuceneQuery<Tag>("TagsByName").WaitForNonStaleResults().ToList();
			List<string> tags = (from t in alltags
								 select t.Name)
								.ToList();
            return tags;
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
