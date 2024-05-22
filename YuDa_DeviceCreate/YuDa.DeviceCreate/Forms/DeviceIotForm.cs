using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace YuDa_DeviceCreate
{
    public partial class DeviceIotForm : Form
    {
        private static ILog log = LogManager.GetLogger(typeof(DeviceIotForm));
        private readonly int EchoErrorCount = int.Parse(ConfigurationManager.AppSettings["EchoErrorCount"]); // 回显重发次数
        //private readonly string QRCodeOnLineUrl = ConfigurationManager.AppSettings["QRCodeOnLineUrl"]; // 二维码链接
        int PrintPageCount = 0;

        //定义DetectCom类 刷新串口
        private DetectCom detectCom;

        //定义SerialPort类实例 串口
        SerialPort SpCom = new SerialPort();

        DeviceReceive AddDeviceInfo;

        public DeviceIotForm()
        {
            InitializeComponent();
        }

        #region Load
        /// <summary>
        /// 加载入口函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceIotForm_Load(object sender, EventArgs e)
        {
            var portList = LoadSerialConfig();
            LoadStopBitsParityConfig();

            // 绑定初始化数据
            BindingInitInfo();

            // 自动刷新检测串口列表
            AutoReflashSericalList(portList);

            //设置控件可跨线程访问
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        #endregion

        #region 加载基础数据

        /// <summary>
        /// 加载串口配置
        /// </summary>
        public string[] LoadSerialConfig()
        {
            //读取电脑串口信息
            List<string> PortName = new List<string>();
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                PortName.Add(vPortName);
            }
            this.cbx_Serial.DataSource = PortName;
            return PortName.ToArray();
        }

        /// <summary>
        /// 自动刷新串口列表
        /// </summary>
        private void AutoReflashSericalList(string[] PortName)
        {
            detectCom = new DetectCom(PortName);
            detectCom.DetectComMode = DetectComModeEnum.Thread;    //线程刷新

            detectCom.DetectComInterval = 100; //设置刷新间隔100ms

            detectCom.AddSerialPortCallback = new Action<List<string>>(AutoAddSerialPortCallback);
            detectCom.DelSerialPortCallback = new Action<List<string>>(AutoDelSerialPortCallback);

            detectCom.Open();
        }

        private void AutoAddSerialPortCallback(List<string> portArray)
        {
            LoadSerialConfig();
            this.cbx_Serial.Text = portArray.First();
        }

        private void AutoDelSerialPortCallback(List<string> portArray)
        {
            string portName = this.cbx_Serial.Text;
            LoadSerialConfig();
            if (!portArray.Contains(portName))
            {
                this.cbx_Serial.Text = portName;
            }
        }

        /// <summary>
        /// 加载波特率/数据校验位/停止位/奇偶校验位数据
        /// </summary>
        public void LoadStopBitsParityConfig()
        {
            var DictionaryResult = NetHelper.HttpPostToObject<PubulicMsgResult<List<DictionaryReceive>>>(LoginUserInfo.ApiUrl + "/api/Dict/GetDictList", "", NetHelper.ContentType.application_json, LoginUserInfo.Account_Token, LoginUserInfo.ClientVersion);

            if (DictionaryResult.Code != ResultCode.Success)
            {
                MessageBox.Show(DictionaryResult.Msg);
                return;
            }

            this.cbx_BaudRate.DataSource = DictionaryResult.Data.Where(c => c.DictCode == "BaudRate" && c.ParentID != null).OrderBy(c => c.Sort).Select(c => c.DictValue).ToList();
            this.cbx_DataBits.DataSource = DictionaryResult.Data.Where(c => c.DictCode == "DataBits" && c.ParentID != null).OrderBy(c => c.Sort).Select(c => c.DictValue).ToList();

            this.cbx_IotInstance.DataSource = DictionaryResult.Data.Where(c => c.DictCode == "IotInstanceId" && c.ParentID != null).OrderBy(c => c.Sort).ToList();

            List<StopBits> stopBitsSource = Common.GetEnumList<StopBits>();
            stopBitsSource.Remove(StopBits.None);
            this.cbx_StopBits.DataSource = stopBitsSource;

            List<ParityStrEmum> paritysSource = Common.GetEnumList<ParityStrEmum>();
            this.cbx_Parity.DataSource = paritysSource;
        }

        /// <summary>
        /// 绑定初始化数据
        /// </summary>
        private void BindingInitInfo()
        {
            this.cbx_Serial.Text = Properties.Settings.Default.Serial;
            this.cbx_BaudRate.Text = Properties.Settings.Default.BaudRate;
            this.cbx_DataBits.Text = Properties.Settings.Default.DataBits;
            this.cbx_Parity.Text = Properties.Settings.Default.Parity;
            this.cbx_StopBits.Text = Properties.Settings.Default.StopBits;
            this.cbx_Product.Text = Properties.Settings.Default.ProductName;
            this.txb_BootloaderPath.Text = Properties.Settings.Default.BootloaderPath;
            this.txb_PartitionTablePath.Text = Properties.Settings.Default.PartitionTablePath;
            this.txb_ProgramPath.Text = Properties.Settings.Default.ProgramPath;
            this.txb_BootloaderAddr.Text = Properties.Settings.Default.BootloaderAddr;
            this.txb_PartitionTableAddr.Text = Properties.Settings.Default.PartitionTableAddr;
            this.txb_ProgramAddr.Text = Properties.Settings.Default.ProgramAddr;
            this.txb_HardwareVersion.Text = Properties.Settings.Default.HardwareVersion;
            this.txb_PcntInterval.Text = Properties.Settings.Default.PcntInterval.ToString();

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.IotInstanceID))
                this.cbx_IotInstance.SelectedValue = Properties.Settings.Default.IotInstanceID;
            //Properties.Settings.Default.Save();
        }
        #endregion

        #region button/ComboBox触发事件
        /// <summary>
        /// 清空打印窗口数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Clean_Click(object sender, EventArgs e)
        {
            txb_Log.Text = "";
        }

        /// <summary>
        /// 打印设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintSetting_Click(object sender, EventArgs e)
        {

            PrintSettingForm printSettingForm = new PrintSettingForm();
            printSettingForm.ShowDialog();
        }

        /// <summary>
        /// 产品实例选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_IotInstance_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbx_IotInstance.SelectedValue == null) return;

                var ProductResult = NetHelper.HttpGetToObject<PubulicMsgResult<List<ProductReceive>>>(LoginUserInfo.ApiUrl + "/api/Product/GetProductOnListBox", "iotInstanceID=" + cbx_IotInstance.SelectedValue.ToString(), LoginUserInfo.Account_Token, LoginUserInfo.ClientVersion, "");

                if (ProductResult.Code != ResultCode.Success)
                {
                    MessageBox.Show(ProductResult.Msg);
                    return;
                }
                this.cbx_Product.DataSource = ProductResult.Data;
                this.cbx_Product.Text = Properties.Settings.Default.ProductName;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// bin文件选择路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbx_PathSelect_DoubleClick(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择bin文件";
            fileDialog.Filter = "(*.bin)|*.bin"; //设置要选择的文件的类型
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Text = fileDialog.FileName;//返回文件的完整路径                
            }
        }

        /// <summary>
        /// 创建设备下发数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DeviceIot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cbx_Product.SelectedValue?.ToString()))
            {
                MessageBox.Show("未选择产品信息");
                return;
            }

            Action action = new Action(IotDeviceCreateAction);
            action.BeginInvoke(new AsyncCallback(ControlOpenCallback), action);
        }

        /// <summary>
        /// ESP烧录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ESP32WriteFlash_Click(object sender, EventArgs e)
        {
            try
            {
                Action action = new Action(() =>
                {
                    ControlEnabledClose();
                    ESP32_WriteFlash();
                });
                action.BeginInvoke(new AsyncCallback(ControlOpenCallback), action);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " ESP烧录 失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 创建新设备按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreateDevice_Click(object sender, EventArgs e)
        {
            try
            {
                Action action = new Action(() =>
                {
                    ControlEnabledClose();
                    CreateDevice();
                });
                action.BeginInvoke(new AsyncCallback(ControlOpenCallback), action);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 创建设备 失败：" + ex.Message);
            }
        }

        /// <summary>
        /// AT指令下发按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ATSendDeviceInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Action action = new Action(() =>
                {
                    ControlEnabledClose();
                    ATSendSerialMsg();
                });
                action.BeginInvoke(new AsyncCallback(ControlOpenCallback), action);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " AT指令下发 失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 打印设备信息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintDeviceInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ControlEnabledClose();
                PrintLable();
                ControlOpenCallback(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 打印标签 失败：" + ex.Message);
            }
        }

        private void txb_PcntInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            //输入0-9和Backspace del 有效
            if ((e.KeyChar >= 47 && e.KeyChar <= 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txb_PcntInterval_Leave(object sender, EventArgs e)
        {
            int textValue = 0;
            if (int.TryParse(txb_PcntInterval.Text, out textValue))
            {
                txb_PcntInterval.Text = textValue.ToString();
            }
            else
            {
                txb_PcntInterval.Text = Properties.Settings.Default.PcntInterval.ToString();
            }
        }

        /// <summary>
        /// 控件启用关闭设置
        /// </summary>
        private void ControlEnabledClose()
        {
            detectCom.Close();
            #region 串口配置禁止选择
            this.cbx_Serial.Enabled = false;
            this.cbx_BaudRate.Enabled = false;
            this.cbx_DataBits.Enabled = false;
            this.cbx_Parity.Enabled = false;
            this.cbx_StopBits.Enabled = false;
            this.cbx_Product.Enabled = false;
            this.btn_DeviceIot.Enabled = false;
            this.cbx_IotInstance.Enabled = false;
            this.btn_PrintSetting.Enabled = false;
            this.btn_ESP32WriteFlash.Enabled = false;
            this.btn_CreateDevice.Enabled = false;
            this.btn_ATSendDeviceInfo.Enabled = false;
            this.btn_PrintDeviceInfo.Enabled = false;
            this.txb_HardwareVersion.Enabled = false;
            this.txb_PcntInterval.Enabled = false;
            #endregion
        }

        /// <summary>
        /// 设备创建回调
        /// </summary>
        /// <param name="ar"></param>
        private void ControlOpenCallback(IAsyncResult ar)
        {
            detectCom.Open();
            //关闭端口号
            if (SpCom.IsOpen) SpCom.Close();

            #region 串口配置开启选择
            this.cbx_Serial.Enabled = true;
            this.cbx_BaudRate.Enabled = true;
            this.cbx_DataBits.Enabled = true;
            this.cbx_Parity.Enabled = true;
            this.cbx_StopBits.Enabled = true;
            this.cbx_Product.Enabled = true;
            this.btn_DeviceIot.Enabled = true;
            this.cbx_IotInstance.Enabled = true;
            this.btn_PrintSetting.Enabled = true;
            this.btn_ESP32WriteFlash.Enabled = true;
            this.btn_CreateDevice.Enabled = true;
            this.btn_ATSendDeviceInfo.Enabled = true;
            this.btn_PrintDeviceInfo.Enabled = true;
            this.txb_HardwareVersion.Enabled = true;
            this.txb_PcntInterval.Enabled = true;
            #endregion
        }

        #endregion

        #region 串口操作
        /// <summary>
        /// 串口打开
        /// </summary>
        private void SerialPortOpen()
        {
            //打开串号
            if (!SpCom.IsOpen)
            {
                var Serial = this.cbx_Serial.SelectedValue.ToString();
                var BaudRate = int.Parse(this.cbx_BaudRate.SelectedValue.ToString());
                var DataBits = int.Parse(this.cbx_DataBits.SelectedValue.ToString());
                var StopBits = this.cbx_StopBits.SelectedValue;
                var Parity = this.cbx_Parity.SelectedValue;

                //设置通讯端口号及波特率、数据位、停止位和校验位。
                SpCom.PortName = Serial;
                SpCom.BaudRate = BaudRate;
                SpCom.Parity = (Parity)Parity;
                SpCom.DataBits = DataBits;
                SpCom.StopBits = (StopBits)StopBits;

                SpCom.Open();
            }
        }

        /// <summary>
        /// 往串口写入数据
        /// </summary>
        /// <param name="msgStr"></param>
        private void WriteSerialMsg(string msgStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(msgStr))
                {
                    Thread.Sleep(10);
                    // 把之前发送到串口的数据清掉
                    int count = SpCom.BytesToRead;
                    if (count > 0)
                    {
                        byte[] readBuffer = new byte[count];
                        SpCom.Read(readBuffer, 0, count);
                    }

                    byte[] data = System.Text.Encoding.Default.GetBytes(msgStr);
                    SpCom.Write(data, 0, data.Length);
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 发→◇" + msgStr);
                }
                else
                {
                    throw new Exception("端口已断开！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("串口写入数据失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 串口返回信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="errorCount">重复次数</param>
        /// <returns></returns>
        public string WaitingSendSerialMsg(string msg, int errorCount = 0)
        {
            msg = msg.Replace("\r\n", "");
            msg = msg + "\r\n";

            if (errorCount < 1)
                errorCount = EchoErrorCount;

            WriteSerialMsg(msg);

            DateTime timeoutStart = DateTime.Now;
            string resultStr = "";
            while (true)
            {
                int count = SpCom.BytesToRead;

                if ((DateTime.Now - timeoutStart).TotalMilliseconds > 5000)  // 设置超时时间
                {
                    throw new Exception("连接超时！");
                }

                if (count == 0) continue;

                byte[] readBuffer = new byte[count];
                SpCom.Read(readBuffer, 0, count);

                resultStr += System.Text.Encoding.Default.GetString(readBuffer);
                //AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 收→◆" + resultStr);

                if (resultStr.Contains("\r\n"))
                {
                    string[] strSplit = Regex.Split(resultStr, "\r\n");

                    if (strSplit.Length >= 3)
                    {
                        AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 收→◆" + strSplit[0]);
                        AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 收→◆" + strSplit[1]);

                        if ($"{strSplit[0]}\r\n" == msg)
                        {
                            return strSplit[1];
                        }
                        else
                        {
                            if (errorCount == 0)
                                throw new Exception("下发重试回显失败：" + msg);
                            resultStr = "";
                            // 重发指令
                            WriteSerialMsg(msg);
                            errorCount--;
                        }
                    }
                }
            }
        }
        #endregion

        #region 烧录 创建设备 下发指令
        /// <summary>
        /// 烧录/设备创建/指令下发/打印操作
        /// </summary>
        private void IotDeviceCreateAction()
        {
            try
            {
                AddDeviceInfo = null;

                ControlEnabledClose();

                // 烧录操作
                if (!ESP32_WriteFlash()) return;

                //打开串号
                SerialPortOpen();

                // 创建新设备
                if (!CreateDevice()) return;

                // 下发指令
                if (!ATSendSerialMsg()) return;

                // 打印
                PrintLable();
            }
            catch (Exception ex)
            {
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 创建设备信息下发 失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 发送AT指令
        /// </summary>
        /// <param name="deviceData"></param>
        /// <returns></returns>
        private bool ATSendSerialMsg()
        {
            try
            {
                if (AddDeviceInfo == null)
                {
                    MessageBox.Show("设备信息未创建");
                    return false;
                }

                //打开串号
                SerialPortOpen();

                //获取机器mac地址
                AddDeviceInfo.MAC = GetDeviceMacAddr();

                string errorMsg = null;

                // 初始化上报时间
                errorMsg = WaitingSendSerialMsg("AT+SETPcntInterval=" + this.txb_PcntInterval.Text);
                if (!errorMsg.Contains("OK"))
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发上报时间失败" + errorMsg);
                    return false;
                }

                // 下发硬件版本号
                errorMsg = WaitingSendSerialMsg("AT+SETHardwareVersion=" + this.txb_HardwareVersion.Text);
                if (!errorMsg.Contains("OK"))
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发硬件版本号失败" + errorMsg);
                    return false;
                }
                // 下发三元组
                errorMsg = WaitingSendSerialMsg($"AT+SETProductKey={AddDeviceInfo.ProductKey}");
                if (!errorMsg.Contains("OK"))
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发ProductKey失败" + errorMsg);
                    return false;
                }

                errorMsg = WaitingSendSerialMsg($"AT+SETDeviceName={AddDeviceInfo.DeviceName}");
                if (!errorMsg.Contains("OK"))
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发DeviceName失败" + errorMsg);
                    return false;
                }

                errorMsg = WaitingSendSerialMsg($"AT+SETDeviceSecret={AddDeviceInfo.DeviceSecret}");
                if (!errorMsg.Contains("OK"))
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发DeviceSecret失败" + errorMsg);
                    return false;
                }

                // 下发设备密钥、mqttHostUrl
                var MQTTConnectResult = NetHelper.HttpGetToObject<PubulicMsgResult<MQTTConnectParamReceive>>(LoginUserInfo.ApiUrl + "/api/Device/GetMQTTConnectParam", $"serviceNo={AddDeviceInfo.ServiceNo}", LoginUserInfo.Account_Token, LoginUserInfo.ClientVersion, "");

                if (MQTTConnectResult.Code != ResultCode.Success)
                    throw new Exception(MQTTConnectResult.Msg);

                AddDeviceInfo.MQTTHostUrl = MQTTConnectResult.Data.MQTTHostUrl;
                AddDeviceInfo.Port = MQTTConnectResult.Data.Port;

                errorMsg = WaitingSendSerialMsg($"AT+SETmqttHostUrl={MQTTConnectResult.Data.MQTTHostUrl}");

                if (!errorMsg.Contains("OK"))
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发MQTTHostUrl失败" + errorMsg);
                    return false;
                }
                errorMsg = WaitingSendSerialMsg($"AT+SETmqttPort={MQTTConnectResult.Data.Port}");
                if (!errorMsg.Contains("OK"))
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发MQTTPort失败" + errorMsg);
                    return false;
                }

                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发设备信息任务完成！");

                SaveDeviceInfoCsv();
                SaveDevicePNG();
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 下发任务 失败 " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 创建新设备
        /// </summary>
        private bool CreateDevice()
        {
            try
            {
                //跳转小程序
                int.TryParse(ConfigurationManager.AppSettings["WechatApplet"], out int wechatApplet);
                if (wechatApplet < 1) wechatApplet = 1;

                AddDeviceRequest addDeviceRequest = new AddDeviceRequest()
                {
                    ProductKey = this.cbx_Product.SelectedValue?.ToString(),
                    IotInstanceId = this.cbx_IotInstance.SelectedValue?.ToString(),
                    WechatApplet = wechatApplet
                };

                var deviceResult = NetHelper.HttpPostToObject<PubulicMsgResult<DeviceReceive>>(LoginUserInfo.ApiUrl + "/api/Device/AddDevice", addDeviceRequest.ObjectToJson(), NetHelper.ContentType.application_json, LoginUserInfo.Account_Token, LoginUserInfo.ClientVersion);

                if (deviceResult.Code != ResultCode.Success)
                {
                    MessageBox.Show("设备添加失败：" + deviceResult.Msg);
                    //AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + $" 设备创建失败");
                    throw new Exception(deviceResult.Msg);
                }
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + $" 设备 {deviceResult.Data.IdentificationCode}创建成功");
                AddDeviceInfo = deviceResult.Data;
                //AddDeviceInfo.MAC = macAddress;
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 创建设备 失败：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取设备mac地址
        /// </summary>
        /// <returns></returns>
        private string GetDeviceMacAddr(int errorCount = 0)
        {
            string macAddress = "";
            errorCount = errorCount <= 0 ? EchoErrorCount : errorCount;

            while (string.IsNullOrEmpty(macAddress))
            {
                // 判断设备是否初始化完成
                try
                {
                    Thread.Sleep(3000);
                    errorCount--;
                    // AT指令获取MAC地址
                    macAddress = WaitingSendSerialMsg($"AT+GETMAC", 1);
                }
                catch (Exception ex)
                {
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 获取MAC地址失败，设备正在初始化中...");
                    if (errorCount == 0)
                        throw ex;
                }
            }

            // mac地址格式化
            int startIndex = macAddress.IndexOf("{");
            int endIndex = macAddress.LastIndexOf("}");
            if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
                macAddress = macAddress.Substring(startIndex + 1, endIndex - (startIndex + 1));

            return Common.InsertStr(macAddress.ToUpper(), 2, ":");   // 格式化mac地址
        }

        private bool ESP32_WriteFlash()
        {
            try
            {

                #region 烧录参数判断
                if (string.IsNullOrWhiteSpace(txb_BootloaderAddr.Text) ||
                    string.IsNullOrWhiteSpace(txb_PartitionTableAddr.Text) ||
                    string.IsNullOrWhiteSpace(txb_ProgramAddr.Text))
                {
                    MessageBox.Show("烧录地址不可为空");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txb_BootloaderPath.Text) ||
                    string.IsNullOrWhiteSpace(txb_PartitionTablePath.Text) ||
                    string.IsNullOrWhiteSpace(txb_ProgramPath.Text))
                {
                    MessageBox.Show("烧录文件地址不可为空");
                    return false;
                }
                if (!File.Exists(txb_BootloaderPath.Text) ||
                    !File.Exists(txb_PartitionTablePath.Text) ||
                    !File.Exists(txb_ProgramPath.Text))
                {
                    MessageBox.Show("请检查烧录文件路径是否正确");
                    return false;
                }

                #endregion

                using (Process p = new Process())
                {
                    string arguments = $" --port {cbx_Serial.SelectedValue} --baud 460800 --before default_reset --after hard_reset --chip esp32 write_flash --flash_mode dio --flash_freq 40m --flash_size=detect" +
                        $" {txb_BootloaderAddr.Text} {txb_BootloaderPath.Text}" +
                        $" {txb_PartitionTableAddr.Text} {txb_PartitionTablePath.Text}" +
                        $" {txb_ProgramAddr.Text} {txb_ProgramPath.Text}";

                    p.StartInfo.FileName = Application.StartupPath + "\\ESP32Tool\\esptool.exe";//可执行程序路径
                    p.StartInfo.Arguments = arguments;//参数以空格分隔，如果某个参数为空，可以传入""
                    p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
                    p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                    p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                    p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 开始烧录");
                    p.Start();
                    p.StandardInput.AutoFlush = true;

                    while (!p.StandardOutput.EndOfStream)
                    {
                        AppendLog(p.StandardOutput.ReadLine() + Environment.NewLine);
                    }
                    p.WaitForExit();

                    //p.WaitForExit();
                    ////正常运行结束放回代码为0
                    //if (p.ExitCode != 0)
                    //{
                    //    output = p.StandardError.ReadToEnd();
                    //    output = output.ToString().Replace(System.Environment.NewLine, string.Empty);
                    //    output = output.ToString().Replace("\n", string.Empty);
                    //    //throw new Exception(output.ToString());
                    //    AppendLog(output.ToString());
                    //}
                    //else
                    //{
                    //    output = p.StandardOutput.ReadToEnd();
                    //    AppendLog(output.ToString());
                    //}

                    if (p.ExitCode != 0)
                    {
                        MessageBox.Show("烧录失败！请重试");
                        return false;
                    }

                    AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 烧录完成 请按RESET按键重启设备");
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " ESP烧录 失败：" + ex.Message);
                return false;
            }
        }
        #endregion

        #region 打印标签
        /// <summary>
        /// 打印标签
        /// </summary>
        private void PrintLable()
        {
            if (!Properties.Settings.Default.OpenPrintTemplate1 && !Properties.Settings.Default.OpenPrintTemplate2)
            {
                AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 未选择打印模板");
                return;
            }

            if (AddDeviceInfo == null)
            {
                MessageBox.Show("设备信息未创建");
                return;
            }

            if (string.IsNullOrEmpty(AddDeviceInfo.MAC))
            {
                SerialPortOpen();
                // 获取设备mac地址
                AddDeviceInfo.MAC = GetDeviceMacAddr();
            }

            AppendLog(DateTime.Now.ToString("HH:mm:ss ffff") + " 执行打印标签操作");


            #region 打印模板一
            if (Properties.Settings.Default.OpenPrintTemplate1)
            {
                //实例化打印对象
                PrintDocument printDocument = new PrintDocument();
                //printDocument.DefaultPageSettings.Landscape = true; // 横向打印
                //设置打印用的纸张,当设置为Custom的时候，可以自定义纸张的大小
                printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom Size 1", (int)(0.393700787 * 100 * 6), (int)(0.393700787 * 100 * 4));
                if (!string.IsNullOrEmpty(Properties.Settings.Default.Printer))
                    printDocument.PrinterSettings.PrinterName = Properties.Settings.Default.Printer;
                else
                    printDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters.Count > 0 ? PrinterSettings.InstalledPrinters[1] : "";
                printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                printDocument.OriginAtMargins = false;

                //注册PrintPage事件，打印每一页时会触发该事件
                printDocument.PrintPage += new PrintPageEventHandler(this.PrintTemplate1_PrintPage);


                ////初始化打印预览对话框对象
                //PrintPreviewDialog printpreviewdialog1 = new PrintPreviewDialog();
                //printpreviewdialog1.Document = printDocument;
                ////打开打印预览对话框
                //DialogResult result = printpreviewdialog1.ShowDialog();
                //if (result == DialogResult.OK)
                printDocument.Print();//开始打印

            }
            #endregion

            #region 打印模板二
            if (Properties.Settings.Default.OpenPrintTemplate2)
            {
                //实例化打印对象
                PrintDocument printDocument = new PrintDocument();
                //printDocument.DefaultPageSettings.Landscape = true; // 横向打印
                //设置打印用的纸张,当设置为Custom的时候，可以自定义纸张的大小
                printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom Size 1", (int)(0.393700787 * 100 * 6), (int)(0.393700787 * 100 * 4));
                if (!string.IsNullOrEmpty(Properties.Settings.Default.Printer))
                    printDocument.PrinterSettings.PrinterName = Properties.Settings.Default.Printer;
                else
                    printDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters.Count > 0 ? PrinterSettings.InstalledPrinters[1] : "";
                printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                printDocument.OriginAtMargins = false;

                //注册PrintPage事件，打印每一页时会触发该事件
                printDocument.PrintPage += new PrintPageEventHandler(this.PrintTemplate2_PrintPage);

                ////初始化打印预览对话框对象
                //PrintPreviewDialog printpreviewdialog1 = new PrintPreviewDialog();
                //printpreviewdialog1.Document = printDocument;
                ////打开打印预览对话框
                //DialogResult result = printpreviewdialog1.ShowDialog();
                //if (result == DialogResult.OK)
                printDocument.Print();//开始打印

            }
            #endregion
        }

        /// <summary>
        /// 模板一
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintTemplate1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                string QRCodeOnLineUrl = Properties.Settings.Default.Template1QRCodeUrl;
                PrintDocument printDocument = sender as PrintDocument;
                List<PrintItemSetting> printItems = Properties.Settings.Default.PrintItemJson.JsonToObject<List<PrintItemSetting>>();

                Font titleFontStyle = new Font("Microsoft YaHei UI", 10f, FontStyle.Regular);//设置字体
                Font fontStyle = new Font("Microsoft YaHei UI", 6f, FontStyle.Regular);//设置字体
                Font deviceFontStyle = new Font("Microsoft YaHei UI", 6.3f, FontStyle.Bold);//设置字体
                Font bottomFontStyle = new Font("Microsoft YaHei UI", 5.8f, FontStyle.Regular);//设置字体

                SolidBrush brush = new SolidBrush(Color.Black);//新建一个画刷

                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                Bitmap bmpLogo = Properties.Resources.logoPrint;
                e.Graphics.DrawImage(bmpLogo, new Rectangle(new Point(10, 2), new Size(25, 16)));

                Bitmap bmpFontLogo = Properties.Resources.fontLogoPrint;
                e.Graphics.DrawImage(bmpFontLogo, new Rectangle(new Point(40, 2), new Size(45, 16)));

                Bitmap bmpLineLogo = Properties.Resources.logoLinePrint;
                e.Graphics.DrawImage(bmpLineLogo, new Rectangle(new Point(0, 21), new Size(236, 12)));
                // 二维码
                Bitmap QRCodeBitmap = CodeHelper.CreateQRCodeBitmap(QRCodeOnLineUrl + AddDeviceInfo.IdentificationCode, 1200, 1200, true);
                e.Graphics.DrawImage(QRCodeBitmap, new Rectangle(new Point(130, 35), new Size(85, 85)));

                //e.Graphics.DrawString("云盒", titleFontStyle, brush, 180, 3);

                float y = 45;
                foreach (var printItem in printItems)
                {
                    if (!printItem.CanDel)
                    {
                        if (printItem.Item.Contains("{机器读取}"))
                            printItem.Item = printItem.Item.Replace("{机器读取}", AddDeviceInfo.MAC);
                        if (printItem.Item.Contains("{硬件版本号}"))
                            printItem.Item = printItem.Item.Replace("{硬件版本号}", this.txb_HardwareVersion.Text);
                    }
                    e.Graphics.DrawString(printItem.Item, fontStyle, brush, 5, y);
                    y += 12;
                }

                e.Graphics.DrawLine(new Pen(Color.Black, 1.2F), new PointF(0.0F, 140.0F), new PointF(236F, 140.0F));

                // 生产日期
                string productDate = string.IsNullOrWhiteSpace(Properties.Settings.Default.ProductDate)
                ? DateTime.Now.ToString("yyyy.MM")
                : Properties.Settings.Default.ProductDate;

                e.Graphics.DrawString("设备识别码：" + Common.InsertStr(AddDeviceInfo.IdentificationCode, 3, " "), deviceFontStyle, brush, 107, 125);
                e.Graphics.DrawString("生产日期：" + productDate, bottomFontStyle, brush, 5, 145);
                e.Graphics.DrawString("制造商：广州宇达数字制造科技有限公司", bottomFontStyle, brush, 80, 145);

                if (PrintPageCount < Properties.Settings.Default.PrintTemplate1Count - 1)
                {
                    e.HasMorePages = true;
                    PrintPageCount++;
                }
                else
                {
                    e.HasMorePages = false;
                    PrintPageCount = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印模板一出错：" + ex.Message);
                log.Error(ex);
            }
        }


        /// <summary>
        /// 模板二
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintTemplate2_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                string QRCodeOnLineUrl = Properties.Settings.Default.Template2QRCodeUrl;

                PrintDocument printDocument = sender as PrintDocument;
                List<PrintItemSetting> printItems = Properties.Settings.Default.PrintItemJson.JsonToObject<List<PrintItemSetting>>();

                Font titleFontStyle = new Font("Microsoft YaHei UI", 11f, FontStyle.Regular);//设置字体
                Font bottomFontStyle = new Font("Microsoft YaHei UI", 14f, FontStyle.Bold);//设置字体

                SolidBrush brush = new SolidBrush(Color.Black);//新建一个画刷

                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                //StringFormat sf = new StringFormat();
                //sf.FormatFlags = StringFormatFlags.DirectionVertical;
                // 二维码
                Bitmap QRCodeBitmap = CodeHelper.CreateQRCodeBitmap(QRCodeOnLineUrl + AddDeviceInfo.IdentificationCode, 1200, 1200, true);
                QRCodeBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                e.Graphics.DrawImage(QRCodeBitmap, new Rectangle(new Point(45, 10), new Size(140, 140)));

                e.Graphics.RotateTransform(270);
                // x+ -> y-   y- -> x-
                e.Graphics.DrawString("数字化平台工位码", titleFontStyle, brush, -145, 15);
                e.Graphics.DrawString(Common.InsertStr(AddDeviceInfo.IdentificationCode, 3, " "), bottomFontStyle, brush, -146, 190);
                e.Graphics.ResetTransform();

                if (PrintPageCount < Properties.Settings.Default.PrintTemplate2Count - 1)
                {
                    e.HasMorePages = true;
                    PrintPageCount++;
                }
                else
                {
                    e.HasMorePages = false;
                    PrintPageCount = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印模板二出错：" + ex.Message);
                log.Error(ex);
            }
        }

        #endregion

        #region 日志操作
        /// <summary>
        /// 输出添加日志
        /// </summary>
        /// <param name="str"></param>
        private void AppendLog(string str)
        {
            txb_Log.AppendText("\r\n" + str?.Replace("\r\n", ""));
            this.txb_Log.Focus();//获取焦点
            this.txb_Log.Select(this.txb_Log.TextLength, 0);
            this.txb_Log.ScrollToCaret();
        }

        #endregion

        #region Order
        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceIotForm_FormClosing(object sender, EventArgs e)
        {
            Properties.Settings.Default.Serial = this.cbx_Serial.Text;
            Properties.Settings.Default.BaudRate = this.cbx_BaudRate.Text;
            Properties.Settings.Default.DataBits = this.cbx_DataBits.Text;
            Properties.Settings.Default.Parity = this.cbx_Parity.Text;
            Properties.Settings.Default.StopBits = this.cbx_StopBits.Text;
            Properties.Settings.Default.ProductName = this.cbx_Product.Text;
            Properties.Settings.Default.IotInstanceID = this.cbx_IotInstance.SelectedValue?.ToString();
            Properties.Settings.Default.BootloaderPath = this.txb_BootloaderPath.Text;
            Properties.Settings.Default.PartitionTablePath = this.txb_PartitionTablePath.Text;
            Properties.Settings.Default.ProgramPath = this.txb_ProgramPath.Text;
            Properties.Settings.Default.BootloaderAddr = this.txb_BootloaderAddr.Text;
            Properties.Settings.Default.PartitionTableAddr = this.txb_PartitionTableAddr.Text;
            Properties.Settings.Default.ProgramAddr = this.txb_ProgramAddr.Text;
            Properties.Settings.Default.HardwareVersion = this.txb_HardwareVersion.Text;
            Properties.Settings.Default.PcntInterval = int.Parse(this.txb_PcntInterval.Text);

            Properties.Settings.Default.Save();
            if (SpCom.IsOpen)
                SpCom.Close();

            System.Environment.Exit(0);
        }

        /// <summary>
        /// 快捷键设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceIotForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                btn_DeviceIot_Click(null, null);
            }
        }

        #endregion


        /// <summary>
        /// 保存设备信息csv
        /// </summary>
        /// <param name="result">写入内容 ----单元格内容，单元格内容-----</param>
        private void SaveDeviceInfoCsv()
        {
            string path = Application.StartupPath + "\\CSV\\";//保存路径
            string fileName = path + "设备信息下发记录.csv";//文件名
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fileName))
            {
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                string[] strArray = new string[] { "设备服务号", "识别码", "上报时间(ms)", "硬件版本号", "产品密钥(ProductKey)", "设备名称(DeviceName)", "设备密钥(DeviceSecret)", "MQTTHostUrl", "Port", "MAC地址", "保存时间" };
                string titleStr = string.Join(",", strArray) + "\t\n";
                sw.Write(titleStr);
                sw.Close();
            }
            StreamWriter swl = new StreamWriter(fileName, true, Encoding.UTF8);
            string deviceStr = AddDeviceInfo.ServiceNo + "," +
                AddDeviceInfo.IdentificationCode + "," +
                this.txb_PcntInterval.Text + "," +
                this.txb_HardwareVersion.Text + "," +
                AddDeviceInfo.ProductKey + "," +
                AddDeviceInfo.DeviceName + "," +
                AddDeviceInfo.DeviceSecret + "," +
                AddDeviceInfo.MQTTHostUrl + "," +
                AddDeviceInfo.Port + "," +
                AddDeviceInfo.MAC + "," +
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                "\t\n";

            swl.Write(deviceStr);
            swl.Close();
        }

        /// <summary>
        /// 保存设备图片信息
        /// </summary>
        private void SaveDevicePNG()
        {
            string QRCodeOnLineUrl = Properties.Settings.Default.Template1QRCodeUrl;
            string path = Application.StartupPath + "\\Picture\\";//保存路径
            string fileName = path + AddDeviceInfo.ServiceNo.ToString().PadLeft(6, '0') + "-" + AddDeviceInfo.IdentificationCode + ".png";//文件名
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(fileName))
            {
                return;
            }

            Bitmap bmp = new Bitmap(300, 340);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // 二维码
            Bitmap QRCodeBitmap = CodeHelper.CreateQRCodeBitmap(QRCodeOnLineUrl + AddDeviceInfo.IdentificationCode, 1200, 1200);
            g.DrawImage(QRCodeBitmap, new Rectangle(new Point(0, 0), new Size(300, 300)));

            SolidBrush brush = new SolidBrush(Color.Black);//新建一个画刷
            Font deviceFontStyle = new Font("Microsoft YaHei UI", 25f, FontStyle.Bold);//设置字体
            g.DrawString(Common.InsertStr(AddDeviceInfo.IdentificationCode, 3, " "), deviceFontStyle, brush, 40, 302);

            bmp.Save(fileName, ImageFormat.Png);
        }

    }
}
