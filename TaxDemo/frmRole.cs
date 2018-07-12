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
    /// 角色管理窗口
    /// </summary>
    public partial class frmRole : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();

        public frmRole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRole_Load(object sender, EventArgs e)
        {
            clearData();
            loadRoles();
        }

        private void clearData()
        {
            lbxYes.Items.Clear();
            lbxNot.Items.Clear();
            txtRoleName.Text = string.Empty;
            txtRoleName.ReadOnly = true;
        }

        /// <summary>
        /// 加载角色列表
        /// </summary>
        private void loadRoles()
        {
            lbxRoles.Items.Clear(); 
            
            var data = db.GROUP_INFOs.Select(p => p.GI_NAME);
            foreach (var d in data)
            {
                lbxRoles.Items.Add(d.ToString());
            }

        }

        /// <summary>
        /// 权限调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            if (lbxNot.SelectedItem != null)
            {
                lbxYes.Items.Add(lbxNot.SelectedItem);
                lbxNot.Items.Remove(lbxNot.SelectedItem);
            }
        }

        /// <summary>
        /// 权限调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (lbxYes.SelectedItem != null)
            {
                lbxNot.Items.Add(lbxYes.SelectedItem);
                lbxYes.Items.Remove(lbxYes.SelectedItem);
            }
        }

        /// <summary>
        /// 新建角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            
            clearData();
            loadAU();
            txtRoleName.ReadOnly = false;

        }

        /// <summary>
        /// 加载权限列表
        /// </summary>
        private void loadAU()
        {
            
            var data1 = db.AUTHORITY_LISTs.Where(p => p.AL_PARENTID != 0);
            foreach (var d in data1)
            {
                lbxNot.Items.Add(d.AL_NAME.ToString());
            }
        }

        /// <summary>
        /// 角色选择后,显示相应信息与权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearData();
            txtRoleName.Text = lbxRoles.SelectedItem.ToString();

            
            var data = db.v_GAUTHORITies.Where(p=>p.GI_NAME==lbxRoles.SelectedItem.ToString()).Select(p=>p.GA_ALNAME);

            //获取权限列表并显示
            var data1 = db.AUTHORITY_LISTs.Where(p => p.AL_PARENTID != 0).Select(p => p.AL_NAME);
            foreach (var d in data1)
            {
                if (data.Contains(d))
                {
                    lbxYes.Items.Add(d);
                }
                else
                {
                    lbxNot.Items.Add(d);
                }
            }
        }

        /// <summary>
        ///新建或更改角色信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(checkisok()==true)
            {

                if (db.GROUP_INFOs.Count(q => q.GI_NAME == txtRoleName.Text) == 0)  //新建
                {

                    GROUP_INFO gi = new GROUP_INFO
                    {
                        GI_NAME = txtRoleName.Text.Trim()
                    };

                    db.GROUP_INFOs.InsertOnSubmit(gi);
                    db.SubmitChanges();

                    //配置权限
                    foreach (var i in lbxYes.Items)
                    {
                        GROUP_AUTHORITY ga = new GROUP_AUTHORITY { GA_GIID = gi.GI_ID, GA_ALNAME = i.ToString() };
                        db.GROUP_AUTHORITies.InsertOnSubmit(ga);
                    }
                    db.SubmitChanges();

                    MessageBox.Show("添加成功！");
                }
                else //修改
                {
                    var gi = db.GROUP_INFOs.First(q => q.GI_NAME == txtRoleName.Text);

                    //权限更改
                    var deleteOrderDetails = db.GROUP_AUTHORITies.Where(p => p.GA_GIID == gi.GI_ID);
                    foreach (var detail in deleteOrderDetails)
                    {
                        db.GROUP_AUTHORITies.DeleteOnSubmit(detail);
                    }

                    //db.SubmitChanges();

                    foreach (var i in lbxYes.Items)
                    {
                        GROUP_AUTHORITY ga = new GROUP_AUTHORITY { GA_GIID = gi.GI_ID, GA_ALNAME = i.ToString() };
                        db.GROUP_AUTHORITies.InsertOnSubmit(ga);
                    }
                    db.SubmitChanges();
                    MessageBox.Show("修改成功！");
                }

                loadRoles();

            }
        
        }

        private bool checkisok()
        {
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void benDel_Click(object sender, EventArgs e)
        {
            if(lbxRoles.SelectedItems.Count!=1)
            {
                MessageBox.Show("请选择一行记录后进行操作");
                return;
            }
            string name = lbxRoles.SelectedItem.ToString();
            var data = db.GROUP_INFOs.SingleOrDefault(p => p.GI_NAME == name);
            var alist = db.GROUP_AUTHORITies.Where(a => a.GA_GIID == data.GI_ID);
            db.GROUP_INFOs.DeleteOnSubmit(data);
            db.GROUP_AUTHORITies.DeleteAllOnSubmit(alist);
            db.SubmitChanges();
            loadRoles();
            MessageBox.Show("删除成功");
        }
    }
}
