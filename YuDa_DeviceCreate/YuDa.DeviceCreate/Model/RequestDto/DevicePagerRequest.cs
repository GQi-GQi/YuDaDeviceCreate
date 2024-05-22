using System;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 设备分页请求类
    /// </summary>
    public class DevicePagerRequest : PageRequest
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// 产品Key
        /// </summary>
        public string ProductKey { get; set; }

        /// <summary>
        /// 设备服务号（递增）
        public string ServiceNo { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
