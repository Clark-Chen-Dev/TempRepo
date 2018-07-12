using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaxDemo
{
    public partial class frmMe : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        public frmMe()
        {
            InitializeComponent();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNew.Text.Trim() == "" || txtOld.Text.Trim() == "" || txtReNew.Text.Trim() == "")
            {
                MessageBox.Show("密码为空，请检查");
                return;
            }
            if (txtNew.Text.Trim() != txtReNew.Text.Trim())
            {
                MessageBox.Show("两次新密码不一致，请检查");
                return;
            }
            var u = db.USER_INFOs.Where(a=>a.UI_ID==SysUtil.CurrentUserID() && a.UI_PASSWORD == MD5Util.GetHash(txtOld.Text)).SingleOrDefault();
            if(u!=null)
            {
                u.UI_PASSWORD = MD5Util.GetHash(txtReNew.Text.Trim());
                db.SubmitChanges();
                MessageBox.Show("密码修改成功");
            }
            else
            {
                MessageBox.Show("原密码有误，请检查");
                return;
            }
        }
    }
}
