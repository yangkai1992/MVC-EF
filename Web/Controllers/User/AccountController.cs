using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: Account
        public ActionResult Create(Model.User user)
        {
            if(!ModelState.IsValid)
            {
                return View("../Home/Index", user);
            }
            user.CreateTime = DateTime.Now;
            _userService.CreateUser(user);

            return View();
        }

        public string Find()
        {
            Model.User us = _userService.Find(new Guid("7DD9EF20-EE4B-448A-9221-B800250A3DBB"));
            return us.UserName;
        }
    }
}