﻿using Common;
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

            LogHelper.Error(string.Empty, filterContext.Exception);

            base.OnException(filterContext);
        }
    }
}