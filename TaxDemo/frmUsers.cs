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
    /// <summary>
    /// 用户管理
    /// </summary>
    public partial class frmUsers : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        

        public frmUsers()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            txtUserName.ReadOnly = true;

            loadUsers();
            loadRoles();
        }

        /// <summary>
        /// 加载角色列表
        /// </summary>
        private void loadRoles()
        {
            cbxRoles.Items.Clear();

            var data = from i in db.GROUP_INFOs select i;

            foreach (var d in data)
            {
                cbxRoles.Items.Add(d.GI_NAME);
            }
            cbxRoles.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载用户
        /// </summary>
        private void loadUsers()
        {
            tvUsers.Nodes.Clear();

            var data = from i in db.GROUP_INFOs select i;

            foreach (var d in data)
            {
                TreeNode tn = new TreeNode(d.GI_NAME);
                var user = from u in db.USER_INFOs where u.UI_GIID == d.GI_ID select u;
                foreach (var u in user)
                {
                    tn.Nodes.Add(u.UI_NAME);
                }

                tvUsers.Nodes.Add(tn);
            }
        }

        private void benNew_Click(object sender, EventArgs e)
        {
            clearInput();
            txtUserName.ReadOnly = false;
        }

        private void clearInput()
        {
            txtMail.Text = txtTel.Text = txtTrueName.Text = txtUserName.Text = string.Empty;
            cbxIsValid.Checked = true;
        }

        /// <summary>
        /// 新建或更改用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isInputValid())
            {
                MessageBox.Show("请检查输入项！");
                return;
            }

            var gi = from g in db.GROUP_INFOs where g.GI_NAME == cbxRoles.SelectedItem.ToString() select g;

            if (txtUserName.ReadOnly == false) //新建
            {

                USER_INFO u = new USER_INFO
                {
                    UI_ISVALID = cbxIsValid.Checked,
                    UI_MAIL = txtMail.Text.Trim(),
                    UI_NAME = txtUserName.Text.Trim(),
                    UI_TEL = txtTel.Text.Trim(),
                    UI_PASSWORD = MD5Util.GetHash("123456"), //txtUserName.Text.Trim()
                    UI_TRUENAME = txtTrueName.Text.Trim(),
                    UI_GIID = gi.First().GI_ID
                };

                db.USER_INFOs.InsertOnSubmit(u);
                
            }
            else //更改
            {
                var user = db.USER_INFOs.Where(u => u.UI_NAME == txtUserName.Text).SingleOrDefault();
                user.UI_GIID = gi.First().GI_ID;
                user.UI_ISVALID = cbxIsValid.Checked;
                user.UI_MAIL = txtMail.Text.Trim();
                user.UI_TEL = txtTel.Text.Trim();
                user.UI_TRUENAME = txtTrueName.Text.Trim();
                
            }

            db.SubmitChanges();

            MessageBox.Show("数据提交成功！");
            loadUsers();
            txtUserName.ReadOnly = true;

        }

        private bool isInputValid()
        {
            return true;
        }

        /// <summary>
        /// 用户信息显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvUsers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvUsers.SelectedNode.Level == 1)
            {
                
                var user = db.v_UGroups.Where( u=> u.UI_NAME == tvUsers.SelectedNode.Text).SingleOrDefault();
                if (user == null)
                {
                    MessageBox.Show("用户信息查找失败");
                    return;
                }

                txtMail.Text = user.UI_MAIL;
                txtTel.Text = user.UI_TEL;
                txtTrueName.Text = user.UI_TRUENAME;
                txtUserName.Text = user.UI_NAME;
                cbxIsValid.Checked = user.UI_ISVALID;
                cbxRoles.SelectedIndex =  cbxRoles.Items.IndexOf(user.GI_NAME); 
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void benDel_Click(object sender, EventArgs e)
        {
            if (tvUsers.SelectedNode.Level == 1)
            {
                DialogResult dialogResult = MessageBox.Show("确定删除此用户吗?", "用户管理", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var user = db.USER_INFOs.Where(u => u.UI_NAME == txtUserName.Text).SingleOrDefault();
                    db.USER_INFOs.DeleteOnSubmit(user);
                    db.SubmitChanges();
                    MessageBox.Show("删除成功！");
                    loadUsers();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择具体用户进行操作");
            }
        }
    }
}
