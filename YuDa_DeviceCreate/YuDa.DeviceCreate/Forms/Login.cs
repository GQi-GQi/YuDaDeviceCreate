using log4net;
using System;
using System.Windows.Forms;

namespace YuDa_DeviceCreate
{
    public partial class Login : Form
    {
        private static ILog log = LogManager.GetLogger(typeof(Login));

        public Login()
        {
            InitializeComponent();

            //设置控件可跨线程访问
            Control.CheckForIllegalCrossThreadCalls = false;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.LoginID))
                txb_LoginID.Text = Properties.Settings.Default.LoginID;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {
            this.btn_Login.Enabled = false;

            Action action = new Action(LoginAcion);
            action.BeginInvoke(new AsyncCallback(LoginAcionCallback), action);
        }

        private void LoginAcion()
        {
            try
            {
                string LoginID = txb_LoginID.Text;
                string PassWord = tbx_PassWord.Text;

                if (string.IsNullOrEmpty(LoginID))
                {
                    MessageBox.Show("请填写登录账号");
                    return;
                }

                if (string.IsNullOrEmpty(PassWord))
                {
                    MessageBox.Show("请填写登录密码");
                    return;
                }

                LoginRequest loginRequest = new LoginRequest()
                {
                    Account = LoginID.Trim(),
                    Password = PassWord.Trim(),
                };

                var LoginResult = NetHelper.HttpPostToObject<PubulicMsgResult<LoginDataReceive>>(LoginUserInfo.ApiUrl + "/api/Account/Login", loginRequest.ObjectToJson(), NetHelper.ContentType.application_json);

                if (LoginResult.Code != ResultCode.Success)
                {
                    MessageBox.Show(LoginResult.Msg);
                    return;
                }

                LoginUserInfo.Account_Token = LoginResult.Data.AccessToken;

                var UserResult = NetHelper.HttpPostToObject<PubulicMsgResult<UserInfoReceive>>(LoginUserInfo.ApiUrl + "/api/Account/GetUserInfo", "", NetHelper.ContentType.application_json, LoginUserInfo.Account_Token, LoginUserInfo.ClientVersion);
                if (UserResult.Code != ResultCode.Success)
                {
                    MessageBox.Show(UserResult.Msg);
                    return;
                }

                LoginUserInfo.CompanyID = UserResult.Data.CompanyID;
                LoginUserInfo.CompanyName = UserResult.Data.CompanyName;
                LoginUserInfo.UserName = UserResult.Data.UserName;

                Properties.Settings.Default.LoginID = txb_LoginID.Text;
                Properties.Settings.Default.Save();


                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录异常：" + ex.Message);
                log.Error(ex);
            }
        }

        /// <summary>
        /// 回车登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)    // 回车键
            {
                btn_Login_Click(null, null);  // 登录方法
            }
        }

        private void LoginAcionCallback(IAsyncResult ar)
        {
            this.btn_Login.Enabled = true;
        }

    }
}
