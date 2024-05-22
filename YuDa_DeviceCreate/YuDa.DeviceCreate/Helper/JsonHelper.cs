using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// json帮助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 通过 Key 获取 Value
        /// </summary>
        /// <returns></returns>
        public static string GetValueByKey(this string json, string key)
        {
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(json);

                return jo[key].ToString();
            }
            catch
            {
                throw new Exception(json);
            }
        }

        /// <summary>
        /// DataRow转Json
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <returns>Json格式对象</returns>
        public static object DataRowToJson(this DataRow row)
        {
            Dictionary<string, object> dataList = new Dictionary<string, object>();
            foreach (DataColumn column in row.Table.Columns)
            {
                dataList.Add(column.ColumnName, row[column]);
            }

            return ObjectToJson(dataList);
        }

        /// <summary>
        /// DataRow转对象，泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="row">DataRow</param>
        /// <returns>Json格式对象</returns>
        public static T DataRowToObject<T>(this DataRow row)
        {
            return JsonToObject<T>(DataRowToJson(row).ToString());
        }

        /// <summary>
        /// DataRow转对象，泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>Json格式对象</returns>
        public static List<T> DataTableToList<T>(this DataTable table)
        {
            return JsonToList<T>(DataTableToJson(table).ToString());
        }
        /// <summary>
        /// DataRow转对象，泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonText">Json文本</param> 
        /// <returns>Json格式对象</returns>
        public static List<T> JsonToList<T>(this string jsonText)
        {
            return JsonToObject<List<T>>(jsonText);
        }

        /// <summary> 
        /// 对象转Json 
        /// </summary> 
        /// <param name="obj">对象</param> 
        /// <returns>Json格式的字符串</returns> 
        public static string ObjectToJson(this object obj)
        {
            try
            {
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsonSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss" });

                // 设置为驼峰命名
                jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                return JsonConvert.SerializeObject(obj, jsonSettings).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.ObjectToJson(): " + ex.Message);
            }
        }
        /// <summary> 
        /// 数据表转Json 
        /// </summary> 
        /// <param name="dataTable">数据表</param> 
        /// <returns>Json字符串</returns> 
        public static object DataTableToJson(this DataTable dataTable)
        {
            return ObjectToJson(dataTable);
        }

        /// <summary> 
        /// Json文本转对象,泛型方法 
        /// </summary> 
        /// <typeparam name="T">类型</typeparam> 
        /// <param name="jsonText">Json文本</param> 
        /// <returns>指定类型的对象</returns> 
        public static T JsonToObject<T>(this string jsonText)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonText.Replace("undefined", "null"));
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.JsonToObject(): " + ex.Message);
            }
        }

        /// <summary> 
        /// Json文本转对象 
        /// </summary> 
        /// <param name="jsonText">Json文本</param> 
        /// <param name="type">类型</param>
        /// <returns>指定类型的对象</returns> 
        public static object JsonToObject(this string jsonText, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonText.Replace("undefined", "null"), type);
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.JsonToObject(): " + ex.Message);
            }
        }


        /// <summary>
        /// [{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]
        /// </summary> 
        /// <param name="strJson">Json字符串</param> 
        /// <returns>DataTable</returns>
        public static DataTable JsonToDataTable(this string strJson)
        {
            return JsonConvert.DeserializeObject(strJson, typeof(DataTable)) as DataTable;
        }


        /// <summary>
        /// 没有Key的 Json 转 数组List
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static List<JToken> JsonToArrayList(this string strJson)
        {
            return ((JArray)JsonConvert.DeserializeObject(strJson)).ToList();
        }


    }
}
