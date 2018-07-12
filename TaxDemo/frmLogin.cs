using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Deployment.Application;

namespace TaxDemo
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    public partial class frmLogin : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();

        public string UserName;
        public frmLogin()
        {
            InitializeComponent();
            //Assembly assembly =  Assembly.GetEntryAssembly();
            //FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            //string version = fileVersionInfo.ProductVersion;
            //lblVersion.Text = version;
            //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
        }

        private void LoadDefaultValues()
        {
            txtPassword.Text = "";
            txtName.Text = "";
        }

        private bool IsValidate()
        {
            errorProvider1.Clear();
            bool result = true;

            // Sql Server Name
            if (txtName.Text.Trim().Length <= 0)
            {
                errorProvider1.SetError(txtName, "Please Provide User Name.");
                result = false;
            }

            // CataLog
            if (txtPassword.Text.Trim().Length <= 0)
            {
                errorProvider1.SetError(txtPassword, "Please Provide Password.");
                result = false;
            }

            return result;

        }

        private bool TestConnection()
        {
            
            return true;

        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            
            if (TestConnection())
            {
                try
                {
                    UserName = txtName.Text.Trim();
                    string password = MD5Util.GetHash(txtPassword.Text.Trim()); //password HASH
                    USER_INFO ui =  db.USER_INFOs.SingleOrDefault(u => u.UI_NAME == UserName && u.UI_PASSWORD == password);
                    if (ui==null)
                    {
                        MessageBox.Show("用户名或密码错误！");
                    }
                    else
                    {
                        SysUtil.setCurrentUserID(ui.UI_ID);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to load schema.\n" + ex.Message, "Error to load schema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadDefaultValues();
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                lblVersion.Text = string.Concat("V", myVersion); //加载版本号
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }
    }
}
