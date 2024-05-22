using System;
using System.ComponentModel;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceReceive
    {
        /// <summary>
        /// 设备服务号（递增）
        /// </summary>
        public int ServiceNo { get; set; }

        /// <summary>
        /// 识别码
        /// </summary>
        public string IdentificationCode { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备密钥
        /// </summary>
        public string DeviceSecret { get; set; }

        /// <summary>
        /// 物联网平台为该设备颁发的设备ID，作为该设备的唯一标识符。
        /// </summary>
        public string IotId { get; set; }

        /// <summary>
        /// 产品密钥
        /// </summary>
        public string ProductKey { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 实例ID
        /// </summary>
        public string IotInstanceId { get; set; }

        /// <summary>
        /// LoRaWAN设备的DevEUI。创建LoRaWAN设备时，该参数必传。
        /// </summary>
        public string DevEui { get; set; }

        /// <summary>
        /// LoRaWAN设备的PIN Code，用于校验DevEUI的合法性。创建LoRaWAN设备时，LoraNodeType为ALIYUNDEFINED，该参数必传。
        /// </summary>
        public string PinCode { get; set; }

        /// <summary>
        /// LoRaWAN设备的入网凭证JoinEui。创建LoRaWAN设备时，LoraNodeType为USERDEFINED，该参数必传。
        /// </summary>
        public string JoinEui { get; set; }

        /// <summary>
        /// LoRaWAN设备的AppKey。创建LoRaWAN设备时，LoraNodeType为USERDEFINED，该参数必传。
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// LoRaWAN设备类型(调接口时需要传入字符串)
        /// </summary>
        public DeviceLoraNodeType LoraNodeType { get; set; }

        /// <summary>
        /// LoRaWAN设备类型
        /// </summary>
        public string LoraNodeTypeStr { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 固件版本
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// 模组串号
        /// </summary>
        public string ModuleNo { get; set; }

        /// <summary>
        /// 流量
        /// </summary>
        public long Flow { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusStr { get; set; }

        /// <summary>
        /// 激活时间（第一次上线的时候更新）
        /// </summary>
        public DateTime? ActivationTime { get; set; }

        /// <summary>
        /// 最后在线时间（接收到下线消息的时候更新）
        /// </summary>
        public DateTime? LastOnLineTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 物理地址
        /// </summary>
        public string MAC { get; set; }

        ///// <summary>
        ///// 是否成功
        ///// </summary>
        //[Newtonsoft.Json.JsonIgnore()]
        //public bool IsATSuccess { get; set; }

        public string MQTTHostUrl { get; set; }

        public string Port { get; set; }

    }

    /// <summary>
    /// 节点类型()
    /// </summary>
    public enum DeviceLoraNodeType
    {
        [Description("阿里云颁发类型")]
        ALIYUNDEFINED = 0,

        [Description("用户自定义类型")]
        USERDEFINED = 1,
    }


    /// <summary>
    /// 设备状态
    /// </summary>
    public class DeviceStatusEnum
    {
        public enum DeviceStatus : int
        {
            [Description("全部")]
            全部 = 0,

            [Description("未激活")]
            未激活 = 1,

            [Description("离线")]
            离线 = 2,

            [Description("在线")]
            在线 = 3,

            [Description("禁用")]
            禁用 = 4,
        }
        public static int GetDeviceStatus(DeviceStatus deviceStatus) => (int)deviceStatus - 1;
    }

    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceGridView
    {
        /// <summary>
        /// 设备服务号（递增）
        /// </summary>
        public int ServiceNo { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusStr { get; set; }
    }
}
