using System.Collections.Generic;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 请求返回类
    /// </summary>
    public class PubulicMsgResult<T> 
    {
        public ResultCode Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }
    }

    /// <summary>
    /// 请求返回状态
    /// </summary>
    public enum ResultCode : int
    {
        Success = 0,
        Error = 1,
    }

    /// <summary>
    /// 分页返回类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pager<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 汇总信息
        /// </summary>
        public string DataStr { get; set; }

        /// <summary>
        /// 列表
        /// </summary>
        public List<T> List { get; set; }
    }
}
