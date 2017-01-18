using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EncryptHelper
    {
        /// <summary>
        /// 16位大写MD5加密
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public static string GetMD5(string rawString)
        {
            byte[] rawBytes = UTF8Encoding.Default.GetBytes(rawString);
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] resultBytes = MD5.ComputeHash(rawBytes);
            string result = BitConverter.ToString(resultBytes, 4, 8);
            return result.Replace("-", "");
        }
    }
}
