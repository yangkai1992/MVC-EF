using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class PagingModel
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public string Url { get; set; }

        public int TotalPages
        {
            get 
            {
                int remainder = TotalCount % PageSize;
                if (remainder > 0)
                    return TotalCount / PageSize + 1;
                return TotalCount / PageSize;
            }
        }

        public Dictionary<string, string> QueryParams { get; set; }
    }
}