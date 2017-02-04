using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum EmailReadStatusEnum
    {
        /// <summary>
        /// 未读
        /// </summary>
        [Description("未读")]
        unread = 0,

        /// <summary>
        /// 已读
        /// </summary>
        [Description("已读")]
        read = 1
    }
}
