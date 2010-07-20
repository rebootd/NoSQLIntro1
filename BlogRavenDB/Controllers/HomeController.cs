using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogRavenDB.Models;

namespace BlogRavenDB.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            var postsQuery = DocumentSession.Query<Post>("PostsByPublished")
                .Where(p => p.Published > DateTime.Now.AddDays(-7));

            Post[] posts = { };
            if(postsQuery != null)
            {
                posts = postsQuery.ToArray();
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
