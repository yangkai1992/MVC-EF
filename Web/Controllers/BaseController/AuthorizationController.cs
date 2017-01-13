using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        protected Model.User _user;

        protected Model.User CurrentUser 
        {
            get 
            {
                if (_user == null)
                {
                    string token = WebHelper.GetCookie("token");
                    _user = Session[token] as Model.User;
                }

                return _user;
            }
        }


        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentUser == null)
            {
                if (!WebHelper.IsAjax())
                {
                    filterContext.Result = base.RedirectToAction("Login", "Account");
                    return;
                }

                JsonResultModel result = new JsonResultModel()
                {
                    State = 0,
                    Message = "登录超时,请重新登录！"
                };

                filterContext.Result = base.Json(result);
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}