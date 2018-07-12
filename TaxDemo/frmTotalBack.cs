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
    /// 倒推表查询,导出
    /// </summary>
    public partial class frmTotalBack : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "倒推表";

        public frmTotalBack()
        {
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxYears.DisplayMember = "AY_NAME";
            cbxYears.ValueMember = "AY_NAME";
        }

        private void frmTotalBack_Load(object sender, EventArgs e)
        {
            loadAgents();
            this.Text = this.funName;

        }

        /// <summary>
        /// 公司加载
        /// </summary>
        private void loadAgents()
        {
            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            cbxAgents.DataSource = data;

            var year = db.AGENT_YEARs.Select(a => a);
            cbxYears.DataSource = year;
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="dgv"></param>
        private void setRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        /// <summary>
        /// 查询符合条件的倒推数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var cul = CultureInfo.GetCultureInfo("en-US");
            var data = db.v_REPORTs.Where(a => a.RI_NEEDPACKREAL > 0 && a.RI_AYNAME == ((AGENT_YEAR)cbxYears.SelectedItem).AY_NAME && a.AI_NAME == ((AGENT_INFO)cbxAgents.SelectedItem).AI_NAME).ToList();

            //汇总信息
            lblMsg.Text = string.Format("注：{0}年度外派人员{1}人须在国内补缴税款计： {2}元", ((AGENT_YEAR)cbxYears.SelectedItem).AY_NAME, data.Count, ((decimal)data.Sum(a => a.RI_NEEDPACKREAL)).ToString("N2"));

            decimal sum = 0;

            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            dgvItems.Rows.Clear();
            /* ***********  End  *********/

            foreach (var d in data)
            {

                dgvItems.Rows.Add(d.AC_NAME, d.TP_NAME, d.RI_BACKSUMSALARY, d.RI_BACKMINUS, d.RI_BACKSUMTAXSALARY, string.Format("{0}%", d.RI_BACKTAXRATE), d.RI_BACKTAXQUICK, d.RI_NEEDPACKREAL);
                sum += (decimal)d.RI_NEEDPACKREAL;

            }
            setRowNumber(dgvItems);
            dgvItems.Rows.Add("合计", "", "", "", "", "", "", sum);
        }

        /// <summary>
        /// 导出
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

            string filename = fbdPath.SelectedPath + string.Format("\\倒退表-{0}年度-{1}_{2}.xls", year, agentname, DateTime.Now.ToString("yyyyMMddHHmm"));
            File.WriteAllBytes(filename, TaxDemo.Properties.Resources.T3);

            ExcelHelper excel = new ExcelHelper();


            excel.OpenFile(filename, 1);
            excel.SetValue(1, 2, string.Format("{0}年度{1}外派雇员境外收入境内补缴明细", year, agentname));


            int i = 0;
            for (i = 0; i < dgvItems.Rows.Count - 1; i++)
            {

                excel.SetValue(4 + i, 1, (i + 1).ToString());
                excel.SetValue(4 + i, 2, dgvItems.Rows[i].Cells[0].Value.ToString());

                /* Modified by cyq 20160331 */
                /* ***********  Begin  *********/
                if (dgvItems.Rows[i].Cells[1].Value != null)
                    excel.SetValue(4 + i, 3, dgvItems.Rows[i].Cells[1].Value.ToString());
                else
                    excel.SetValue(4 + i, 3, string.Empty);
                /* ***********  End  *********/

                excel.SetValue(4 + i, 4, ((decimal)(dgvItems.Rows[i].Cells[2].Value)).ToString("N2"));
                excel.SetValue(4 + i, 5, ((decimal)(dgvItems.Rows[i].Cells[3].Value)).ToString("N2"));
                excel.SetValue(4 + i, 6, ((decimal)(dgvItems.Rows[i].Cells[4].Value)).ToString("N2"));
                excel.SetValue(4 + i, 7, dgvItems.Rows[i].Cells[5].Value.ToString());
                excel.SetValue(4 + i, 8, ((int)(dgvItems.Rows[i].Cells[6].Value)).ToString("N2"));
                excel.SetValue(4 + i, 9, ((decimal)(dgvItems.Rows[i].Cells[7].Value)).ToString("N2"));
            }

            excel.SetValue(4 + i, 2, "合计：");
            excel.SetValue(4 + i, 10, ((decimal)(dgvItems.Rows[i].Cells[7].Value)).ToString("N2"));
            excel.SetValue(6 + i, 2, lblMsg.Text);

            excel.SetValue(8 + i, 2, "制表：");
            excel.SetValue(8 + i, 9, dateTimePicker1.Value.ToString("yyyy年MM月dd日"));
            excel.SetValue(9 + i, 2, "审核：");
            excel.SetValue(9 + i, 9, "确认：");

            excel.Close();

            MessageBox.Show("导出成功", this.funName);
        }
    }
}
