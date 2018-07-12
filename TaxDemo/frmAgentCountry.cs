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
    public partial class frmAgentCountry : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "公司国别维护";

        public frmAgentCountry()
        {
            InitializeComponent();
            dgvAgentCs.AutoGenerateColumns = false;
            //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void frmAgentCountry_Load(object sender, EventArgs e)
        {
            loadAgentCs(); 
             
            
        }

        
        /// <summary>
        /// 公司国别加载
        /// </summary>
        private void loadAgentCs()
        {
            //dgvAgentCs.Rows.Clear();

            var data = db.v_AgentCountries.Where(ag => ag.AI_ISDEL == false && ag.AC_ISDEL == false).OrderBy(c=>c.AC_AIID).Select(a => a);
            dgvAgentCs.DataSource = data;
             
        }

       

        private bool checkInput()
        {
            return true;
        }

        /// <summary>
        /// 删除公司国别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void benDel_Click(object sender, EventArgs e)
        {
            if (dgvAgentCs.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一条记录后进行操作", this.funName, MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("确定删除此记录吗", this.funName, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int aid = (int)dgvAgentCs.SelectedRows[0].Cells[1].Value;
                var agent = db.AGENT_COUNTRies.Where(ai => ai.AC_ID == aid).Select(a => a).Single();
                agent.AC_ISDEL = true;
                db.SubmitChanges();
                MessageBox.Show("删除成功", this.funName, MessageBoxButtons.OK);
                loadAgentCs();
            }
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmAgentCAssistant fa = new frmAgentCAssistant())
            {
                fa.Flag = "NEW";
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadAgentCs();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(dgvAgentCs.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一行记录后进行操作",this.funName,MessageBoxButtons.OK);
                return;
            }

            using (frmAgentCAssistant fa = new frmAgentCAssistant())
            {
                fa.Flag = "UPDATE";
                fa.ACID = (int)dgvAgentCs.SelectedRows[0].Cells[0].Value;
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadAgentCs();
                }
            }
        }

        
    }
}
