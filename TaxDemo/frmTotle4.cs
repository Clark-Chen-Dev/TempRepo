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
    /// 申报大表
    /// </summary>
    public partial class frmTotal4 : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "申报大表";

        public frmTotal4()
        {
            InitializeComponent();
            this.Text = funName;
            //dgvCredit.AutoGenerateColumns = false;

            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";
        }

        private void frmTotle4_Load(object sender, EventArgs e)
        {
            

            loadAgents();
            loadYears();


        }
        private void loadAgents()
        {
            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            cbxAgents.DataSource = data;


        }

        private void loadYears()
        {
            //cbxAgents.Items.Clear();

            //var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            //cbxAgents.DataSource = data;

            cbxYears.Items.Clear();
            var data = db.AGENT_YEARs.Select(a => a);
            cbxYears.DataSource = data;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
