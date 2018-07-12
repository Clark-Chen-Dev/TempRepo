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
    /// 税率等级管理
    /// </summary>
    public partial class frmTaxRate : Form
    {
        private string funName = "税率管理";
        private TAXDBDataContext db = new TAXDBDataContext();

        public frmTaxRate()
        {
            InitializeComponent();
        }

        private void frmTaxRate_Load(object sender, EventArgs e)
        {
            dgvRates.Rows.Clear();
            var data = db.TAX_RATEs.OrderBy(a => a.TR_ID).Select(tr=>tr).ToList();

            foreach (var d in data)
            {
                dgvRates.Rows.Add(d.TR_NAME, string.Format("{0}-{1}", d.TR_LOW, d.TR_HIGH), string.Format("{0}%", d.TR_RATE), d.TR_QUICH);
            }
        }
    }
}
