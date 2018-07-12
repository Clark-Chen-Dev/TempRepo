using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;

namespace TaxDemo
{
    /// <summary>
    /// 系统主界面,以菜单方式提供功能选项
    /// </summary>
    public partial class frmMain : Form
    {
        private string UserName ;
        private TAXDBDataContext db = new TAXDBDataContext();
       
        public frmMain()
        {
            InitializeComponent();
            this.Text = "个税计算系统";
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                this.Text = string.Concat(this.Text ,"V", myVersion);
            }

        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            frmLogin loginScreen = new frmLogin();
            DialogResult result = loginScreen.ShowDialog();
             
            
            if (result == DialogResult.OK)
            {
                UserName = loginScreen.UserName;
                toolStripStatusLabel1.Text = "当前用户:" + loginScreen.UserName + "   当前年度:" + SysUtil.GetCurrentYear();
                
                loadMenu();
               
            }
            else
            {
                Application.Exit();
            }
        }

        private void loadYear()
        {
            toolStripStatusLabel2.Text = "当前年度：" + SysUtil.GetCurrentYear();
        }

        /// <summary>
        /// 根据用户权限设置菜单可见性
        /// </summary>
        private void loadMenu()
        {
            List<string> ua = db.v_UAUTHORITies.Where(u => u.UI_NAME == UserName).Select(uaa => uaa.AL_NAME).ToList();
            foreach (ToolStripMenuItem menuItem in menuStrip1.Items)
            {
                foreach (ToolStripItem menu in menuItem.DropDownItems)
                {
                    if (ua.Contains(menu.Text))
                    {
                        menu.Enabled = true;
                    }
                    else
                    {
                        menu.Enabled = false;
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmAgentCountry frm = new frmAgentCountry();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 个人信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMe me = new frmMe();
            me.MdiParent = this;
            me.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 公司信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAgent frm = new frmAgent();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 员工信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPlayer frm = new frmPlayer();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 个税税率表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTaxRate frm = new frmTaxRate();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 角色权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRole frm = new frmRole();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers frm = new frmUsers();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 系统原始表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTemplateSys frm = new frmTemplateSys();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 公司原始表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTemplateAssitant frm = new frmTemplateAssitant();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 公司国别原始表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTemplateAgentCountry frm = new frmTemplateAgentCountry();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 模板查看导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTemplate frm = new frmTemplate();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 原始薪资类导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportSalary frm = new frmImportSalary();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 原始劳务类导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportREM frm = new frmImportREM();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 公司年度明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTotalDetails frm = new frmTotalDetails();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 公司ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTotalFee frm = new frmTotalFee();
            frm.MdiParent = this;
            frm.Show(); 
        }

        private void 公司倒退表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTotalBack frm = new frmTotalBack();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmYear frm = new frmYear();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 报表1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportList frm = new frmReportList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 留抵记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreditList frm = new frmCreditList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 申报大表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTotal4 frm = new frmTotal4();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 操作日志查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
