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
    /// 纳税人管理
    /// </summary>
    public partial class frmPlayer : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "纳税人管理";

        public frmPlayer()
        {
            InitializeComponent();
            dgvPlayers.AutoGenerateColumns = false;

            //数据绑定
            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxAgentCs.DisplayMember = "AC_NAME";
            cbxAgentCs.ValueMember = "AC_ID";
        }

        private void frmPlayer_Load(object sender, EventArgs e)
        {
            loadAgents();

        }
        private void loadAgents()
        {
            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            cbxAgents.DataSource = data;

            
        }


        private void loadPlayers()
        {
            var data = from i in db.v_TAXPLAYERs select i;
            dgvPlayers.DataSource = data;

        }

        
        /// <summary>
        /// 新建已纳税人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
           
            using (frmPlayerAssitant fa = new frmPlayerAssitant())
            {
                fa.Flag = "NEW";
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadPlayers();
                    MessageBox.Show("新增成功", this.funName, MessageBoxButtons.OK);
                }
            }

        }

        /// <summary>
        /// 修改已纳税人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void benUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPlayers.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一行记录后进行操作", this.funName, MessageBoxButtons.OK);
                return;
            }

            using (frmPlayerAssitant fa = new frmPlayerAssitant())
            {
                fa.Flag = "UPDATE";
                fa.TaxPlayerID = (int)dgvPlayers.SelectedRows[0].Cells[0].Value;
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadPlayers();
                    MessageBox.Show("更新成功",this.funName,MessageBoxButtons.OK);

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Microsoft Excel files (*.xls)|*.xls;*.xlsx";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = choofdlog.FileName;
                       
            }
        }

        /// <summary>
        /// 已纳税人导入方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (checkItemsInFile())
            {
                ExcelHelper excel = new ExcelHelper();
                try
                {
                    excel.OpenFile(txtDir.Text, 1,true);
                    int rows = excel.GetRowsCount();
                    int i = 2;

                    for (; i <= rows; i++) //check first
                    {
                        List<string> sl = excel.GetValue(i, 1, i, 5);
                        int acid = SysUtil.GetAgentCID(sl[0], sl[1]);
                        if (acid == -1)
                        {
                            MessageBox.Show(sl[1] + "国别记录不存在，请检查", this.funName);
                            //excel.Close();
                            return;
                        }
                        if (db.TAX_PLAYERs.SingleOrDefault(a => (a.TP_ACID == acid) && (a.TP_NAME == sl[2].Replace(" ", string.Empty))) != null)
                        {
                            MessageBox.Show(sl[2] + "此纳税人已存在，请检查", this.funName);
                            //excel.Close();
                            return;
                        }

                    }

                    i = 2;
                    for (; i <= rows; i++)
                    {
                        List<string> sl = excel.GetValue(i, 1, i, 5);

                        TAX_PLAYER tp = new TAX_PLAYER
                        {

                            TP_ACID = SysUtil.GetAgentCID(sl[0], sl[1]),
                            TP_AIID = SysUtil.GetAgentID(sl[0]),
                            TP_NAME = sl[2].Replace(" ", string.Empty),
                            TP_ENNAME = sl[3],
                            TP_IDNUMBER = sl[4],

                        };
                        db.TAX_PLAYERs.InsertOnSubmit(tp);

                    }
                    db.SubmitChanges();

                    //excel.Close();
                    loadPlayers();
                    MessageBox.Show("导入成功,导入记录" + (rows - 1) + "条", this.funName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    excel.Close();
                }
            }

             
        }

        private bool checkItemsInFile()
        {
            return true;
        }

        private void dgvPlayers_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
                this.dgvPlayers.Rows[e.RowIndex + i].Cells[1].Value = (e.RowIndex + i +1).ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            LoadData();
        }

        /// <summary>
        /// 加载已纳税人记录
        /// </summary>
        private void LoadData()
        {
            if (cbxAgentCs.SelectedItem == null)
            {
                dgvPlayers.DataSource = db.v_TAXPLAYERs.Where(a => a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME).Select(a => a).ToList();
            }
            else
            {
                dgvPlayers.DataSource = db.v_TAXPLAYERs.Where(a => a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME && (a.AC_NAME == ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_NAME)).Select(a => a).ToList();
            }
        }

        private void cbxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxAgentCs.Items.Clear();

            var data = db.AGENT_COUNTRies.Where(a => a.AC_AIID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID).Select(p => p);
            foreach (var d in data)
            {
                cbxAgentCs.Items.Add(d);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int count = dgvPlayers.SelectedRows.Count;
            if (count < 1)
            {
                MessageBox.Show("请选择记录后再操作");
                return;
            }
            
            DialogResult dialogResult = MessageBox.Show("确定将此"+count+"记录删除吗", this.funName, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                for (int i = 0; i < count; i++)
                {
                    int id = (int)dgvPlayers.SelectedRows[i].Cells[0].Value;
                    var tp = db.TAX_PLAYERs.SingleOrDefault(a=>a.TP_ID==id);
                    db.TAX_PLAYERs.DeleteOnSubmit(tp);
                }
                    
                db.SubmitChanges();
                LoadData();
                MessageBox.Show("删除成功");
            }
            

        }

       
    }
}
