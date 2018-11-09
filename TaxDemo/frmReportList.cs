using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Configuration;

namespace TaxDemo
{
    public partial class frmReportList : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "报表记录查看";
        public frmReportList()
        {
            InitializeComponent();
            dgvReportList.AutoGenerateColumns = false;

            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxAgentCs.DisplayMember = "AC_NAME";
            cbxAgentCs.ValueMember = "AC_ID";

            cbxStartYear.DisplayMember = "AY_NAME";
            cbxStartYear.ValueMember = "AY_NAME";

            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            cbxName.DisplayMember = "TP_NAME"; // 姓名
            cbxName.ValueMember = "TP_ID";

            cbxID.DisplayMember = "TP_IDNUMBER"; // 身份证号
            cbxID.ValueMember = "TP_ID";

            cbxEndYear.DisplayMember = "AY_NAME"; // 终止年度
            cbxEndYear.ValueMember = "AY_NAME";
            /* ***********  End  *********/

        }

        private void frmReportList_Load(object sender, EventArgs e)
        {
            //loadReports();
            loadAgents();
            loadYears();

            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            LoadTax_Player();
            cbxName.SelectedIndex = -1;
            cbxID.SelectedIndex = -1;
            /* ***********  End  *********/
        }

        /* Modified by cyq 20160331 */
        /* ***********  Begin  *********/
        private void LoadTax_Player()
        {
            cbxName.Items.Clear();

            // 加载姓名
            var names = db.TAX_PLAYERs
                     .OrderBy(tp => tp.TP_NAME)
                     .Select(tp => new { TP_ID = tp.TP_ID, TP_NAME = tp.TP_NAME });
            cbxName.DataSource = names;

            // 加载身份证
            var ids = db.TAX_PLAYERs
                        .Where(tp => tp.TP_IDNUMBER != string.Empty)
                        .OrderBy(tp => tp.TP_IDNUMBER)
                        .Select(tp => new { TP_ID = tp.TP_ID, TP_IDNUMBER = tp.TP_IDNUMBER });
            cbxID.DataSource = ids;
        }
        /* ***********  End  *********/

        private void loadAgents()
        {
            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            cbxAgents.DataSource = data;


        }

        private void loadYears()
        {
            //cbxAgents.Items.Clear();

            //var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            //cbxAgents.DataSource = data;

            cbxStartYear.Items.Clear();

            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            //var data = db.AGENT_YEARs.Select(a => a);
            var starYears = db.AGENT_YEARs.OrderByDescending(year => year.AY_NAME).Select(a => a);
            /* ***********  End  *********/

            cbxStartYear.DataSource = starYears;


            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            var endYears = db.AGENT_YEARs.OrderByDescending(year => year.AY_NAME).Select(a => a);
            cbxEndYear.DataSource = endYears;
            /* ***********  End  *********/
        }

        private void loadReports()
        {
            var data = db.v_REPORTs.Select(a => a).ToList();
            dgvReportList.DataSource = data;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一行记录后进行操作", this.funName, MessageBoxButtons.OK);
                return;
            }

            int riid = (int)dgvReportList.SelectedRows[0].Cells[0].Value;

            using (frmReportNew fa = new frmReportNew())
            {
                Report1Wrapper rw1 = new Report1Wrapper();
                Report2Wrapper rw2 = new Report2Wrapper();

                REPORT_INFO ri = db.REPORT_INFOs.SingleOrDefault(a => a.RI_ID == riid);
                rw1.Base = rw2.Base = ri;

                rw1.ACNAME = dgvReportList.SelectedRows[0].Cells[3].Value.ToString();
                rw2.TPNAME = rw1.TPNAME = dgvReportList.SelectedRows[0].Cells[4].Value.ToString();
                rw1.Details = db.REPORT1_DETAILs.Where(a => a.R1_RIID == riid).ToList();
                rw2.Details = db.REPORT2_DETAILs.Where(a => a.R2_RIID == riid).ToList();

                fa.RW1 = rw1;
                fa.RW2 = rw2;
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cbxAgents.SelectedItem == null)
            {
                /* Modified by cyq 20160331 */
                /* ***********  Begin  *********/
                //MessageBox.Show("请选择国别");
                MessageBox.Show("请选择所属公司");
                /* ***********  End  *********/

                return;
            }

            LoadData();

        }

        private void LoadData()
        {
            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/

            //if (cbxAgentCs.SelectedItem == null)
            //{
            //    if (cbxStartYear.SelectedItem == null)
            //    {
            //        var data = db.v_REPORTs.Where(a => a.AI_ID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID).Select(p => p);
            //        dgvReportList.DataSource = data;
            //    }
            //    else
            //    {
            //        var data = db.v_REPORTs.Where(a => (a.AI_ID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID) && a.RI_AYNAME == ((AGENT_YEAR)cbxStartYear.SelectedItem).AY_NAME).Select(p => p);
            //        dgvReportList.DataSource = data;
            //    }
            //}
            //else
            //{
            //    if (cbxStartYear.SelectedItem == null)
            //    {
            //        var data = db.v_REPORTs.Where(a => (a.AI_ID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID) && a.RI_ACID == ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID).Select(p => p);
            //        dgvReportList.DataSource = data;
            //    }
            //    else
            //    {
            //        var data = db.v_REPORTs.Where(a => (a.AI_ID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID) && a.RI_ACID == ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID && a.RI_AYNAME == ((AGENT_YEAR)cbxStartYear.SelectedItem).AY_NAME).Select(p => p);
            //        dgvReportList.DataSource = data;
            //    }
            //}

            //************************************************************************************************************************
            StringBuilder sqlBldr = new StringBuilder(200);
            int ai_id = ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID;

            sqlBldr.AppendLine("SELECT RI_ID,  ");
            sqlBldr.AppendLine(" RI_AYNAME, ");
            sqlBldr.AppendLine(" AI_NAME, ");
            sqlBldr.AppendLine(" AC_NAME, ");
            sqlBldr.AppendLine(" TP_NAME, ");
            sqlBldr.AppendLine(" RI_SUMTAXSALARY, ");
            sqlBldr.AppendLine(" RI_SUMTAXRMB, ");
            sqlBldr.AppendLine(" RI_SUMTAXALREADYRMB, ");
            sqlBldr.AppendLine(" UI_NAME, ");
            sqlBldr.AppendLine(" RI_CREATETIME ");
            sqlBldr.AppendLine(" FROM v_REPORT ");
            sqlBldr.AppendFormat(" WHERE AI_ID = {0}", ai_id); // 所属公司

            // 所属国别
            if (cbxAgentCs.SelectedIndex != -1)
            {
                sqlBldr.AppendFormat(" AND RI_ACID = {0} ", ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID);
            }

            // 起始年度、终止年度
            if (cbxStartYear.SelectedIndex != -1 && cbxEndYear.SelectedIndex != -1)
            {
                sqlBldr.AppendFormat(" AND RI_AYNAME >= {0} AND  RI_AYNAME <= {1} ",
                    ((AGENT_YEAR)cbxStartYear.SelectedItem).AY_NAME,
                    ((AGENT_YEAR)cbxEndYear.SelectedItem).AY_NAME);
            }

            // 姓名
            if (cbxName.SelectedIndex != -1)
            {
                sqlBldr.AppendFormat(" AND RI_TPID = {0} ", (int)cbxName.SelectedValue);
            }


            // 身份证
            if (cbxID.SelectedIndex != -1)
            {
                sqlBldr.AppendFormat(" AND RI_TPID = {0} ", (int)cbxID.SelectedValue);
            }

            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            //var query = db.ExecuteQuery<v_REPORT>(sqlBldr.ToString()).ToArray();
            // if (!query.Any())
            //{
            //    MessageBox.Show("无满足查询条件的可用记录", "提示", 
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvReportList.DataSource = null;
            //    return;
            //}
            
            string connStr = ConfigurationManager.ConnectionStrings["TaxDemo.Properties.Settings.testdbConnectionString"].ConnectionString;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlBldr.ToString(), connStr);
            DataTable query = new DataTable();
            adapter.Fill(query);

            if (query.Rows.Count == 0)
            { 
                MessageBox.Show("无满足查询条件的可用记录", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvReportList.DataSource = null;
                return;
            }

            // 添加总计数据行
            DataRow totalRow = query.NewRow();
            totalRow["RI_SUMTAXSALARY"] = Convert.ToDecimal(
                query.Compute("SUM(RI_SUMTAXSALARY)", "")); // 应纳税所得额
            totalRow["RI_SUMTAXRMB"] = Convert.ToDecimal(
                query.Compute("SUM(RI_SUMTAXRMB)", "")); // 应纳税额合计
            totalRow["RI_SUMTAXALREADYRMB"] = Convert.ToDecimal(
               query.Compute("SUM(RI_SUMTAXALREADYRMB)", "")); // 已纳税额
            query.Rows.Add(totalRow);

            /* ***********  End  *********/
           
            dgvReportList.DataSource = query;

            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            // 对于应纳税所得额、应纳税额合计、已纳税额加粗显示
            using (Font font = new Font(
              dgvReportList.DefaultCellStyle.Font, FontStyle.Bold))
            {
                dgvReportList.Rows[query.Rows.Count - 1].Cells[5].Style.Font = font;
                dgvReportList.Rows[query.Rows.Count - 1].Cells[6].Style.Font = font;
                dgvReportList.Rows[query.Rows.Count - 1].Cells[7].Style.Font = font;
            } // end using
            /* ***********  End  *********/
            /* ***********  End  *********/

            setRowNumber(dgvReportList);

          
        }

        private void setRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
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
            int count = dgvReportList.SelectedRows.Count;
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
                    /* Modified by cyq 20160331 */
                    /* ***********  Begin  *********/
                    //int riid = (int)dgvReportList.SelectedRows[i].Cells[0].Value;
                    //db.REPORT1_DETAILs.DeleteAllOnSubmit(db.REPORT1_DETAILs.Where(a => a.R1_RIID == riid));
                    //db.REPORT2_DETAILs.DeleteAllOnSubmit(db.REPORT2_DETAILs.Where(a => a.R2_RIID == riid));
                    //db.REPORT_INFOs.DeleteAllOnSubmit(db.REPORT_INFOs.Where(a => a.RI_ID == riid));
                    //db.TAX_CREDITs.DeleteAllOnSubmit(db.TAX_CREDITs.Where(a => a.TC_RIID == riid));
                    //db.SubmitChanges();

                    int riid = (int)dgvReportList.SelectedRows[i].Cells[0].Value;

                    try
                    {
                        db.Connection.Open();
                        db.Transaction = db.Connection.BeginTransaction();
                        
                        var reportInfo = db.REPORT_INFOs.Where(a => a.RI_ID == riid);
                        var tpid = reportInfo.Select(report => report.RI_TPID).Single();
                        var relatedCredits = db.TAX_CREDITs
                                               .Where(a => a.TP_ID == tpid && (SysUtil.GetCurrentYear() - a.TC_YEAR) <= 5 
                                                   && a.TC_CREDITUSED > 0).ToList();
                        
                        if (relatedCredits.Any())
                        {
                            decimal avail = reportInfo.Single().RI_USEALL.GetValueOrDefault() - reportInfo.Single().RI_USE2.GetValueOrDefault();
                            decimal diff = 0;

                            foreach (var credit in relatedCredits)
                            {
                                diff = credit.TC_CREDITUSED.GetValueOrDefault() - avail;

                                if (diff >= 0)
                                {
                                    credit.TC_CREDITUSED -= avail;
                                    credit.TC_CREDITBALANCE += avail;
                                    break;
                                }
                                else 
                                {
                                    avail -= credit.TC_CREDITUSED.GetValueOrDefault();
                                    credit.TC_CREDITBALANCE = credit.TC_CREDITALL;
                                    credit.TC_CREDITUSED = 0;
                                }
                            }
                        }

                        db.REPORT1_DETAILs.DeleteAllOnSubmit(db.REPORT1_DETAILs.Where(a => a.R1_RIID == riid));
                        db.REPORT2_DETAILs.DeleteAllOnSubmit(db.REPORT2_DETAILs.Where(a => a.R2_RIID == riid));
                        db.REPORT_INFOs.DeleteAllOnSubmit(reportInfo);
                        db.TAX_CREDITs.DeleteAllOnSubmit(db.TAX_CREDITs.Where(a => a.TC_RIID == riid));
                        db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                        db.Transaction.Commit();
                    }
                    catch (ChangeConflictException)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show("报表记录及报表对应的留抵因未知原因无法级联删除。", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    /* ***********  End  *********/
                }
            }

            LoadData();
        }
    }
}
