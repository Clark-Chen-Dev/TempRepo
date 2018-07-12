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
    public partial class frmPlayerAssitant : Form
    {
        public string Flag = "NEW";
        public int TaxPlayerID;
        private string funName = "纳税人管理";
        private TAXDBDataContext db = new TAXDBDataContext();

        public frmPlayerAssitant()
        {
            InitializeComponent();

            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";
            cbxAgentCs.DisplayMember = "AC_NAME";
            cbxAgentCs.ValueMember = "AC_ID";
        }

        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkInput() != true)
            {
                MessageBox.Show("请检查输入项",this.funName, MessageBoxButtons.OK);
                return;
            }
            if (this.Flag == "NEW") //新建
            {
                TAX_PLAYER t = db.TAX_PLAYERs.SingleOrDefault(a => (a.TP_ACID == ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID) && (a.TP_NAME == txtName.Text.Trim()));
                if (t != null)
                {
                    MessageBox.Show(this.funName, "此人已在该国别中存在，请检查",MessageBoxButtons.OK);
                    return;
                }
                TAX_PLAYER tp = new TAX_PLAYER
                {
                    TP_NAME = txtName.Text.Trim(),
                    TP_ENNAME = txtEnName.Text.Trim(),
                    TP_IDNUMBER = txtID.Text.Trim(),
                    TP_AIID = (int)cbxAgents.SelectedValue,
                    TP_ACID = ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID
                };
                db.TAX_PLAYERs.InsertOnSubmit(tp);
            }
            else if (this.Flag == "UPDATE") //修改
            {
                TAX_PLAYER tp =  db.TAX_PLAYERs.Single(a => a.TP_ID == this.TaxPlayerID);
                if (tp != null)
                { 
                    tp.TP_NAME = txtName.Text.Trim();
                    tp.TP_ENNAME = txtEnName.Text.Trim();
                    tp.TP_IDNUMBER = txtID.Text.Trim();
                    tp.TP_AIID = (int)cbxAgents.SelectedValue;
                    tp.TP_ACID = ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID;
                }
            }
            db.SubmitChanges();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private bool checkInput()
        {
            return true;
        }

        private void frmPlayerAssitant_Load(object sender, EventArgs e)
        {
            loadAgents();
            this.Text = funName;
            if (this.Flag=="UPDATE" && this.TaxPlayerID != null)
            {
                btnUpdate.Text = "更改";
                TAX_PLAYER tp =  db.TAX_PLAYERs.Single(a => a.TP_ID == this.TaxPlayerID);
                if (tp != null)
                { 
                    txtName.Text = tp.TP_NAME;
                    txtEnName.Text = tp.TP_ENNAME;
                    txtID.Text = tp.TP_IDNUMBER;
                    cbxAgents.SelectedValue = tp.TP_AIID;
                    
                }
            }
            
        }

        private void loadAgents()
        {
            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
            cbxAgents.DataSource = data;
        }

        private void loadAgentCs(int agentID)
        {
            cbxAgentCs.Items.Clear();

            var data = db.AGENT_COUNTRies.Where(a => a.AC_AIID == agentID).Select(p => p);
            foreach (var d in data)
            {
                cbxAgentCs.Items.Add(d);
            }
            if (data.Count() >= 1)
            {
                cbxAgentCs.SelectedIndex = 0;
            }
        }


        private void frmPlayerAssitant_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
        }

        private void cbxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAgentCs((int)cbxAgents.SelectedValue);
        }
    }
}
