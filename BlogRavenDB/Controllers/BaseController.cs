using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Database;
using Raven.Client;

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
    }
}
