﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace Common
{
    public static class JsonUtls
    {
        /// <summary>
        /// 对象转为json
        /// </summary>
        /// <param name="o">要转换的对象</param>
        /// <returns>json字符串</returns>
        public static string ToJson(object o) 
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(o, convert);
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// DataTable转json
        /// </summary>
        /// <param name="obj">DataTable转json</param>
        /// <returns>Json字符串</returns>
        public static string DataTableToJson(this DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值  
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合  
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值  
            }
            return javaScriptSerializer.Serialize(arrayList);
        }
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T">待反序列化的类型</typeparam>
        /// <param name="jsonString">待反序列化的Json字符串</param>
        /// <returns>反序列化后的对象</returns>
        //public static T FromJson<T>(this string jsonString) where T : class
        //{
        //    T ouput = null;
        //    if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Dictionary<,>))
        //    {
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        ouput = serializer.Deserialize<T>(jsonString);
        //    }
        //    else
        //    {
        //        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        //        {
        //            ouput = (T)ser.ReadObject(ms);
        //        }
        //    }
        //    return ouput;
        //}
    }
}
