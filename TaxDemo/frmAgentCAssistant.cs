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
    public partial class frmAgentCAssistant : Form
    {
        private string funName = "公司国别管理";

        private TAXDBDataContext db = new TAXDBDataContext();
        public int ACID;
        public string Flag;

        public frmAgentCAssistant()
        {
            InitializeComponent();

            //数据绑定
            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxTempName.DisplayMember = "TI_NAME";
            cbxTempName.ValueMember = "TI_ID";

            cbxCurrency.DisplayMember = "C_NAME";
            cbxCurrency.ValueMember = "C_ID";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 新建或更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                 MessageBox.Show("请检查输入项", this.funName, MessageBoxButtons.OK);
                return;
            }
            if(this.Flag == "NEW")
            {
                var d = db.AGENT_COUNTRies.SingleOrDefault(a=>(a.AC_AIID==((AGENT_INFO)cbxAgents.SelectedItem).AI_ID && a.AC_NAME==txtCountry.Text.Trim())) ;
                if (d != null)
                {
                    MessageBox.Show("此条记录已存在，请检查");
                    return;
                }

                AGENT_COUNTRY ac = new AGENT_COUNTRY
                {
                    AC_AIID = ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID,
                    AC_ISDEL = false,
                    AC_CURRENCY = ((CURRENCY)cbxCurrency.SelectedItem).C_NAME,
                    AC_NAME = txtCountry.Text.Trim(),
                    AC_TIID = ((TEMPLATE_INFO)cbxTempName.SelectedItem).TI_ID,
                     
                };

                db.AGENT_COUNTRies.InsertOnSubmit(ac);
                db.SubmitChanges();
                MessageBox.Show("添加成功", this.funName, MessageBoxButtons.OK);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                AGENT_COUNTRY ac = db.AGENT_COUNTRies.Where(a => a.AC_ID == ACID).SingleOrDefault();

                ac.AC_CURRENCY = ((CURRENCY)cbxCurrency.SelectedItem).C_NAME;
                ac.AC_NAME = txtCountry.Text.Trim();
                ac.AC_TIID = ((TEMPLATE_INFO)cbxTempName.SelectedItem).TI_ID;
                db.SubmitChanges();
                MessageBox.Show("修改成功", this.funName, MessageBoxButtons.OK);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private bool checkInput()
        {
            return true;
        }

        private void frmAgentCAssistant_Load(object sender, EventArgs e)
        {
            loadAgents(); 
            loadTemps();
            loadCurrency();

            if (this.Flag == "NEW")
            {
    
            }
            else
            {
                v_AgentCountry vac = db.v_AgentCountries.Where(a => a.AC_ID == ACID).SingleOrDefault();
                txtCountry.Text = vac.AC_NAME;
                 
                int x = 0;

                foreach (var a in cbxCurrency.Items)
                {
                    if (((CURRENCY)a).C_NAME == vac.AC_CURRENCY)
                    {
                        cbxCurrency.SelectedIndex = x;

                    }
                    x++;
                }

                x = 0;

                foreach (var a in cbxTempName.Items)
                {
                    if (((TEMPLATE_INFO)a).TI_ID == vac.AC_TIID)
                    {
                        cbxTempName.SelectedIndex = x;

                    }
                    x++;
                }

                x = 0;
                foreach (var a in cbxAgents.Items)
                {
                    if (((AGENT_INFO)a).AI_NAME == vac.AI_NAME)
                    {
                        cbxAgents.SelectedIndex = x;

                    }
                    x++;
                }

            }

            

        }

        /// <summary>
        /// 币种加载
        /// </summary>
        private void loadCurrency()
        {
            cbxCurrency.Items.Clear();
             
            var data = db.CURRENCies.Where(a => a.C_ISDEL == false).Select(a => a);
            foreach (var d in data)
            {

                cbxCurrency.Items.Add(d);
            }
        }

        /// <summary>
        /// 公司列表加载
        /// </summary>
        private void loadAgents()
        {

            cbxAgents.Items.Clear();

            var data = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(a => a);
            foreach (var d in data)
            {

                cbxAgents.Items.Add(d);
            }
        }

        /// <summary>
        /// 模板列表加载
        /// </summary>
        private void loadTemps()
        {
            cbxTempName.Items.Clear();

            var data = db.TEMPLATE_INFOs.Where(a => a.TI_ISVALID == true).Select(a => a);
            foreach (var d in data)
            {

                cbxTempName.Items.Add(d);
            }

        }
    }
}
