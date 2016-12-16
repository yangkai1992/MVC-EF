using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class WebHelper
    {
        public static string GetCookie(string name)
        {
            string str;
            HttpCookie item = HttpContext.Current.Request.Cookies[name];
            str = item == null ? string.Empty : item.Value;
            return str;
        }

        public static bool IsAjax()
        {
            return HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
