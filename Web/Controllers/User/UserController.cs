using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.User
{
    public class UserController : AuthorizationController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}