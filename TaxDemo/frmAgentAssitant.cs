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
    public partial class frmAgentAssitant : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "公司信息管理";
        public string Flag;
        public int AgentID;
        public frmAgentAssitant()
        {
            InitializeComponent();
            cbxTempName.DisplayMember = "TI_NAME";
            cbxTempName.ValueMember = "TI_ID";
        }

        private void benNew_Click(object sender, EventArgs e)
        {
            if (!checkInput() )
            {
                MessageBox.Show("请检查输入项", this.funName, MessageBoxButtons.OK);
            }

            //新建
            if(this.Flag == "NEW")
            {
                AGENT_INFO ai = new AGENT_INFO
                {
                    AI_NAME = txtName.Text.Trim(),
                    AI_ADDRESS = txtAddress.Text.Trim(),
                    AI_INDUSTRY = txtIndustry.Text.Trim(),
                    AI_POSTCODE = txtPostCode.Text.Trim(),
                    AI_TAXCODEID = txtTaxCodeID.Text.Trim(),
                    AI_TAXID = txtTAXID.Text.Trim(),
                    AI_TAXNAME = txtTaxName.Text.Trim(),
                    AI_TEL = txtTel.Text.Trim(),
                    AI_ISDEL = false,
                    
                };
                if (cbxTempName.SelectedIndex!=-1)
                {
                    ai.AI_TIID = ((TEMPLATE_INFO)cbxTempName.SelectedItem).TI_ID;
                }

                db.AGENT_INFOs.InsertOnSubmit(ai);
                db.SubmitChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                MessageBox.Show("添加成功");
               
            }

            else //修改
            {
                AGENT_INFO ai = db.AGENT_INFOs.Where(a => a.AI_ID == this.AgentID).SingleOrDefault();
                if (ai != null)
                { 
                    ai.AI_NAME = txtName.Text.Trim();
                    ai.AI_ADDRESS = txtAddress.Text.Trim();
                    ai.AI_INDUSTRY = txtIndustry.Text.Trim();
                    ai.AI_POSTCODE = txtPostCode.Text.Trim();
                    ai.AI_TAXCODEID = txtTaxCodeID.Text.Trim();
                    ai.AI_TAXID = txtTAXID.Text.Trim();
                    ai.AI_TAXNAME = txtTaxName.Text.Trim();
                    ai.AI_TEL = txtTel.Text.Trim();
                    ai.AI_ISDEL = false;
                    if(cbxTempName.SelectedIndex!=-1)
                    {
                        ai.AI_TIID = ((TEMPLATE_INFO)cbxTempName.SelectedItem).TI_ID;
                    }
                    db.SubmitChanges();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    MessageBox.Show("修改成功");
                }
            }
        }

        private bool checkInput()
        {
            return true;
        }

        private void frmAgentAssitant_Load(object sender, EventArgs e)
        {
            loadTemps();

            if (this.Flag == "NEW")
            {

            }
            else
            {
                var data = db.AGENT_INFOs.Where(a => a.AI_ID == this.AgentID).SingleOrDefault();
                txtAddress.Text = data.AI_ADDRESS;
                txtIndustry.Text = data.AI_INDUSTRY;
                txtName.Text = data.AI_NAME;
                txtPostCode.Text = data.AI_POSTCODE;
                txtTaxCodeID.Text = data.AI_TAXCODEID;
                txtTAXID.Text = data.AI_TAXCODEID;
                txtTaxName.Text = data.AI_TAXNAME;
                txtTel.Text = data.AI_TEL;

                int x = 0;
                foreach(var a in cbxTempName.Items)
                {
                    if (((TEMPLATE_INFO)a).TI_ID == data.AI_TIID)
                    { 
                        cbxTempName.SelectedIndex = x;
                       
                    }
                    x++;    
                }

            }
        }

        /// <summary>
        /// 加载系统中模板列表
        /// </summary>
        private void loadTemps()
        {
            cbxTempName.Items.Clear();

            var data =  db.TEMPLATE_INFOs.Where(a => a.TI_ISVALID == true).Select(a => a);
            foreach (var d in data)
            {
                
                cbxTempName.Items.Add(d);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
