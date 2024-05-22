using System;

namespace YuDa_DeviceCreate
{
    /// <summary>
    /// 字典表
    /// </summary>
    public class DictionaryReceive
    {
        /// <summary>
        /// 字典表ID
        /// </summary>
        public Guid DictID { get; set; }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string DictName { get; set; }

        /// <summary>
        /// 枚举标识
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string DictKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>	
        public int Sort { get; set; }

        /// <summary>
        /// 父级节点
        /// </summary>
        public Nullable<Guid> ParentID { get; set; }

        /// <summary>
        /// 是否系统枚举
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DictStatus Status { get; set; } = DictStatus.正常;
    }

    /// <summary>
    /// 字典状态
    /// </summary>
    public enum DictStatus
    {
        无效 = 0,
        正常 = 1
    }
}
