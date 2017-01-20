using Common;
using Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.User
{
    public class UserController : AuthorizationController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            int pageIndex = WebHelper.GetIntQuery("PageIndex");
            if (pageIndex < 1)
                pageIndex = 1;
            int totalCount;

            List<string> wheres = new List<string>();
            wheres.Add(" UserName = @userName");
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@userName", "test2"));

            List<Model.User> users = _userService.PageSearch(pageIndex, 10, out totalCount);

            ViewBag.PageInfo = new PagingModel { PageIndex = pageIndex, PageSize = 10, TotalCount = totalCount, Url = Url.Content("~/User/Index") };
            return View(users);
        }
    }
}