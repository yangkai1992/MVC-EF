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

            _userService.CreateUser(user);

            return View();
        }
    }
}