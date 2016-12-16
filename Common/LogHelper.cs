using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public sealed class LogHelper
    {
        /// <summary>
        /// 错误级别日志方法
        /// </summary>
        /// <param name="message">错误说明</param>
        /// <param name="ex">异常</param>
        public static void Error(string message,Exception ex)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Error(message, ex);
        }

        public static void Error(string message)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Error(message);
        }

        public static void Info(string message)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Info(message);
        }

        public static void Debug(string message)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Debug(message);
        }


        private static string GetCurrentMethodFullName()
        {
            StackFrame frame;
            string str;
            string str1;
            bool flag;
            try
            {
                int num = 2;
                StackTrace stackTrace = new StackTrace();
                int length = stackTrace.GetFrames().Length;
                do
                {
                    int num1 = num;
                    num = num1 + 1;
                    frame = stackTrace.GetFrame(num1);
                    str = frame.GetMethod().DeclaringType.ToString();
                    flag = (!str.EndsWith("Exception") ? false : num < length);
                }
                while (flag);
                string name = frame.GetMethod().Name;
                str1 = string.Concat(str, ".", name);
            }
            catch
            {
                str1 = null;
            }
            return str1;
        }

    }
}
