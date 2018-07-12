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
    public partial class frmAgent : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "公司信息管理";
            
        public frmAgent()
        {
            InitializeComponent();
            dgvAgents.AutoGenerateColumns = false;
            //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            
        }

        private bool checkInput()
        {
            return true;
        }

        private void frmAgent_Load(object sender, EventArgs e)
        {
            loadAgents();
        }

        /// <summary>
        /// 加载公司列表
        /// </summary>
        private void loadAgents()
        {
            //dgvAgents.Rows.Clear();

            var data = db.v_AgentInfos.Where(ag => ag.AI_ISDEL == false).Select(a => a);
            dgvAgents.DataSource = data;
            
        }

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if(dgvAgents.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一条记录后进行操作", this.funName, MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("确定删除此记录吗", "公司信息管理", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int aid = (int)dgvAgents.SelectedRows[0].Cells[0].Value;
                var agent = db.AGENT_INFOs.Where(ai => ai.AI_ID == aid).Select(a => a).Single();
                agent.AI_ISDEL = true;
                db.SubmitChanges();
                MessageBox.Show("删除成功", "公司信息管理", MessageBoxButtons.OK);
                loadAgents();
            }
             
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 新建公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            using (frmAgentAssitant fa = new frmAgentAssitant())
            {
                fa.Flag = "NEW";
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadAgents();
                }
            }
        }

        /// <summary>
        /// 修改公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAgents.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一行记录后进行修改操作",this.funName,MessageBoxButtons.OK);
                return;
            }
            using (frmAgentAssitant fa = new frmAgentAssitant())
            {
                fa.Flag = "UPDATE";
                fa.AgentID = (int)dgvAgents.SelectedRows[0].Cells[0].Value;
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadAgents();
                }
            }
        }

        

        
    }
}
