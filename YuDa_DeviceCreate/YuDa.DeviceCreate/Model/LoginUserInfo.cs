using System;
using System.Configuration;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public static class LoginUserInfo
    {
        public static string LoginID;
        public static string UserName;
        public static string CompanyName;
        public static Guid? CompanyID;
        public static string Account_Token;


        public static string ClientVersion = ConfigurationManager.AppSettings["ClientVersion"];
        public static string ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
    }
}
