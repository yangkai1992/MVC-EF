using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public sealed class LogHelper
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 错误级别日志方法
        /// </summary>
        /// <param name="message">错误说明</param>
        /// <param name="ex">异常</param>
        public static void Error(string message,Exception ex)
        {
            log.Error(message, ex);
        }

        public static void Error(string message)
        {
            log.Error(message);
        }

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Warn(string message)
        {
            log.Warn(message);
        }
    }
}
