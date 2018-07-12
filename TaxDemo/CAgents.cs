using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaxDemo
{
    public partial class CAgents : UserControl
    {

        public CAgents()
        {
            InitializeComponent();

            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxAgentCs.DisplayMember = "AC_NAME";
            cbxAgentCs.ValueMember = "AC_ID";
        }

        private void CAgents_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadData()
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());

                cbxAgents.Items.Clear();

                var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
                cbxAgents.DataSource = data;
            }
        }

        private void cbxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());

                cbxAgentCs.Items.Clear();

                var data = db.AGENT_COUNTRies.Where(a => a.AC_AIID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID).Select(p => p);
                foreach (var d in data)
                {
                    cbxAgentCs.Items.Add(d);
                }

            }
        }
    }
}
