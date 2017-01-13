using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class Common
    {
        public static JsonResultModel GetErrorMessage(ModelStateDictionary modelState)
        {
            List<string> errorList=new List<string>();
            foreach (ModelState eachItem in modelState.Values)
            {
                foreach (ModelError item in eachItem.Errors)
                {
                    errorList.Add(item.ErrorMessage);
                }
            }

            return new JsonResultModel()
            {
                State = 0,
                Message = errorList
            };
        }
    }
}