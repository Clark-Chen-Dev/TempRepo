using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCalc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TaxDemo
{
    /// <summary>
    /// 不再使用
    /// </summary>
    public partial class frmTemplateAgentCountry : Form
    {

        public frmTemplateAgentCountry()
        {
            InitializeComponent();
        }

        private void frmTemplateAgentCountry_Load(object sender, EventArgs e)
        {
            cbxUseParent.Checked = false;

        }

        private void cbxUseParent_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUseParent.Checked)
            {
                gbxTep.Enabled = false;
                gbxF.Enabled = false;

            }
            else
            {
                gbxTep.Enabled = true;
                gbxF.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string test = "A+B-(C-D)+Abs(E)";
            if (FormulaC.IsValid(test))
            {
                string x = FormulaC.GetFormula(test);
            }

        }

    }
}
