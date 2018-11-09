using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace TaxDemo
{
    /// <summary>
    /// 个税申报汇总
    /// </summary>
    public partial class frmTotalDetails : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "12万个人所得税申报情况";

        public frmTotalDetails()
        {
            InitializeComponent();
            dgvDetails.AutoGenerateColumns = false;
            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxYears.DisplayMember = "AY_NAME";
            cbxYears.ValueMember = "AY_NAME";
        }

        private void frmTotalDetails_Load(object sender, EventArgs e)
        {
            loadAgents();
            this.Text = this.funName;
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
        /// 个税申报查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

            int yearname = ((AGENT_YEAR)cbxYears.SelectedItem).AY_NAME;
            dgvDetails.Rows.Clear();

            // Modified by CYQ on 2018-09-14
            // 根据财通嘉鑫要求，去除应纳税所得额 >= 120000的条件限制
            // 并修改为：>= 12个月时，“12万申报”列显示已申报
            //           < 12 个月时，“12万申报”列显示为横杠
            // Begin
            //var data = db.v_REPORTs.Where(a => a.RI_MOUNTHCOUNT == 12 && a.RI_SUMTAXSALARY >= 120000 && a.RI_AYNAME ==yearname && a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME).ToList();
            //foreach (var d in data)
            //{
            //    dgvDetails.Rows.Add(d.AC_NAME, d.TP_NAME, d.RI_SUMTAXSALARY, d.RI_SUMTAXRMB, d.RI_SUMTAXALREADYRMB + d.RI_USEALL, (decimal)d.RI_NEEDPACKREAL,"已申报");

            //}

            var data = db.v_REPORTs.Where(a => a.RI_AYNAME == yearname && a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME).ToList();
            foreach (var d in data)
            {
                if (d.RI_MOUNTHCOUNT >= 12)
                {
                    dgvDetails.Rows.Add(d.AC_NAME, d.TP_NAME, d.RI_SUMTAXSALARY, d.RI_SUMTAXRMB, d.RI_SUMTAXALREADYRMB + d.RI_USEALL, (decimal)d.RI_NEEDPACKREAL, "已申报");
                }
                else
                {
                    dgvDetails.Rows.Add(d.AC_NAME, d.TP_NAME, d.RI_SUMTAXSALARY, d.RI_SUMTAXRMB, d.RI_SUMTAXALREADYRMB + d.RI_USEALL, (decimal)d.RI_NEEDPACKREAL, "-");
                }

            }
            // End

            lblMsg.Text = string.Format("注：{0}年度外派上述国家{1}人，境外收入境内应补缴税款{2}元人民币", yearname, data.Count, ((decimal)data.Sum(r => r.RI_NEEDPACKREAL)).ToString("N2"));

            setRowNumber(dgvDetails);
        }

        private void setRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxYears_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 数据导出
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

            string filename = fbdPath.SelectedPath + string.Format("\\{0}年度-{1}_{2}.xls", year, agentname, DateTime.Now.ToString("yyyyMMddHHmm"));
            File.WriteAllBytes(filename, TaxDemo.Properties.Resources.T1);

            ExcelHelper excel = new ExcelHelper();

            //
            excel.OpenFile(filename, 1);
            excel.SetValue(1, 1, string.Format("{0}年度{1}外派雇员境外收入境内补税及年收入12万元个人所得税申报情况表", year, agentname));
            int i = 0;
            for (i = 0; i < dgvDetails.Rows.Count; i++)
            {
                excel.SetValue(5 + i, 1, (i + 1).ToString());
                excel.SetValue(5 + i, 2, dgvDetails.Rows[i].Cells[0].Value.ToString());

                /* Modified by cyq 20160331 */
                /* ***********  Begin  *********/
                if (dgvDetails.Rows[i].Cells[1].Value != null)
                {
                    excel.SetValue(5 + i, 3, dgvDetails.Rows[i].Cells[1].Value.ToString());
                }
                else
                {
                    excel.SetValue(5 + i, 3, string.Empty);
                }

                /* ***********  End  *********/

                excel.SetValue(5 + i, 4, ((decimal)(dgvDetails.Rows[i].Cells[2].Value)).ToString("N2"));
                excel.SetValue(5 + i, 5, ((decimal)(dgvDetails.Rows[i].Cells[3].Value)).ToString("N2"));
                excel.SetValue(5 + i, 6, ((decimal)(dgvDetails.Rows[i].Cells[4].Value)).ToString("N2"));
                excel.SetValue(5 + i, 7, ((decimal)(dgvDetails.Rows[i].Cells[5].Value)).ToString("N2"));
                excel.SetValue(5 + i, 8, dgvDetails.Rows[i].Cells[6].Value.ToString());
            }

            excel.SetValue(6 + i, 1, lblMsg.Text);
            excel.SetValue(8 + i, 1, "制表：");
            excel.SetValue(8 + i, 7, string.Format("制表日期：{0}", dateTimePicker1.Value.ToString("yyyy年MM月dd日")));
            excel.SetValue(9 + i, 1, "确认：");

            excel.Close();
            MessageBox.Show("导出成功", this.funName);
        }
    }
}
