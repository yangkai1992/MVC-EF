using Common;
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
            int pageIndex = WebHelper.GetIntQuery("PageIndex");
            if (pageIndex < 1)
                pageIndex = 1;

            List<Model.User> users = new List<Model.User>();

            ViewBag.PageInfo = new PagingModel { PageIndex = pageIndex, PageSize = 10, TotalCount = 150, Url = Url.Content("~/User/Index") };
            return View(users);
        }
    }
}