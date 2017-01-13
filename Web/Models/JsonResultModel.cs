using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class JsonResultModel
    {
        public int State { get; set; }

        public object Message { get; set; }

        public object Data { get; set; }
    }
}