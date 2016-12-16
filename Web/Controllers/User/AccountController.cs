using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account
        [HttpPost]
        public JsonResult Register(Model.User user)
        {
            user.CreateTime = DateTime.Now;
            if(!ModelState.IsValid)
            {
                JsonResultModel result = Common.GetErrorMessage(ModelState);
                return Json(result);
            }
           
            //_userService.CreateUser(user);

            var ResultJson = new { State = 1 };

            return Json(ResultJson);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public string Find()
        {
            Model.User us = _userService.Find(new Guid("7DD9EF20-EE4B-448A-9221-B800250A3DBB"));
            return us.UserName;
        }
    }
}