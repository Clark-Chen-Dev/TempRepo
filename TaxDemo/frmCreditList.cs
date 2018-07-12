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
    public partial class frmCreditList : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "留抵记录管理";

        public frmCreditList()
        {
            InitializeComponent();
            dgvCredit.AutoGenerateColumns = false;

            //数据绑定
            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxAgentCs.DisplayMember = "AC_NAME";
            cbxAgentCs.ValueMember = "AC_ID";
        }

        private bool checkItemsInFile()
        {
            return true;
        }

        /// <summary>
        /// 导入留抵记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (cbxAgentCs.SelectedItem == null)
            {
                MessageBox.Show("请选择国别信息后进行导入");
                return;
            }
            int acid = ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID;

            if (checkItemsInFile())
            {
                ExcelHelper excel = new ExcelHelper();
                try
                {
                    excel.OpenFile(txtDir.Text, 1, true);
                    int rows = excel.GetRowsCount();
                    int i = 2;

                    List<string> sl;
                    for (; i <= rows; i++) //首先检查纳税人和对应年份的留抵记录是否已存在
                    {
                        sl = excel.GetValue(i, 1, i, 5);
                        string tname = sl[0].Replace(" ", string.Empty);
                        int year = Convert.ToInt32(sl[1].Replace(" ", string.Empty));
                        if (db.TAX_PLAYERs.SingleOrDefault(a => (a.TP_ACID == acid) && (a.TP_NAME == tname)) == null)
                        {
                            MessageBox.Show(tname + "此纳税人不存在，请检查", this.funName);
                            //excel.Close();
                            return;
                        }
                        if (db.v_TAXCREDITs.SingleOrDefault(a => a.TC_YEAR == year && a.TP_NAME == tname) != null)
                        {
                            MessageBox.Show(tname + "此纳税人" + year + "留抵记录已存在,请检查", this.funName);
                            //excel.Close();
                            return;
                        }

                    }

                    i = 2;
                    for (; i <= rows; i++)
                    {
                        sl = excel.GetValue(i, 1, i, 5);
                        string tname = sl[0].Replace(" ", string.Empty);
                        int year = Convert.ToInt32(sl[1].Replace(" ", string.Empty));
                        int tpid = SysUtil.GetPlayerID(acid, tname);
                        if (sl[2] == string.Empty)
                        {
                            sl[2] = "0";
                        }
                        if (sl[3] == string.Empty)
                        {
                            sl[3] = "0";
                        }
                        if (sl[4] == string.Empty)
                        {
                            sl[4] = "0";

                        }

                        TAX_CREDIT tc = new TAX_CREDIT
                        {
                            TP_ID = tpid,
                            TC_YEAR = year,
                            TC_CREDITALL = Math.Abs(Convert.ToDecimal(sl[2])),
                            TC_CREDITUSED = Math.Abs(Convert.ToDecimal(sl[3])),
                            TC_CREDITBALANCE = Math.Abs(Convert.ToDecimal(sl[4]))
                        };
                        db.TAX_CREDITs.InsertOnSubmit(tc);
                    }
                    db.SubmitChanges();

                    

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
        /// 公司,国别联动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxAgentCs.Items.Clear();

            var data = db.AGENT_COUNTRies.Where(a => a.AC_AIID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID).Select(p => p);
            foreach (var d in data)
            {
                cbxAgentCs.Items.Add(d);
            }
        }

        private void frmCreditList_Load(object sender, EventArgs e)
        {
            loadAgents();
        }

        /// <summary>
        /// 公司列表加载
        /// </summary>
        private void loadAgents()
        {
            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            cbxAgents.DataSource = data;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        /// <summary>
        /// 查询留抵记录
        /// </summary>
        private void LoadData()
        {
            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            int currYear = SysUtil.GetCurrentYear();
            db.Log = Console.Out;
            if (cbxAgentCs.SelectedItem == null)
            {
                //dgvCredit.DataSource = db.v_TAXCREDITs
                //                            .Where(a => a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME)
                //                            .Select(a => a).ToList();
                dgvCredit.DataSource = db.v_TAXCREDITs
                                            .Where(a => a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME &&
                                                (currYear - a.TC_YEAR) <= 5 )
                                            .Select(a => a).ToList();
            }
            else
            {
                //dgvCredit.DataSource = db.v_TAXCREDITs
                //                         .Where(a => a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME && 
                //                            (a.AC_NAME == ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_NAME))
                //                         .Select(a => a).ToList();

                dgvCredit.DataSource = db.v_TAXCREDITs
                                        .Where(a => a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME &&
                                           (a.AC_NAME == ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_NAME) &&
                                           (currYear - a.TC_YEAR) <= 5)
                                        .Select(a => a).ToList();
            }

            

            /* ***********  End  *********/

            setRowNumber(dgvCredit);
        }

        private void setRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int count = dgvCredit.SelectedRows.Count;
            if (count == 0)
            {
                MessageBox.Show("请选择记录后进行操作");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("确定将此" + count + "记录删除吗", this.funName, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                for (int i = 0; i < count; i++)
                {
                    int clid = (int)dgvCredit.SelectedRows[i].Cells[0].Value;
                    db.TAX_CREDITs.DeleteAllOnSubmit(db.TAX_CREDITs.Where(a => a.TC_ID == clid));
                    
                    
                }
                db.SubmitChanges();
                MessageBox.Show("删除成功");
            }
            
            LoadData();
        }

    }
}
