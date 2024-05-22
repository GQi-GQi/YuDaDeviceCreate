using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// MQTT连接参数
    /// </summary>
    public class MQTTConnectParamReceive
    {
        public string ClientId { get; set; }

        public string UserName { get; set; }

        public string Passwd { get; set; }

        public string MQTTHostUrl { get; set; }

        public string Port { get; set; }
    }
}
