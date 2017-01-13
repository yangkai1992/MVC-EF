using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : AuthorizationController
    {
        // GET: Home
        public ActionResult Index()
        {
            Model.User user = new Model.User();
            return View(user);
        }

        public ActionResult Logout()
        {
            string token = WebHelper.GetCookie("token");
            Session[token] = null;
            return View("../Account/Login");
        }
    }
}