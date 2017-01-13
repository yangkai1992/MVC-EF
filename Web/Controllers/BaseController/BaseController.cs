using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            string message = string.Format("页面未捕获的异常：Controller:{0},Action:{1}", controller, action);

            LogHelper.Error(message, filterContext.Exception);

            if (WebHelper.IsAjax())
            {
                ViewResult viewResult = new ViewResult()
                {
                    ViewName = "Error"
                };

                viewResult.TempData.Add("Title", "系统内部异常");
                filterContext.Result = viewResult;
            }
            else
            {
                JsonResultModel result = new JsonResultModel()
                {
                    State = 0,
                    Message = "系统内部异常"
                };

                filterContext.Result = base.Json(result);                
            }

            filterContext.HttpContext.Response.StatusCode = 200;
            filterContext.ExceptionHandled = true;
        }
    }
}