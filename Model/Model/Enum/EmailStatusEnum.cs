using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum EmailStatusEnum
    {
        /// <summary>
        /// 收件箱
        /// </summary>
        [Description("收件箱")]
        inbox,

        /// <summary>
        /// 发件箱
        /// </summary>
        [Description("发件箱")]
        sent,

        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        drafts,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        junk,

        /// <summary>
        /// 垃圾箱
        /// </summary>
        [Description("垃圾箱")]
        trash
    }
}
