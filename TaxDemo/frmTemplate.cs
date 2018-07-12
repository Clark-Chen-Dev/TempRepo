using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace TaxDemo
{
    /// <summary>
    /// 导入模板管理
    /// </summary>
    public partial class frmTemplate : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "模板管理";

        public frmTemplate()
        {
            InitializeComponent();
        }

        private void frmTemplate_Load(object sender, EventArgs e)
        {
            dgvTemplates.AutoGenerateColumns = false;
            loadTemplates();
        }

        /// <summary>
        /// 加载模板列表
        /// </summary>
        private void loadTemplates()
        {
            var data = db.TEMPLATE_INFOs.Where(a => a.TI_ISVALID == true).Select(t => t);
            dgvTemplates.DataSource = data;
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            using (frmTemplateAssitant fa = new frmTemplateAssitant())
            {
                fa.flag = "NEW";
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadTemplates();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 更改模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTemplates.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一条记录后进行修改操作",this.funName,MessageBoxButtons.OK);
                return;
            }
            using (frmTemplateAssitant fa = new frmTemplateAssitant())
            {
                fa.flag = "UPDATE";
                fa.tiidtmp = (int)dgvTemplates.SelectedRows[0].Cells[0].Value;
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadTemplates();
                }
            }
        }

        /// <summary>
        /// 导出模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dgvTemplates.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一条记录后再进行导出操作", this.funName, MessageBoxButtons.OK);
                return;
            }
            int tmpID = (int)dgvTemplates.SelectedRows[0].Cells[0].Value;
            
            var tmp = db.TEMPLATE_INFOs.SingleOrDefault(a => a.TI_ID == tmpID);
            savefile.FileName = tmp.TI_NAME + ".xlsx";
            savefile.Filter = "Microsoft Excel files (*.xls)|*.xls;*.xlsx";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                ExcelHelper excel = new ExcelHelper();
                excel.NewFile();
                
                excel.AddRow(1, tmp.TI_COLHEADERINORDER.Split(',').ToList());

                excel.SaveAs(savefile.FileName);
                excel.Close();

                if (cbxOpen.Checked)
                {
                    System.Diagnostics.Process.Start(savefile.FileName);
                }

            }

        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvTemplates.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一条记录后进行操作", funName, MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("确定删除此列吗", funName, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int tid = (int)dgvTemplates.SelectedRows[0].Cells[0].Value;
                TEMPLATE_INFO lti = db.TEMPLATE_INFOs.SingleOrDefault(t => t.TI_ID == tid); //TODO ,should be single or default
                lti.TI_ISVALID = false;
                List<TEMPLATE_COLUMN> lTC = db.TEMPLATE_COLUMNs.Where(tc => tc.TC_TIID == lti.TI_ID).Select(a => a).ToList();
                foreach (var detail in lTC)
                {
                    db.TEMPLATE_COLUMNs.DeleteOnSubmit(detail);
                }
                db.SubmitChanges();
                MessageBox.Show("删除成功", funName, MessageBoxButtons.OK);
                loadTemplates();
            }
        }

        /// <summary>
        /// 导出已纳税额导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaxTmp_Click(object sender, EventArgs e)
        {
            savefile.FileName = "已纳税额导入模板.xlsx";
            savefile.Filter = "Microsoft Excel files (*.xls)|*.xls;*.xlsx";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string filename = savefile.FileName;
                File.WriteAllBytes(filename, TaxDemo.Properties.Resources.Tax);
            }

        }

        /// <summary>
        /// 导出员工导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayerTmp_Click(object sender, EventArgs e)
        {
            savefile.FileName = "员工导入模板.xlsx";
            savefile.Filter = "Microsoft Excel files (*.xls)|*.xls;*.xlsx";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string filename = savefile.FileName;
                File.WriteAllBytes(filename, TaxDemo.Properties.Resources.tpalyer);
            }
        }

        /// <summary>
        /// 导出留抵税额导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            savefile.FileName = "留抵税额导入模板.xlsx";
            savefile.Filter = "Microsoft Excel files (*.xls)|*.xls;*.xlsx";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string filename = savefile.FileName;
                File.WriteAllBytes(filename, TaxDemo.Properties.Resources.TaxCredit);
            }

        }
    }
}
