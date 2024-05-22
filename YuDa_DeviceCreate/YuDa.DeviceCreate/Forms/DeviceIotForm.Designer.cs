namespace YuDa_DeviceCreate
{
    partial class DeviceIotForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceIotForm));
            this.cbx_Parity = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbx_StopBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbx_DataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx_BaudRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_DeviceIot = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_Serial = new System.Windows.Forms.ComboBox();
            this.txb_Log = new System.Windows.Forms.TextBox();
            this.cbx_Product = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Clean = new System.Windows.Forms.Button();
            this.btn_PrintSetting = new System.Windows.Forms.Button();
            this.cbx_IotInstance = new System.Windows.Forms.ComboBox();
            this.dictionaryReceiveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txb_BootloaderAddr = new System.Windows.Forms.TextBox();
            this.txb_BootloaderPath = new System.Windows.Forms.TextBox();
            this.txb_PartitionTablePath = new System.Windows.Forms.TextBox();
            this.txb_PartitionTableAddr = new System.Windows.Forms.TextBox();
            this.txb_ProgramPath = new System.Windows.Forms.TextBox();
            this.txb_ProgramAddr = new System.Windows.Forms.TextBox();
            this.tip_Iot = new System.Windows.Forms.ToolTip(this.components);
            this.btn_ESP32WriteFlash = new System.Windows.Forms.Button();
            this.btn_CreateDevice = new System.Windows.Forms.Button();
            this.btn_ATSendDeviceInfo = new System.Windows.Forms.Button();
            this.btn_PrintDeviceInfo = new System.Windows.Forms.Button();
            this.txb_HardwareVersion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txb_PcntInterval = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryReceiveBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbx_Parity
            // 
            this.cbx_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Parity.FormattingEnabled = true;
            this.cbx_Parity.Location = new System.Drawing.Point(93, 187);
            this.cbx_Parity.Name = "cbx_Parity";
            this.cbx_Parity.Size = new System.Drawing.Size(132, 20);
            this.cbx_Parity.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 50;
            this.label6.Text = "奇偶校验:";
            // 
            // cbx_StopBits
            // 
            this.cbx_StopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_StopBits.FormattingEnabled = true;
            this.cbx_StopBits.Location = new System.Drawing.Point(316, 188);
            this.cbx_StopBits.Name = "cbx_StopBits";
            this.cbx_StopBits.Size = new System.Drawing.Size(118, 20);
            this.cbx_StopBits.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "停止位:";
            // 
            // cbx_DataBits
            // 
            this.cbx_DataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_DataBits.FormattingEnabled = true;
            this.cbx_DataBits.Location = new System.Drawing.Point(93, 152);
            this.cbx_DataBits.Name = "cbx_DataBits";
            this.cbx_DataBits.Size = new System.Drawing.Size(132, 20);
            this.cbx_DataBits.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "数据位:";
            // 
            // cbx_BaudRate
            // 
            this.cbx_BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_BaudRate.FormattingEnabled = true;
            this.cbx_BaudRate.Location = new System.Drawing.Point(316, 152);
            this.cbx_BaudRate.Name = "cbx_BaudRate";
            this.cbx_BaudRate.Size = new System.Drawing.Size(118, 20);
            this.cbx_BaudRate.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(259, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 44;
            this.label3.Text = "波特率:";
            // 
            // btn_DeviceIot
            // 
            this.btn_DeviceIot.Location = new System.Drawing.Point(380, 298);
            this.btn_DeviceIot.Name = "btn_DeviceIot";
            this.btn_DeviceIot.Size = new System.Drawing.Size(191, 21);
            this.btn_DeviceIot.TabIndex = 43;
            this.btn_DeviceIot.Text = "烧录并创建设备信息下发（F10）";
            this.btn_DeviceIot.UseVisualStyleBackColor = true;
            this.btn_DeviceIot.Click += new System.EventHandler(this.btn_DeviceIot_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "端口号:";
            // 
            // cbx_Serial
            // 
            this.cbx_Serial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Serial.FormattingEnabled = true;
            this.cbx_Serial.Location = new System.Drawing.Point(93, 117);
            this.cbx_Serial.Name = "cbx_Serial";
            this.cbx_Serial.Size = new System.Drawing.Size(132, 20);
            this.cbx_Serial.TabIndex = 41;
            // 
            // txb_Log
            // 
            this.txb_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_Log.HideSelection = false;
            this.txb_Log.Location = new System.Drawing.Point(5, 329);
            this.txb_Log.Margin = new System.Windows.Forms.Padding(83, 83, 3, 100);
            this.txb_Log.Multiline = true;
            this.txb_Log.Name = "txb_Log";
            this.txb_Log.ReadOnly = true;
            this.txb_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txb_Log.Size = new System.Drawing.Size(571, 200);
            this.txb_Log.TabIndex = 57;
            this.txb_Log.Text = "请选择下发设备串口信息，点击“烧录并创建设备信息下发”按钮或按快捷键“F10”";
            // 
            // cbx_Product
            // 
            this.cbx_Product.DisplayMember = "ProductName";
            this.cbx_Product.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Product.FormattingEnabled = true;
            this.cbx_Product.Location = new System.Drawing.Point(316, 223);
            this.cbx_Product.Name = "cbx_Product";
            this.cbx_Product.Size = new System.Drawing.Size(118, 20);
            this.cbx_Product.TabIndex = 60;
            this.cbx_Product.ValueMember = "ProductKey";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 59;
            this.label1.Text = "设备产品:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Clean
            // 
            this.btn_Clean.Location = new System.Drawing.Point(5, 297);
            this.btn_Clean.Name = "btn_Clean";
            this.btn_Clean.Size = new System.Drawing.Size(87, 23);
            this.btn_Clean.TabIndex = 61;
            this.btn_Clean.Text = "清除打印信息";
            this.btn_Clean.UseVisualStyleBackColor = true;
            this.btn_Clean.Click += new System.EventHandler(this.btn_Clean_Click);
            // 
            // btn_PrintSetting
            // 
            this.btn_PrintSetting.Location = new System.Drawing.Point(450, 117);
            this.btn_PrintSetting.Name = "btn_PrintSetting";
            this.btn_PrintSetting.Size = new System.Drawing.Size(118, 23);
            this.btn_PrintSetting.TabIndex = 62;
            this.btn_PrintSetting.Text = "打印选项设置";
            this.btn_PrintSetting.UseVisualStyleBackColor = true;
            this.btn_PrintSetting.Click += new System.EventHandler(this.btn_PrintSetting_Click);
            // 
            // cbx_IotInstance
            // 
            this.cbx_IotInstance.DataSource = this.dictionaryReceiveBindingSource;
            this.cbx_IotInstance.DisplayMember = "DictKey";
            this.cbx_IotInstance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_IotInstance.FormattingEnabled = true;
            this.cbx_IotInstance.Location = new System.Drawing.Point(93, 223);
            this.cbx_IotInstance.Name = "cbx_IotInstance";
            this.cbx_IotInstance.Size = new System.Drawing.Size(132, 20);
            this.cbx_IotInstance.TabIndex = 64;
            this.cbx_IotInstance.ValueMember = "DictValue";
            this.cbx_IotInstance.SelectedIndexChanged += new System.EventHandler(this.cbx_IotInstance_SelectedIndexChanged);
            // 
            // dictionaryReceiveBindingSource
            // 
            this.dictionaryReceiveBindingSource.DataSource = typeof(YuDa_DeviceCreate.DictionaryReceive);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 63;
            this.label7.Text = "产品实例:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 65;
            this.label8.Text = "bootloader:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 66;
            this.label9.Text = "partition_table:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(52, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 67;
            this.label10.Text = "program:";
            // 
            // txb_BootloaderAddr
            // 
            this.txb_BootloaderAddr.Location = new System.Drawing.Point(113, 9);
            this.txb_BootloaderAddr.Name = "txb_BootloaderAddr";
            this.txb_BootloaderAddr.Size = new System.Drawing.Size(56, 21);
            this.txb_BootloaderAddr.TabIndex = 68;
            this.txb_BootloaderAddr.Text = "0x1000";
            // 
            // txb_BootloaderPath
            // 
            this.txb_BootloaderPath.Location = new System.Drawing.Point(175, 9);
            this.txb_BootloaderPath.Name = "txb_BootloaderPath";
            this.txb_BootloaderPath.ReadOnly = true;
            this.txb_BootloaderPath.Size = new System.Drawing.Size(398, 21);
            this.txb_BootloaderPath.TabIndex = 69;
            this.txb_BootloaderPath.TabStop = false;
            this.tip_Iot.SetToolTip(this.txb_BootloaderPath, "双击选文件");
            this.txb_BootloaderPath.DoubleClick += new System.EventHandler(this.tbx_PathSelect_DoubleClick);
            // 
            // txb_PartitionTablePath
            // 
            this.txb_PartitionTablePath.Location = new System.Drawing.Point(175, 39);
            this.txb_PartitionTablePath.Name = "txb_PartitionTablePath";
            this.txb_PartitionTablePath.ReadOnly = true;
            this.txb_PartitionTablePath.Size = new System.Drawing.Size(398, 21);
            this.txb_PartitionTablePath.TabIndex = 71;
            this.txb_PartitionTablePath.TabStop = false;
            this.tip_Iot.SetToolTip(this.txb_PartitionTablePath, "双击选文件");
            this.txb_PartitionTablePath.DoubleClick += new System.EventHandler(this.tbx_PathSelect_DoubleClick);
            // 
            // txb_PartitionTableAddr
            // 
            this.txb_PartitionTableAddr.Location = new System.Drawing.Point(113, 39);
            this.txb_PartitionTableAddr.Name = "txb_PartitionTableAddr";
            this.txb_PartitionTableAddr.Size = new System.Drawing.Size(56, 21);
            this.txb_PartitionTableAddr.TabIndex = 70;
            this.txb_PartitionTableAddr.Text = "0x8000";
            // 
            // txb_ProgramPath
            // 
            this.txb_ProgramPath.Location = new System.Drawing.Point(175, 70);
            this.txb_ProgramPath.Name = "txb_ProgramPath";
            this.txb_ProgramPath.ReadOnly = true;
            this.txb_ProgramPath.Size = new System.Drawing.Size(398, 21);
            this.txb_ProgramPath.TabIndex = 73;
            this.txb_ProgramPath.TabStop = false;
            this.tip_Iot.SetToolTip(this.txb_ProgramPath, "双击选文件");
            this.txb_ProgramPath.DoubleClick += new System.EventHandler(this.tbx_PathSelect_DoubleClick);
            // 
            // txb_ProgramAddr
            // 
            this.txb_ProgramAddr.Location = new System.Drawing.Point(113, 70);
            this.txb_ProgramAddr.Name = "txb_ProgramAddr";
            this.txb_ProgramAddr.Size = new System.Drawing.Size(56, 21);
            this.txb_ProgramAddr.TabIndex = 72;
            this.txb_ProgramAddr.Text = "0x10000";
            // 
            // btn_ESP32WriteFlash
            // 
            this.btn_ESP32WriteFlash.Location = new System.Drawing.Point(450, 152);
            this.btn_ESP32WriteFlash.Name = "btn_ESP32WriteFlash";
            this.btn_ESP32WriteFlash.Size = new System.Drawing.Size(118, 23);
            this.btn_ESP32WriteFlash.TabIndex = 74;
            this.btn_ESP32WriteFlash.Text = "重新烧录";
            this.btn_ESP32WriteFlash.UseVisualStyleBackColor = true;
            this.btn_ESP32WriteFlash.Click += new System.EventHandler(this.btn_ESP32WriteFlash_Click);
            // 
            // btn_CreateDevice
            // 
            this.btn_CreateDevice.Location = new System.Drawing.Point(450, 187);
            this.btn_CreateDevice.Name = "btn_CreateDevice";
            this.btn_CreateDevice.Size = new System.Drawing.Size(118, 23);
            this.btn_CreateDevice.TabIndex = 75;
            this.btn_CreateDevice.Text = "创建新设备信息";
            this.btn_CreateDevice.UseVisualStyleBackColor = true;
            this.btn_CreateDevice.Click += new System.EventHandler(this.btn_CreateDevice_Click);
            // 
            // btn_ATSendDeviceInfo
            // 
            this.btn_ATSendDeviceInfo.Location = new System.Drawing.Point(450, 223);
            this.btn_ATSendDeviceInfo.Name = "btn_ATSendDeviceInfo";
            this.btn_ATSendDeviceInfo.Size = new System.Drawing.Size(118, 23);
            this.btn_ATSendDeviceInfo.TabIndex = 76;
            this.btn_ATSendDeviceInfo.Text = "指令下发设备信息";
            this.btn_ATSendDeviceInfo.UseVisualStyleBackColor = true;
            this.btn_ATSendDeviceInfo.Click += new System.EventHandler(this.btn_ATSendDeviceInfo_Click);
            // 
            // btn_PrintDeviceInfo
            // 
            this.btn_PrintDeviceInfo.Location = new System.Drawing.Point(450, 257);
            this.btn_PrintDeviceInfo.Name = "btn_PrintDeviceInfo";
            this.btn_PrintDeviceInfo.Size = new System.Drawing.Size(118, 23);
            this.btn_PrintDeviceInfo.TabIndex = 77;
            this.btn_PrintDeviceInfo.Text = "打印设备信息";
            this.btn_PrintDeviceInfo.UseVisualStyleBackColor = true;
            this.btn_PrintDeviceInfo.Click += new System.EventHandler(this.btn_PrintDeviceInfo_Click);
            // 
            // txb_HardwareVersion
            // 
            this.txb_HardwareVersion.Location = new System.Drawing.Point(316, 258);
            this.txb_HardwareVersion.Name = "txb_HardwareVersion";
            this.txb_HardwareVersion.Size = new System.Drawing.Size(118, 21);
            this.txb_HardwareVersion.TabIndex = 79;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(235, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 78;
            this.label11.Text = "硬件版本号:";
            // 
            // txb_PcntInterval
            // 
            this.txb_PcntInterval.Location = new System.Drawing.Point(93, 258);
            this.txb_PcntInterval.Name = "txb_PcntInterval";
            this.txb_PcntInterval.Size = new System.Drawing.Size(96, 21);
            this.txb_PcntInterval.TabIndex = 81;
            this.txb_PcntInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_PcntInterval_KeyPress);
            this.txb_PcntInterval.Leave += new System.EventHandler(this.txb_PcntInterval_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 263);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 80;
            this.label12.Text = "上报时间间隔:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(195, 263);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 82;
            this.label13.Text = "毫秒";
            // 
            // DeviceIotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 530);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txb_PcntInterval);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txb_HardwareVersion);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btn_PrintDeviceInfo);
            this.Controls.Add(this.btn_ATSendDeviceInfo);
            this.Controls.Add(this.btn_CreateDevice);
            this.Controls.Add(this.btn_ESP32WriteFlash);
            this.Controls.Add(this.txb_ProgramPath);
            this.Controls.Add(this.txb_ProgramAddr);
            this.Controls.Add(this.txb_PartitionTablePath);
            this.Controls.Add(this.txb_PartitionTableAddr);
            this.Controls.Add(this.txb_BootloaderPath);
            this.Controls.Add(this.txb_BootloaderAddr);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbx_IotInstance);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_PrintSetting);
            this.Controls.Add(this.btn_Clean);
            this.Controls.Add(this.cbx_Product);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_Log);
            this.Controls.Add(this.cbx_Parity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbx_StopBits);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbx_DataBits);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_BaudRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_DeviceIot);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbx_Serial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "DeviceIotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "烧录/创建设备工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeviceIotForm_FormClosing);
            this.Load += new System.EventHandler(this.DeviceIotForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeviceIotForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryReceiveBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbx_Parity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbx_StopBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbx_DataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx_BaudRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_DeviceIot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_Serial;
        private System.Windows.Forms.TextBox txb_Log;
        private System.Windows.Forms.ComboBox cbx_Product;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Clean;
        private System.Windows.Forms.Button btn_PrintSetting;
        private System.Windows.Forms.ComboBox cbx_IotInstance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource dictionaryReceiveBindingSource;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txb_BootloaderAddr;
        private System.Windows.Forms.TextBox txb_BootloaderPath;
        private System.Windows.Forms.TextBox txb_PartitionTablePath;
        private System.Windows.Forms.TextBox txb_PartitionTableAddr;
        private System.Windows.Forms.TextBox txb_ProgramPath;
        private System.Windows.Forms.TextBox txb_ProgramAddr;
        private System.Windows.Forms.ToolTip tip_Iot;
        private System.Windows.Forms.Button btn_ESP32WriteFlash;
        private System.Windows.Forms.Button btn_CreateDevice;
        private System.Windows.Forms.Button btn_ATSendDeviceInfo;
        private System.Windows.Forms.Button btn_PrintDeviceInfo;
        private System.Windows.Forms.TextBox txb_HardwareVersion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txb_PcntInterval;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}