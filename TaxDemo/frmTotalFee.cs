using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TaxDemo
{
    /// <summary>
    /// 公司费用
    /// </summary>
    public partial class frmTotalFee : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "12万申报服务费计算明细";
        public frmTotalFee()
        {
            InitializeComponent();
            this.Text = this.funName;
            dgvFees.AutoGenerateColumns = false;
            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxYears.DisplayMember = "AY_NAME";
            cbxYears.ValueMember = "AY_NAME";
        }

        private void frmTotalFee_Load(object sender, EventArgs e)
        {
            loadAgents();
        }

        private void loadAgents()
        {
            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            cbxAgents.DataSource = data;

            var year = db.AGENT_YEARs.Select(a => a);
            cbxYears.DataSource = year;
        }

        /// <summary>
        /// 费用查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = db.v_REPORTs.Where(a =>a.RI_AYNAME == ((AGENT_YEAR)cbxYears.SelectedItem).AY_NAME && a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME).ToList();
            decimal monthFee = Convert.ToDecimal(txtMonth.Text);
            decimal eachFee = Convert.ToDecimal(txtEach.Text);

            dgvFees.Columns[4].HeaderText = string.Format("每人每月{0}元",monthFee.ToString("F0"));
            dgvFees.Columns[5].HeaderText = string.Format("12万申报每人次{0}元", eachFee.ToString("F0"));

            decimal sum = 0;
            decimal sum1 = 0;
            decimal sum2 = 0;
            foreach (var d in data)
            {

                if (d.RI_MOUNTHCOUNT != 12 || d.RI_SUMS < 120000)
                {
                    dgvFees.Rows.Add(d.AC_NAME, d.TP_NAME, d.RI_MONTH, d.RI_MOUNTHCOUNT, d.RI_MOUNTHCOUNT * monthFee, decimal.Zero, d.RI_MOUNTHCOUNT * monthFee );
                    sum +=  d.RI_MOUNTHCOUNT * monthFee;
                    sum1 += d.RI_MOUNTHCOUNT * monthFee;
                }
                else
                {
                    dgvFees.Rows.Add(d.AC_NAME, d.TP_NAME, d.RI_MONTH, d.RI_MOUNTHCOUNT, d.RI_MOUNTHCOUNT * monthFee, eachFee, d.RI_MOUNTHCOUNT * monthFee + eachFee);
                    sum1 += d.RI_MOUNTHCOUNT * monthFee;
                    sum2 += eachFee;
                    sum += (d.RI_MOUNTHCOUNT * monthFee + eachFee);
                }
            }
            setRowNumber(dgvFees);
            

            dgvFees.Rows.Add("合计", "", "", data.Sum(a => a.RI_MOUNTHCOUNT), sum1, sum2, sum);
        }

        private void setRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        /// <summary>
        /// 文件导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            
            fbdPath.Description = "将导出文件保存到...";
            DialogResult result = fbdPath.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK) return;

            string agentname = ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME;
            int year = ((AGENT_YEAR)cbxYears.SelectedItem).AY_NAME;

            string filename = fbdPath.SelectedPath + string.Format("\\服务费-{0}年度-{1}_{2}.xls", year, agentname, DateTime.Now.ToString("yyyyMMddHHmm"));
            File.WriteAllBytes(filename, TaxDemo.Properties.Resources.T2);

            ExcelHelper excel = new ExcelHelper();

            
            excel.OpenFile(filename, 1);
            excel.SetValue(2, 1, string.Format("{0}{1}年度外派雇员",  agentname,year));
            excel.SetValue(3, 1, string.Format("境内补税和年收入12万元的个税汇算、申报服务费计算明细表"));
            excel.SetValue(4, 6, dgvFees.Columns[4].HeaderText);
            excel.SetValue(4, 7, dgvFees.Columns[5].HeaderText);

            int i = 0;
            for (i = 0; i < dgvFees.Rows.Count; i++)
            {
                
                excel.SetValue(5 + i, 1, (i + 1).ToString());
                excel.SetValue(5 + i, 2, dgvFees.Rows[i].Cells[0].Value.ToString());

                /* Modified by cyq 20160331 */
                /* ***********  Begin  *********/
                if (dgvFees.Rows[i].Cells[1].Value != null)
                {
                    excel.SetValue(5 + i, 3, dgvFees.Rows[i].Cells[1].Value.ToString());
                }
                else
                {
                    excel.SetValue(5 + i, 3, string.Empty);
                }
                /* ***********  End  *********/

                excel.SetValue(5 + i, 4, dgvFees.Rows[i].Cells[2].Value.ToString());
                excel.SetValue(5 + i, 5, dgvFees.Rows[i].Cells[3].Value.ToString());
                excel.SetValue(5 + i, 6, ((decimal)(dgvFees.Rows[i].Cells[4].Value)).ToString("N2"));
                excel.SetValue(5 + i, 7, ((decimal)(dgvFees.Rows[i].Cells[5].Value)).ToString("N2"));
                excel.SetValue(5 + i, 8, ((decimal)(dgvFees.Rows[i].Cells[6].Value)).ToString("N2"));
            }

            excel.SetValue(4 + i, 1, "");

            excel.SetValue(8 + i, 1, "制表：");
            excel.SetValue(9 + i, 1, "确认：");
            excel.SetValue(10 + i, 5, string.Format("制表日期：{0}", dateTimePicker1.Value.ToString("yyyy年MM月dd日")));
            excel.Close();

            MessageBox.Show("导出成功", this.funName);
        }
    }
}
