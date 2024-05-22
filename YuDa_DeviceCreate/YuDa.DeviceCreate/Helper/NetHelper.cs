using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 向远程Url Post/Get数据类
    /// </summary>
    public class NetHelper
    {
        public enum RequestType
        {
            GET,
            POST
        }

        /// <summary>
        /// 设置header信息
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        /// <summary>
        /// 通过代理访问网络
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Request(string url, RequestType type = RequestType.GET, string data = null, string host = null)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url); //建立HttpWebRequest對象
            httpRequest.Timeout = 600000; //定義服務器超時時間  
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
            httpRequest.Accept = "*";

            if (!string.IsNullOrEmpty(host))
            {
                SetHeaderValue(httpRequest.Headers, "Host", host);
            }
            switch (type)
            {
                case RequestType.GET:
                    httpRequest.ContentType = "text/html; charset=UTF-8";
                    httpRequest.Method = "GET";
                    break;
                case RequestType.POST:
                    httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    httpRequest.Method = "POST";

                    // 通过流写入请求数据
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data); // 编码形式按照个人需求来设置
                    httpRequest.ContentLength = bytes.Length;
                    Stream requestStream = httpRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close(); // 不要忘记关闭流 
                    break;
                default:
                    httpRequest.ContentType = "text/html; charset=UTF-8";
                    break;
            }

            StringBuilder content = new StringBuilder(); // 
            try
            {
                HttpWebResponse hwrs = (HttpWebResponse)httpRequest.GetResponse(); //取得回應 
                Stream s = hwrs.GetResponseStream(); //得到回應的流對象 
                StreamReader sr = new StreamReader(s, Encoding.UTF8); //以UTF-8編碼讀取流 

                while (sr.Peek() != -1) //每次讀取一行,直到 
                { //下一個字節沒有內容 
                    content.Append(sr.ReadLine()); //返回為止 
                } //  
                s.Dispose();
                sr.Close();
                hwrs.Close();

                System.Threading.Thread.Sleep(10);
            }
            catch (Exception)
            {

            }

            return content.ToString(); //返回得到的字符串 
        }

        public static string HttpPostJson(string url, string data = null, string host = null)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url); //建立HttpWebRequest對象
            httpRequest.Timeout = 600000; //定義服務器超時時間  
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
            httpRequest.Accept = "*";

            if (!string.IsNullOrEmpty(host))
            {
                SetHeaderValue(httpRequest.Headers, "Host", host);
            }
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";

            // 通过流写入请求数据
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data); // 编码形式按照个人需求来设置
            httpRequest.ContentLength = bytes.Length;
            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close(); // 不要忘记关闭流 

            StringBuilder content = new StringBuilder(); // 
            try
            {
                HttpWebResponse hwrs = (HttpWebResponse)httpRequest.GetResponse(); //取得回應 
                Stream s = hwrs.GetResponseStream(); //得到回應的流對象 
                StreamReader sr = new StreamReader(s, Encoding.UTF8); //以UTF-8編碼讀取流 

                while (sr.Peek() != -1) //每次讀取一行,直到 
                { //下一個字節沒有內容 
                    content.Append(sr.ReadLine()); //返回為止 
                } //  
                s.Dispose();
                sr.Close();
                hwrs.Close();

                System.Threading.Thread.Sleep(10);
            }
            catch (Exception)
            {

            }

            return content.ToString(); //返回得到的字符串 
        }

        /// <summary>
        /// 获取网址域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetDomain(string url)
        {
            Regex reg = new Regex(@"(?is)((http|https)://[^/]+)/");
            var m = reg.Match(url);
            if (m.Success)
            {
                return m.Groups[0].Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取网址域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRelativeUrl(string url)
        {
            Regex reg = new Regex(@"(?is)(http|https)://[^/]+(/.*)");
            var m = reg.Match(url);
            if (m.Success)
            {
                return m.Groups[2].Value;
            }
            return url;
        }


        /// <summary>  
        /// 向Url发送post请求  
        /// </summary>  
        /// <param name="postData">发送数据</param>  
        /// <param name="uriStr">接受数据的Url</param>  
        /// <returns>返回网站响应请求的回复</returns>  
        public static string HttpPost(string uriStr, string postData, ContentType contenttype)
        {
            return GetPageContent(uriStr, postData, contenttype);
        }

        /// <summary>  
        /// 向Url发送post请求  
        /// </summary>  
        /// <param name="postData">发送数据</param>  
        /// <param name="uriStr">接受数据的Url</param>  
        /// <returns>返回网站响应请求的回复</returns>  
        public static string HttpPost(string uriStr, string postData, ContentType contenttype, string accountToken, string clientVersion)
        {
            return GetPageContent(uriStr, postData, contenttype, 0, "", 0, accountToken, clientVersion);
        }

        #region post请求转Object
        /// <summary>  
        /// 向Url发送post请求  
        /// </summary>  
        /// <param name="postData">发送数据</param>  
        /// <param name="uriStr">接受数据的Url</param>  
        /// <returns>返回网站响应请求的回复</returns>  
        public static T HttpPostToObject<T>(string uriStr, string postData, ContentType contenttype)
        {
            string resultStr = GetPageContent(uriStr, postData, contenttype);
            try
            {
                return resultStr.JsonToObject<T>();
            }
            catch
            {
                throw new Exception("json to object error: " + resultStr);
            }
        }

        /// <summary>  
        /// 向Url发送post请求  
        /// </summary>  
        /// <param name="postData">发送数据</param>  
        /// <param name="uriStr">接受数据的Url</param>  
        /// <returns>返回对应实体类</returns>  
        public static T HttpPostToObject<T>(string uriStr, string postData, ContentType contenttype, string accountToken, string clientVersion)
        {
            string resultStr = GetPageContent(uriStr, postData, contenttype, 0, "", 0, accountToken, clientVersion);
            try
            {
                return resultStr.JsonToObject<T>();
            }
            catch
            {
                throw new Exception("json to object error: " + resultStr);
            }
        }

        #endregion

        /// <summary>  
        /// 向Url发送post请求  
        /// </summary>  
        /// <param name="postData">发送数据</param>  
        /// <param name="uriStr">接受数据的Url</param>  
        /// <returns>返回网站响应请求的回复</returns>  
        public static string HttpPost(string uriStr, NameValueCollection postData, ContentType contenttype, int errors = 0, string host = "", int timeout = 0)
        {
            StringBuilder tempData = new StringBuilder();
            if (postData != null)
            {
                foreach (string key in postData.AllKeys)
                {
                    tempData.AppendFormat("{0}={1}&", key, postData.GetValues(key).GetValue(0));
                }
                tempData = tempData.Remove(tempData.Length - 1, 1);
            }
            return GetPageContent(uriStr, tempData.ToString(), contenttype, errors, host, timeout);
        }



        public enum ContentType : int
        {
            application_json = 1,
            application_x_www_form_urlencoded = 2
        }

        private static string GetPageContent(string uriStr, string tempData, ContentType contenttype, int errors = 0, string host = "", int timeout = 0, string accountToken = "", string clientVersion = "")
        {
            //超时提醒
            try
            {
                HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(uriStr);
                byte[] data = Encoding.UTF8.GetBytes(tempData);

                requestScore.Method = "POST";
                requestScore.KeepAlive = false;
                requestScore.AllowAutoRedirect = true;
                switch (contenttype)
                {
                    case ContentType.application_json:
                        requestScore.ContentType = "application/json";
                        break;
                    case ContentType.application_x_www_form_urlencoded:
                        requestScore.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                        break;
                }

                requestScore.UserAgent = "Ocean/NET-SDKClient";
                requestScore.ContentLength = data.Length;

                if (!string.IsNullOrEmpty(accountToken))
                {
                    requestScore.Headers.Add("Account-Token", accountToken);
                }

                if (!string.IsNullOrEmpty(clientVersion))
                {
                    requestScore.Headers.Add("ClientVersion", clientVersion);
                }

                if (timeout <= 0)
                    requestScore.KeepAlive = true;
                else
                    requestScore.Timeout = timeout * 1000;

                if (!string.IsNullOrEmpty(host))
                {
                    SetHeaderValue(requestScore.Headers, "Host", host);
                }

                Stream stream = requestScore.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse responseSorce;
                responseSorce = (HttpWebResponse)requestScore.GetResponse();
                StreamReader reader = new StreamReader(responseSorce.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();

                requestScore.Abort();
                responseSorce.Close();
                responseSorce.Close();
                reader.Dispose();
                stream.Dispose();
                return content;
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 发送get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// 发送get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string Url, string postDataStr, string accountToken, string clientVersion, string instanceCode)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Account-Token", accountToken);
                request.Headers.Add("ClientVersion", clientVersion);
                request.Headers.Add("InstanceCode", instanceCode);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #region get请求转Object
        /// <summary>  
        /// 向Url发送get请求  
        /// </summary>  
        /// <param name="postData">发送数据</param>  
        /// <param name="uriStr">接受数据的Url</param>  
        /// <returns>返回网站响应请求的回复</returns>  
        public static T HttpGetToObject<T>(string Url, string postDataStr, string accountToken, string clientVersion, string instanceCode)
        {
            string resultStr = HttpGet(Url, postDataStr, accountToken, clientVersion, instanceCode);
            try
            {
                return resultStr.JsonToObject<T>();
            }
            catch
            {
                throw new Exception("json to object error: " + resultStr);
            }
        }

        #endregion

    }
}
