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
    /// 系统原始表项,也就是薪资表所有可能的列
    /// </summary>
    public partial class frmTemplateSys : Form
    {
        class ColInfo
        {
            public int    CI_ID         { set; get; }
            public string CI_NAME       { set; get; }
            public string CI_COLNAME    { set; get; }
            public string CT_NAME       { set; get; }
            public string CI_MARK       { set; get; }
            public string CI_TITLE      { set; get; }
            public string CI_ALIAS      { set; get; }

        }


        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "系统原始表维护";
         
        public frmTemplateSys()
        {
            InitializeComponent();
            dgvCols.AutoGenerateColumns = false;
        }

        private void frmTemplateSys_Load(object sender, EventArgs e)
        {
            loadCols();
        }

        private void clearInput()
        {
            
        }

        /// <summary>
        /// 加载所有列
        /// </summary>
        private void loadCols()
        {

            var data = db.v_COLUMNs.Where(c => c.CI_ISVALID == true).OrderBy(a=>a.CI_ID).Select(cl => cl);
            var ds = new BindingList<ColInfo>();
            foreach (var d in data)
            {
                ColInfo ci = new ColInfo{
               
                    CI_ID = d.CI_ID,
                    CI_COLNAME  = d.CI_COLNAME,
                    CI_MARK = d.CI_MARK,
                    CI_ALIAS = string.Join(",", db.COLUMNS_ALIAs.Where(c => c.CA_CIID == d.CI_ID).Select(a => a.CA_ALIAS).ToList()),
                    CI_TITLE = d.CI_NAME,
                    CI_NAME = d.CI_NAME,
                    CT_NAME = SysUtil.GetTypeName(d.CI_TYPEID),
                };

                ds.Add(ci);

            }

            dgvCols.DataSource = ds;

        }

        private bool checkInput()
        {
            //need to check is colname is avaliable
            return true;
        }

        /// <summary>
        /// 删除1条列数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {

            if (dgvCols.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一条记录后进行操作", funName, MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("确定删除此列吗", funName, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int aid = (int)dgvCols.SelectedRows[0].Cells[0].Value;
                var agent = db.COLUMNS_INFOs.Where(ai => ai.CI_ID == aid).Select(a => a).Single();
                agent.CI_ISVALID = false;
                db.SubmitChanges();
                MessageBox.Show("删除成功", funName, MessageBoxButtons.OK);
                loadCols();
            }
        }

       
        private void btnNew_Click(object sender, EventArgs e)
        {
            using (frmTemplateSysAssitant fa = new frmTemplateSysAssitant())
            {
                fa.Flag = "NEW";
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                { 
                    MessageBox.Show("新增成功", this.funName, MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// 修改列信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgvCols.SelectedRows.Count!=1)
            {
                MessageBox.Show("请选择一条记录后进行操作",this.funName,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            using (frmTemplateSysAssitant fa = new frmTemplateSysAssitant())
            {
                fa.Flag = "UPDATE";
                fa.ColID = (int)dgvCols.SelectedRows[0].Cells[0].Value;  // fa.TaxPlayerID = (int)dgvPlayers.SelectedRows[0].Cells[0].Value;
                if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadCols();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                MessageBox.Show("请检查输入项", this.funName, MessageBoxButtons.OK);
                return;
            }

            COLUMNS_INFO ci = new COLUMNS_INFO
            {
                CI_COLNAME = txtColName.Text.Trim().Replace(" ",string.Empty),
                CI_ISVALID = true,
                CI_MARK = txtMark.Text.Trim(),
                CI_TYPEID = (short)cbxType.SelectedIndex,
                CI_NAME = txtName.Text.Trim().Replace(" ", string.Empty),
            };

            db.COLUMNS_INFOs.InsertOnSubmit(ci);
            db.SubmitChanges();

            ColumnsDAL cd = new ColumnsDAL();

            cd.OpenConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TaxDemo.Properties.Settings.testdbConnectionString"].ConnectionString);
            
            cd.InsertCol(ci);
            cd.CloseConnection();

            MessageBox.Show("添加成功", this.funName, MessageBoxButtons.OK);
            loadCols();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
