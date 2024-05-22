namespace YuDa_DeviceCreate
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.tbx_PassWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_LoginID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbx_PassWord
            // 
            this.tbx_PassWord.Location = new System.Drawing.Point(157, 105);
            this.tbx_PassWord.Name = "tbx_PassWord";
            this.tbx_PassWord.PasswordChar = '*';
            this.tbx_PassWord.Size = new System.Drawing.Size(100, 21);
            this.tbx_PassWord.TabIndex = 7;
            this.tbx_PassWord.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Control_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "密码:";
            // 
            // txb_LoginID
            // 
            this.txb_LoginID.Location = new System.Drawing.Point(157, 72);
            this.txb_LoginID.Name = "txb_LoginID";
            this.txb_LoginID.Size = new System.Drawing.Size(100, 21);
            this.txb_LoginID.TabIndex = 5;
            this.txb_LoginID.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Control_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "登录账号:";
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(262, 172);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 8;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 221);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.tbx_PassWord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_LoginID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbx_PassWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_LoginID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Login;
    }
}