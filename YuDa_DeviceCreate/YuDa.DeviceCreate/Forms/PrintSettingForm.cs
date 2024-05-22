using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuDa_DeviceCreate
{
    public partial class PrintSettingForm : Form
    {
        TextBox tbx_Edit = new TextBox();

        List<PrintItemSetting> printItems = new List<PrintItemSetting>();

        public PrintSettingForm()
        {
            InitializeComponent();
            //初始化打印机下拉列表选项
            InitPrinterComboBox();

            SetTextBox();
            SetListBox();

            // 绑定初始化数据
            BindingInitInfo();
        }

        #region 初始化Init
        /// <summary>
        /// 绑定初始化数据
        /// </summary>
        private void BindingInitInfo()
        {
            this.cbx_1PageCount.Text = Properties.Settings.Default.PrintTemplate1Count.ToString();
            this.cbx_2PageCount.Text = Properties.Settings.Default.PrintTemplate2Count.ToString();
            this.chx_Template1.Checked = Properties.Settings.Default.OpenPrintTemplate1;
            this.chx_Template2.Checked = Properties.Settings.Default.OpenPrintTemplate2;
            this.tbx_Template1QRCodeUrl.Text = Properties.Settings.Default.Template1QRCodeUrl;
            this.tbx_Template2QRCodeUrl.Text = Properties.Settings.Default.Template2QRCodeUrl;

            this.txb_ProductDate.Text = string.IsNullOrWhiteSpace(Properties.Settings.Default.ProductDate)
                ? DateTime.Now.ToString("yyyy.MM")
                : Properties.Settings.Default.ProductDate;

            printItems = Properties.Settings.Default.PrintItemJson.JsonToObject<List<PrintItemSetting>>();

            lbx_PrintItem.Items.AddRange(printItems.Select(s => s.Item).ToArray());
        }

        //设置ListBox，高度为20
        private void SetListBox()
        {
            lbx_PrintItem.DrawMode = DrawMode.OwnerDrawFixed;
            lbx_PrintItem.ItemHeight = 20;
        }

        private void SetTextBox()
        {
            this.tbx_Edit.KeyDown += new KeyEventHandler(tbx_Edit_KeyDown);
            //this.tbx_Edit.Size = new System.Drawing.Size(263, 21);
            this.tbx_Edit.Visible = false;
        }

        /// <summary>
        /// KeyDown事件定义
        /// </summary>
        private void tbx_Edit_KeyDown(object sender, KeyEventArgs e)
        {
            //Enter键 更新项并隐藏编辑框
            if (e.KeyCode == Keys.Enter)
            {
                lbx_PrintItem.Items[lbx_PrintItem.SelectedIndex] = tbx_Edit.Text;
                printItems[lbx_PrintItem.SelectedIndex].Item = tbx_Edit.Text;
                tbx_Edit.Visible = false;
            }
            //Esc键 直接隐藏编辑框
            if (e.KeyCode == Keys.Escape)
                tbx_Edit.Visible = false;
        }

        /// <summary>
        /// 打印机列表
        /// </summary>
        private void InitPrinterComboBox()
        {
            List<string> printer = new List<string>();
            foreach (string item in PrinterSettings.InstalledPrinters)
            {
                printer.Add(item);
            }
            this.cbx_PrintDeviceName.DataSource = printer;
            this.cbx_PrintDeviceName.Text = Properties.Settings.Default.Printer;
        }
        #endregion

        #region lbx_PrintItem
        private void lbx_PrintItem_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(lbx_PrintItem.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }

        private void lbx_PrintItem_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Edit.Visible = false;
        }

        private void lbx_PrintItem_DoubleClick(object sender, EventArgs e)
        {
            int itemSelected = lbx_PrintItem.SelectedIndex;

            if (itemSelected >= 0)
            {
                if (!printItems[itemSelected].CanDel)
                {
                    MessageBox.Show("固定选项不可编辑");
                    return;
                }
                string itemText = lbx_PrintItem.Items[itemSelected].ToString();

                Rectangle rect = lbx_PrintItem.GetItemRectangle(itemSelected);
                tbx_Edit.Parent = lbx_PrintItem;
                tbx_Edit.Bounds = rect;
                tbx_Edit.Multiline = true;
                tbx_Edit.Visible = true;
                tbx_Edit.Text = itemText;
                tbx_Edit.Focus();
                //tbx_Edit.SelectAll();
            }
        }

        /// <summary>
        /// 数据上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemMoveUp_Click(object sender, EventArgs e)
        {
            int lbxLength = this.lbx_PrintItem.Items.Count;//listbox的长度   
            int iselect = this.lbx_PrintItem.SelectedIndex;//listbox选择的索引   
            if (lbxLength > iselect && iselect > 0)
            {
                object oTempItem = this.lbx_PrintItem.SelectedItem;
                this.lbx_PrintItem.Items.RemoveAt(iselect);
                this.lbx_PrintItem.Items.Insert(iselect - 1, oTempItem);
                this.lbx_PrintItem.SelectedIndex = iselect - 1;

                PrintItemSetting printItemSetting = printItems[iselect];
                printItems.RemoveAt(iselect);
                printItems.Insert(iselect - 1, printItemSetting);
            }
        }

        /// <summary>
        /// 数据下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemMoveDown_Click(object sender, EventArgs e)
        {
            int lbxLength = this.lbx_PrintItem.Items.Count;//listbox的长度
            int iselect = this.lbx_PrintItem.SelectedIndex;//listbox选择的索引
            if (lbxLength > iselect && iselect < lbxLength - 1 && iselect >= 0)
            {
                object oTempItem = this.lbx_PrintItem.SelectedItem;
                this.lbx_PrintItem.Items.RemoveAt(iselect);
                this.lbx_PrintItem.Items.Insert(iselect + 1, oTempItem);
                this.lbx_PrintItem.SelectedIndex = iselect + 1;

                PrintItemSetting printItemSetting = printItems[iselect];
                printItems.RemoveAt(iselect);
                printItems.Insert(iselect + 1, printItemSetting);
            }
        }

        /// <summary>
        /// listbox添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemAdd_Click(object sender, EventArgs e)
        {
            this.lbx_PrintItem.Items.Add("Item");
            printItems.Add(new PrintItemSetting() { Item = "Item" });
        }
        /// <summary>
        /// listbox删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemDel_Click(object sender, EventArgs e)
        {
            int iselect = this.lbx_PrintItem.SelectedIndex;//listbox选择的索引

            if (iselect >= 0)
            {
                if (!printItems[iselect].CanDel)
                {
                    MessageBox.Show("固定选项不可删除");
                    return;
                }
                this.lbx_PrintItem.Items.RemoveAt(iselect);
                printItems.RemoveAt(iselect);
            }
        }

        #endregion

        /// <summary>
        /// 设置保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SaveSetting_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Printer = this.cbx_PrintDeviceName.Text;
            Properties.Settings.Default.PrintTemplate1Count = int.Parse(this.cbx_1PageCount.Text);
            Properties.Settings.Default.PrintTemplate2Count = int.Parse(this.cbx_2PageCount.Text);
            //Properties.Settings.Default.AutoPrint = this.chx_Template1.Checked;
            Properties.Settings.Default.PrintItemJson = printItems.ObjectToJson();
            Properties.Settings.Default.ProductDate = this.txb_ProductDate.Text;
            Properties.Settings.Default.OpenPrintTemplate1 = this.chx_Template1.Checked;
            Properties.Settings.Default.OpenPrintTemplate2 = this.chx_Template2.Checked;
            Properties.Settings.Default.Template1QRCodeUrl = this.tbx_Template1QRCodeUrl.Text;
            Properties.Settings.Default.Template2QRCodeUrl = this.tbx_Template2QRCodeUrl.Text;

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
