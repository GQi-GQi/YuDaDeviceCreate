using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 添加设备
    /// </summary>
    public class AddDeviceRequest
    {
        /// <summary>
        /// 产品密钥
        /// </summary>
        public string ProductKey { get; set; }

        /// <summary>
        /// 实例ID
        /// </summary>
        public string IotInstanceId { get; set; }

        /// <summary>
        /// 小程序码
        /// </summary>
        public int WechatApplet { get; set; } = 1;
    }
}
