using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuDa_DeviceCreate
{
    // 定义进程事件的参数类
    public class ProgresValueEventArgs
        : EventArgs
    {
        public int ProgresValue { set; get; }
        public string TextValue { get; set; }
    }

    // 定义事件使用的委托
    public delegate void ValueChangedEventHandler(object sender, ProgresValueEventArgs e);

    public class LongTimeWork
    {
        public bool CancelWork { get; set; } = false;

        //public Action<DeviceReceive> action = null;

        // 定义一个事件来提示界面工作的进度
        public event ValueChangedEventHandler ValueChanged;

        // 触发事件的方法
        public void OnValueChanged(ProgresValueEventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }
    }

}
