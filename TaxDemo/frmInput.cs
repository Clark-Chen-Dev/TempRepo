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
    /// 每月减费用额修改
    /// </summary>
    public partial class frmInput : Form
    {
        
        public List<string> monthList;
        public Dictionary<string, decimal> minusList;
        public frmInput()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            minusList = new Dictionary<string,decimal>();
            for (int i = 0; i < dgvMinusList.Rows.Count; i++)
            { 
                minusList.Add(dgvMinusList.Rows[i].Cells[0].Value.ToString(),Convert.ToDecimal(dgvMinusList.Rows[i].Cells[1].Value.ToString()));
            }
            this.DialogResult = DialogResult.OK;
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            dgvMinusList.Rows.Clear();
            dgvMinusList.Columns[0].ReadOnly = true;
            foreach (string s in monthList)
            {
                dgvMinusList.Rows.Add(s, 4800);
            }
        }

    }
}
