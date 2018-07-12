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
    /// 留抵记录展示
    /// </summary>
    public partial class frmCredit : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "留抵记录";

        public int TPID=0;
        public int CURRENTYEAR = 0;
        public frmCredit()
        {
            InitializeComponent();
            dgvCredit.AutoGenerateColumns = false;
        }

        private void frmCredit_Load(object sender, EventArgs e)
        {
            if (TPID != 0)
            { 
                var tp = db.TAX_PLAYERs.SingleOrDefault(a=>a.TP_ID == TPID);
                lblInfo.Text = tp.TP_NAME;

                var data = db.v_TAXCREDITs.Where(a => (a.TP_ID == TPID) && ((CURRENTYEAR - a.TC_YEAR) <= 5)).Select(l=>l).ToList();
                dgvCredit.DataSource = data;
                lblSum.Text = string.Format("总计可用留抵额：{0}条，{1}元",data.Count(), data.Sum(a => a.TC_CREDITBALANCE));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
