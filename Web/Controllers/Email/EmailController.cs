using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.Email
{
    public class EmailController : AuthorizationController
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }
    }
}