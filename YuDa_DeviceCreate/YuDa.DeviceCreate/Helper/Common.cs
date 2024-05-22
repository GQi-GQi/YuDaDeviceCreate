using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace YuDa_DeviceCreate
{
    public class Common
    {
        const string KEY_64 = "VavicApp";//注意了，是8个字符，64位
        const string IV_64 = "VavicApp";

        /// <summary>
        /// 加密
        /// </summary>
        public string Encode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

        }

        /// <summary>
        /// 解密
        /// </summary>
        public string Decode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 属性长度检查
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="PropertyName"></param>
        /// <param name="MinLength"></param>
        /// <param name="MaxLength"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool StringLengthVerification(string Input, string PropertyName, int MinLength, int MaxLength, out string Message)
        {
            if (Input == null)
            {
                Message = string.Format("{0} 不能为null", PropertyName);
                return false;
            }
            if (Input.Length < MinLength)
            {
                if (Input.Length == 0)
                {
                    Message = string.Format("{0} 不能为空", PropertyName);
                    return false;
                }
                else
                {
                    Message = string.Format("{0} 超出长度范围，允许的最小长度是 {1}，请检查", PropertyName, MinLength);
                    return false;
                }
            }
            if (Input.Length > MaxLength)
            {
                Message = string.Format("{0} 超出长度范围，允许的最大长度是 {1}，请检查", PropertyName, MaxLength);
                return false;
            }

            Message = string.Format("{0} 长度检查通过", PropertyName);
            return true;
        }

        public static bool IsChinaMobilePhone(string Input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Input, @"^1\d{10}$");
        }
        public static bool IsChinaFixedTelephone(string Input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Input, @"^0\d{2,3}-\d{7,8}$");
        }

        /// <summary>
        /// 验证用户输入的是否是整数
        /// </summary>
        public static bool IsInt(decimal num)
        {
            if (num != (int)num)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 对字符串进行MD5加密
        /// 32位，大写
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MD5(string text)
        {
            try
            {
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(MD5.ComputeHash(Encoding.GetEncoding(Encoding.UTF8.BodyName).GetBytes(text))).Replace("-", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool GetIsDecimal(string decMessage)
        {
            bool IsDecimal = false;
            if (Regex.IsMatch(decMessage, @"^(\-|\+)?\d+(\.\d+)?$"))
            {
                IsDecimal = true;
            }
            return IsDecimal;
        }

        public static bool GetIsInt(string decMessage)
        {
            bool IsInt = false;
            if (Regex.IsMatch(decMessage, @"^[0-9]*$"))
            {
                IsInt = true;
            }
            return IsInt;
        }

        /// &lt;summary&gt;
        /// 把字节数组转换为十六进制格式的字符串。
        /// &lt;/summary&gt;
        /// &lt;param name="pByte"&gt;要转换的字节数组。&lt;/param&gt;
        /// &lt;returns&gt;返回十六进制格式的字符串。&lt;/returns&gt;
        public static string getStringFromBytes(byte[] pByte, int length)
        {
            string str = "";     //定义字符串类型临时变量。
            //遍历字节数组，把每个字节转换成十六进制字符串，不足两位前面添“0”，以空格分隔累加到字符串变量里。
            for (int i = 0; i < length; i++)
                str += (pByte[i].ToString("X").PadLeft(2, '0') + " ");
            str = str.TrimEnd(' ');     //去掉字符串末尾的空格。
            return str;     //返回字符串临时变量。
        }

        /// <summary>
        /// 数据封包 要发送的数据长度(4个字节)+原数据
        /// </summary>
        /// <returns></returns>
        public static byte[] PacketData(byte[] data)
        {
            int len = data.Length;

            byte[] headByte = new byte[4];
            //ConvertIntToByteArray(len, ref headByte);
            headByte = System.Text.Encoding.ASCII.GetBytes(len.ToString("0000"));

            byte[] newByte = new byte[headByte.Length + data.Length];
            Array.Copy(headByte, 0, newByte, 0, headByte.Length);
            Array.Copy(data, 0, newByte, headByte.Length, data.Length);

            return newByte;
        }

        /// <summary>
        /// 通过枚举类型获取枚举列表;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> GetEnumList<T>() where T : Enum
        {
            List<T> list = Enum.GetValues(typeof(T)).OfType<T>().ToList();
            return list;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static long GetCurrentMilliseconds(DateTime? current = null)
        {
            DateTime dt1970 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));// new DateTime(1970, 1, 1);
            DateTime time = current == null ? DateTime.Now : (DateTime)current;
            return (long)(time - dt1970).TotalMilliseconds;
        }

        /// <summary>
        /// 隔几个字符插入字符
        /// </summary>
        /// <param name="inString"></param>
        /// <param name="num"></param>
        /// <param name="addString"></param>
        /// <returns></returns>
        public static string InsertStr(string inString, int num, string addString)
        {
            for (int i = num; i < inString.Length; i += num + addString.Length)
                inString = inString.Insert(i, addString);
            return inString;
        }
    }
}
