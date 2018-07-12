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
    /// 系统表项显示,主要是可用表头添加
    /// </summary>
    public partial class frmTemplateSysAssitant : Form
    {
        public string Flag = "NEW";
        public int ColID = 0;
        private string funName = "系统表项维护";
        private TAXDBDataContext db  = new TAXDBDataContext();

        public frmTemplateSysAssitant()
        {
            InitializeComponent();
            //db = new TAXDBDataContext(Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()));
            //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Flag == "NEW")
            {
                
            }
            else
            { 
                //
            }
        }

        private bool checkInput()
        {
            return true;
        }

        private void frmTemplateSysAssitant_Load(object sender, EventArgs e)
        {
            this.Text = funName;
            if (this.Flag == "UPDATE" && this.ColID != 0) //update or add col alias
            {
                //gbx1.Enabled = false;
                loadColAlias();
                loadCol();
                 
            }
            else
            {
                //txtColName.Text = "SI_";
                gbx2.Enabled = false;

            }
            
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
             //new Col
            gbx2.Enabled = false;
            COLUMNS_ALIA a = new COLUMNS_ALIA{ CA_CIID = this.ColID, CA_ALIAS = txtTitleAlias.Text.Trim()};
            db.COLUMNS_ALIAs.InsertOnSubmit(a);
            db.SubmitChanges();

            MessageBox.Show("添加成功", this.funName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            loadColAlias();
            gbx2.Enabled = true;
        }

        private void loadColAlias()
        {
            lbxAlias.Items.Clear();
            List<string> alias = db.COLUMNS_ALIAs.Where(a => a.CA_CIID == this.ColID).Select(a => a.CA_ALIAS).ToList();
            foreach (string s in alias)
            {
                lbxAlias.Items.Add(s);
            }
        }

        private void loadCol()
        {
            //COLUMNS_INFO ci =  db.COLUMNS_INFOs.Where(a => a.CI_ID == this.ColID).SingleOrDefault();
            //txtName.Text = ci.CI_NAME;
            //txtColName.Text = ci.CI_COLNAME;
            //txtMark.Text = ci.CI_MARK;
            //cbxType.SelectedIndex = ci.CI_TYPEID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
