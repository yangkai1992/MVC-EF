using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model;

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

        [HttpPost]
        public JsonResult Register(Model.User user)
        {
            user.CreateTime = DateTime.Now;
            if(!ModelState.IsValid)
            {
                JsonResultModel result = Common.GetErrorMessage(ModelState);
                return Json(result);
            }
            var oldUser=_userService.Find(user.Account);
            if (oldUser != null)
            {
                return Json(new JsonResultModel { State = 0, Message = "账号已经存在" });
            }

            _userService.CreateUser(user);

            var ResultJson = new { State = 1 };

            return Json(ResultJson);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginHistory model)
        {
            JsonResultModel result;

            if (!ModelState.IsValid)
            {
                result = Common.GetErrorMessage(ModelState);
                return Json(result);
            }

            var user = _userService.Find(model.Account);
            if (user == null)
            {
                result = new JsonResultModel()
                {
                    State = 0,
                    Message = "账号不存在"
                };
                return Json(result);
            }

            if (user.Password != EncryptHelper.GetMD5(model.Password))
            {
                result = new JsonResultModel()
                {
                    State = 0,
                    Message = "密码不正确"
                };
                return Json(result);
            }
            else
            {
                string token = Guid.NewGuid().ToString();
                HttpCookie cookie = new HttpCookie("token", token);
                Response.Cookies.Add(cookie);
                Session[token] = user;

                result = new JsonResultModel()
                {
                    State = 1,
                    Message = ""
                };
                return Json(result);

            }
        }
    }
}