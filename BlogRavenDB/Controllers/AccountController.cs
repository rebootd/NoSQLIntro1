using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using BlogRavenDB.Models;

namespace BlogRavenDB.Controllers
{
    [HandleError]
    public class AccountController : BaseController
    {
        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(Author author, string returnUrl)
        {
            if (ConfigurationManager.AppSettings["username"] == author.Username && ConfigurationManager.AppSettings["password"] == author.Password)
            {
                CurrentAuthor = author;
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            LogoffAuthor();
            return RedirectToAction("Index", "Home");
        }

    }
}
