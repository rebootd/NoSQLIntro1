using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogRavenDB.Models;

namespace BlogRavenDB.Controllers
{
    [HandleError]
    public class ApplicationController : BaseController
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            Post[] posts = DocumentSession.Query<Post>("PostsByPublishedDate")
                            .Where(x => x.Published > DateTime.Now)
                            .ToArray();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
