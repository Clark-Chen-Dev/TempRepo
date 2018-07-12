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
    /// 年度与货币维护
    /// </summary>
    public partial class frmYear : Form
    {
        private string funName = "其它参数维护";
        public frmYear()
        {
            InitializeComponent();
            this.Text = funName;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addYear();
        }

       

        private void frmYear_Load(object sender, EventArgs e)
        {
            dgvYear.AutoGenerateColumns = false;
            dgvCurrency.AutoGenerateColumns = false;
            loadYears();
            loadCurrency();
           
        }

        private void loadCurrency()
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                dgvCurrency.DataSource = db.v_CURRENCies.Select(a => a);
            }
        }

        
        /// <summary>
        /// 设置为当前年度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetcurrent_Click(object sender, EventArgs e)
        {
            SetcurrentYear();
        }

        /// <summary>
        /// 新增年度
        /// </summary>
        private void addYear()
        {
            if(!checkInput())
            {
                MessageBox.Show("请检查输入项",this.funName);
                return;
            }
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                AGENT_YEAR ay = new AGENT_YEAR
                {
                    AY_NAME = Convert.ToInt32( txtYear.Text.Trim()),
                    AY_ADDTIME = DateTime.Now,
                    AY_UIID = SysUtil.CurrentUserID(),

                };
                db.AGENT_YEARs.InsertOnSubmit(ay);
                db.SubmitChanges();
                loadYears();
            }
        }

        private bool checkInput()
        {
            return true;
        }

        /// <summary>
        /// 加载年度
        /// </summary>
        private void loadYears()
        {
            dgvYear.Rows.Clear();

            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                
                var data =  db.v_AgentYears.Select(a => a);
                foreach (var d in data)
                {
                    dgvYear.Rows.Add(d.AY_ID,d.AY_NAME.ToString(), d.AY_ISCURRENT, d.UI_NAME, d.AY_ADDTIME);
                }
            }
        }

        /// <summary>
        /// 设置为当前年度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetcurrentYear(object sender, EventArgs e)
        {
            if(dgvYear.SelectedRows.Count!=1)
            {
                MessageBox.Show("请选择一行后进行操作", this.funName);
                return;
            }
            
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                AGENT_YEAR old = db.AGENT_YEARs.SingleOrDefault(a => a.AY_ISCURRENT == true);
                if (old != null)
                {
                    old.AY_ISCURRENT = false;
                }
                db.SubmitChanges();
                
                AGENT_YEAR ay = db.AGENT_YEARs.SingleOrDefault(a => a.AY_ID == (int)dgvYear.SelectedRows[0].Cells[0].Value);
                ay.AY_ISCURRENT = true;
                db.SubmitChanges();
                loadYears();
                MessageBox.Show("修改成功", this.funName);
            }
        }

        private void dgvYear_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    //e.Value = (value) ? "是" : "否";
                    if (value)
                    {
                        //e.CellStyle.ForeColor = Color.Red;
                        dgvYear.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        
                    }
                    e.FormattingApplied = true;
                     
                }
            }
        }

        /// <summary>
        /// 增加货币单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCurr_Click(object sender, EventArgs e)
        {
            if (!checkInputOfCurrency())
            {
                MessageBox.Show("请检查输入项");
                return;
            }
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
                CURRENCY curr = new CURRENCY
                {
                    C_ADDTIME = DateTime.Now,
                    C_NAME = txtCurrency.Text.Trim(),
                    C_UIID = SysUtil.CurrentUserID(),
                    C_ISDEL = false
                };
                db.CURRENCies.InsertOnSubmit(curr);
                db.SubmitChanges();
                loadCurrency();
                MessageBox.Show("添加成功"); 
            }
        }

        private bool checkInputOfCurrency()
        {
            if (txtCurrency.Text.Trim() == "") return false;
            else
            {
                return true;
            }
        }

        private void SetcurrentYear()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
