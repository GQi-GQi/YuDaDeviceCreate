namespace YuDa_DeviceCreate
{
    partial class PrintSettingForm
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
            this.lbl_PrintDeviceName = new System.Windows.Forms.Label();
            this.cbx_PrintDeviceName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_1PageCount = new System.Windows.Forms.ComboBox();
            this.btn_SaveSetting = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_ProductDate = new System.Windows.Forms.TextBox();
            this.lbx_PrintItem = new System.Windows.Forms.ListBox();
            this.btn_PrintItemMoveUp = new System.Windows.Forms.Button();
            this.btn_PrintItemMoveDown = new System.Windows.Forms.Button();
            this.tip_PrintBtnTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_PrintItemAdd = new System.Windows.Forms.Button();
            this.btn_PrintItemDel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_Edit = new System.Windows.Forms.TextBox();
            this.printItemSettingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbx_2PageCount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chx_Template1 = new System.Windows.Forms.CheckBox();
            this.chx_Template2 = new System.Windows.Forms.CheckBox();
            this.tbx_Template1QRCodeUrl = new System.Windows.Forms.TextBox();
            this.tbx_Template2QRCodeUrl = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printItemSettingBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_PrintDeviceName
            // 
            this.lbl_PrintDeviceName.AutoSize = true;
            this.lbl_PrintDeviceName.Location = new System.Drawing.Point(33, 23);
            this.lbl_PrintDeviceName.Name = "lbl_PrintDeviceName";
            this.lbl_PrintDeviceName.Size = new System.Drawing.Size(53, 12);
            this.lbl_PrintDeviceName.TabIndex = 0;
            this.lbl_PrintDeviceName.Text = "打印机：";
            // 
            // cbx_PrintDeviceName
            // 
            this.cbx_PrintDeviceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_PrintDeviceName.FormattingEnabled = true;
            this.cbx_PrintDeviceName.Location = new System.Drawing.Point(87, 20);
            this.cbx_PrintDeviceName.Name = "cbx_PrintDeviceName";
            this.cbx_PrintDeviceName.Size = new System.Drawing.Size(311, 20);
            this.cbx_PrintDeviceName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "打印页数：";
            // 
            // cbx_1PageCount
            // 
            this.cbx_1PageCount.FormattingEnabled = true;
            this.cbx_1PageCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbx_1PageCount.Location = new System.Drawing.Point(230, 51);
            this.cbx_1PageCount.Name = "cbx_1PageCount";
            this.cbx_1PageCount.Size = new System.Drawing.Size(93, 20);
            this.cbx_1PageCount.TabIndex = 3;
            // 
            // btn_SaveSetting
            // 
            this.btn_SaveSetting.Location = new System.Drawing.Point(270, 372);
            this.btn_SaveSetting.Name = "btn_SaveSetting";
            this.btn_SaveSetting.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveSetting.TabIndex = 6;
            this.btn_SaveSetting.Text = "保存设置";
            this.btn_SaveSetting.UseVisualStyleBackColor = true;
            this.btn_SaveSetting.Click += new System.EventHandler(this.btn_SaveSetting_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 377);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "生产日期：";
            // 
            // txb_ProductDate
            // 
            this.txb_ProductDate.Location = new System.Drawing.Point(89, 373);
            this.txb_ProductDate.Name = "txb_ProductDate";
            this.txb_ProductDate.Size = new System.Drawing.Size(130, 21);
            this.txb_ProductDate.TabIndex = 10;
            // 
            // lbx_PrintItem
            // 
            this.lbx_PrintItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbx_PrintItem.DisplayMember = "Item";
            this.lbx_PrintItem.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbx_PrintItem.FormattingEnabled = true;
            this.lbx_PrintItem.Location = new System.Drawing.Point(3, 20);
            this.lbx_PrintItem.Name = "lbx_PrintItem";
            this.lbx_PrintItem.Size = new System.Drawing.Size(318, 145);
            this.lbx_PrintItem.TabIndex = 11;
            this.tip_PrintBtnTip.SetToolTip(this.lbx_PrintItem, "双击编辑，回车保存");
            this.lbx_PrintItem.ValueMember = "Item";
            this.lbx_PrintItem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbx_PrintItem_MouseClick);
            this.lbx_PrintItem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbx_PrintItem_DrawItem);
            this.lbx_PrintItem.DoubleClick += new System.EventHandler(this.lbx_PrintItem_DoubleClick);
            // 
            // btn_PrintItemMoveUp
            // 
            this.btn_PrintItemMoveUp.AutoEllipsis = true;
            this.btn_PrintItemMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_PrintItemMoveUp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_PrintItemMoveUp.Location = new System.Drawing.Point(333, 20);
            this.btn_PrintItemMoveUp.Name = "btn_PrintItemMoveUp";
            this.btn_PrintItemMoveUp.Size = new System.Drawing.Size(25, 25);
            this.btn_PrintItemMoveUp.TabIndex = 12;
            this.btn_PrintItemMoveUp.TabStop = false;
            this.btn_PrintItemMoveUp.Text = "↑";
            this.btn_PrintItemMoveUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tip_PrintBtnTip.SetToolTip(this.btn_PrintItemMoveUp, "上移");
            this.btn_PrintItemMoveUp.UseVisualStyleBackColor = true;
            this.btn_PrintItemMoveUp.Click += new System.EventHandler(this.btn_PrintItemMoveUp_Click);
            // 
            // btn_PrintItemMoveDown
            // 
            this.btn_PrintItemMoveDown.AutoEllipsis = true;
            this.btn_PrintItemMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_PrintItemMoveDown.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_PrintItemMoveDown.Location = new System.Drawing.Point(333, 47);
            this.btn_PrintItemMoveDown.Name = "btn_PrintItemMoveDown";
            this.btn_PrintItemMoveDown.Size = new System.Drawing.Size(25, 25);
            this.btn_PrintItemMoveDown.TabIndex = 13;
            this.btn_PrintItemMoveDown.TabStop = false;
            this.btn_PrintItemMoveDown.Text = "↓";
            this.btn_PrintItemMoveDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tip_PrintBtnTip.SetToolTip(this.btn_PrintItemMoveDown, "下移");
            this.btn_PrintItemMoveDown.UseVisualStyleBackColor = true;
            this.btn_PrintItemMoveDown.Click += new System.EventHandler(this.btn_PrintItemMoveDown_Click);
            // 
            // btn_PrintItemAdd
            // 
            this.btn_PrintItemAdd.AutoEllipsis = true;
            this.btn_PrintItemAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_PrintItemAdd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_PrintItemAdd.Location = new System.Drawing.Point(333, 73);
            this.btn_PrintItemAdd.Name = "btn_PrintItemAdd";
            this.btn_PrintItemAdd.Size = new System.Drawing.Size(25, 25);
            this.btn_PrintItemAdd.TabIndex = 14;
            this.btn_PrintItemAdd.TabStop = false;
            this.btn_PrintItemAdd.Text = "➕";
            this.btn_PrintItemAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tip_PrintBtnTip.SetToolTip(this.btn_PrintItemAdd, "添加");
            this.btn_PrintItemAdd.UseVisualStyleBackColor = true;
            this.btn_PrintItemAdd.Click += new System.EventHandler(this.btn_PrintItemAdd_Click);
            // 
            // btn_PrintItemDel
            // 
            this.btn_PrintItemDel.AutoEllipsis = true;
            this.btn_PrintItemDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_PrintItemDel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_PrintItemDel.Location = new System.Drawing.Point(333, 101);
            this.btn_PrintItemDel.Name = "btn_PrintItemDel";
            this.btn_PrintItemDel.Size = new System.Drawing.Size(25, 25);
            this.btn_PrintItemDel.TabIndex = 15;
            this.btn_PrintItemDel.TabStop = false;
            this.btn_PrintItemDel.Text = "➖";
            this.btn_PrintItemDel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tip_PrintBtnTip.SetToolTip(this.btn_PrintItemDel, "删除");
            this.btn_PrintItemDel.UseVisualStyleBackColor = true;
            this.btn_PrintItemDel.Click += new System.EventHandler(this.btn_PrintItemDel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbx_PrintItem);
            this.panel1.Controls.Add(this.btn_PrintItemMoveUp);
            this.panel1.Controls.Add(this.btn_PrintItemDel);
            this.panel1.Controls.Add(this.btn_PrintItemMoveDown);
            this.panel1.Controls.Add(this.btn_PrintItemAdd);
            this.panel1.Location = new System.Drawing.Point(24, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 173);
            this.panel1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "打印参数设置（双击编辑，回车保存）：";
            // 
            // tbx_Edit
            // 
            this.tbx_Edit.Location = new System.Drawing.Point(0, 0);
            this.tbx_Edit.Name = "tbx_Edit";
            this.tbx_Edit.Size = new System.Drawing.Size(100, 21);
            this.tbx_Edit.TabIndex = 0;
            // 
            // printItemSettingBindingSource
            // 
            this.printItemSettingBindingSource.DataSource = typeof(YuDa_DeviceCreate.PrintItemSetting);
            // 
            // cbx_2PageCount
            // 
            this.cbx_2PageCount.FormattingEnabled = true;
            this.cbx_2PageCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbx_2PageCount.Location = new System.Drawing.Point(230, 122);
            this.cbx_2PageCount.Name = "cbx_2PageCount";
            this.cbx_2PageCount.Size = new System.Drawing.Size(93, 20);
            this.cbx_2PageCount.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "打印页数：";
            // 
            // chx_Template1
            // 
            this.chx_Template1.AutoSize = true;
            this.chx_Template1.Location = new System.Drawing.Point(37, 55);
            this.chx_Template1.Name = "chx_Template1";
            this.chx_Template1.Size = new System.Drawing.Size(54, 16);
            this.chx_Template1.TabIndex = 5;
            this.chx_Template1.Text = "模板1";
            this.chx_Template1.UseVisualStyleBackColor = true;
            // 
            // chx_Template2
            // 
            this.chx_Template2.AutoSize = true;
            this.chx_Template2.Location = new System.Drawing.Point(37, 124);
            this.chx_Template2.Name = "chx_Template2";
            this.chx_Template2.Size = new System.Drawing.Size(54, 16);
            this.chx_Template2.TabIndex = 20;
            this.chx_Template2.Text = "模板2";
            this.chx_Template2.UseVisualStyleBackColor = true;
            // 
            // tbx_Template1QRCodeUrl
            // 
            this.tbx_Template1QRCodeUrl.Location = new System.Drawing.Point(27, 76);
            this.tbx_Template1QRCodeUrl.Multiline = true;
            this.tbx_Template1QRCodeUrl.Name = "tbx_Template1QRCodeUrl";
            this.tbx_Template1QRCodeUrl.Size = new System.Drawing.Size(371, 40);
            this.tbx_Template1QRCodeUrl.TabIndex = 21;
            this.tip_PrintBtnTip.SetToolTip(this.tbx_Template1QRCodeUrl, "模板一跳转链接配置");
            // 
            // tbx_Template2QRCodeUrl
            // 
            this.tbx_Template2QRCodeUrl.Location = new System.Drawing.Point(28, 149);
            this.tbx_Template2QRCodeUrl.Multiline = true;
            this.tbx_Template2QRCodeUrl.Name = "tbx_Template2QRCodeUrl";
            this.tbx_Template2QRCodeUrl.Size = new System.Drawing.Size(370, 36);
            this.tbx_Template2QRCodeUrl.TabIndex = 22;
            this.tip_PrintBtnTip.SetToolTip(this.tbx_Template2QRCodeUrl, "模板一跳转链接配置");
            // 
            // PrintSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 411);
            this.Controls.Add(this.tbx_Template2QRCodeUrl);
            this.Controls.Add(this.tbx_Template1QRCodeUrl);
            this.Controls.Add(this.chx_Template2);
            this.Controls.Add(this.cbx_2PageCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txb_ProductDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_SaveSetting);
            this.Controls.Add(this.chx_Template1);
            this.Controls.Add(this.cbx_1PageCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbx_PrintDeviceName);
            this.Controls.Add(this.lbl_PrintDeviceName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印配置";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printItemSettingBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_PrintDeviceName;
        private System.Windows.Forms.ComboBox cbx_PrintDeviceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_1PageCount;
        private System.Windows.Forms.Button btn_SaveSetting;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_ProductDate;
        private System.Windows.Forms.ListBox lbx_PrintItem;
        private System.Windows.Forms.Button btn_PrintItemMoveUp;
        private System.Windows.Forms.Button btn_PrintItemMoveDown;
        private System.Windows.Forms.ToolTip tip_PrintBtnTip;
        private System.Windows.Forms.Button btn_PrintItemAdd;
        private System.Windows.Forms.Button btn_PrintItemDel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource printItemSettingBindingSource;
        private System.Windows.Forms.ComboBox cbx_2PageCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chx_Template1;
        private System.Windows.Forms.CheckBox chx_Template2;
        private System.Windows.Forms.TextBox tbx_Template1QRCodeUrl;
        private System.Windows.Forms.TextBox tbx_Template2QRCodeUrl;
    }
}