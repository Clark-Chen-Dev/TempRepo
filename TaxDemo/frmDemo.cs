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
    public partial class frmDemo : Form
    {
        public frmDemo()
        {
            InitializeComponent();
        }

        private void frmDemo_Load(object sender, EventArgs e)
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = System.Drawing.Color.Transparent;
        }

        private void btncanel_Click(object sender, EventArgs e)
        {

        }

        private bool TestConnection()
        {

            return true;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (TestConnection())
            {
                try
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to load schema.\n" + ex.Message, "Error to load schema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
