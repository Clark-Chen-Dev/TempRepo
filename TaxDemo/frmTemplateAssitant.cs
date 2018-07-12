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
    /// 模板显示与调整
    /// </summary>
    public partial class frmTemplateAssitant : Form
    {
        private class COLINFO
        {
            public int CI_ID { get; set; }
            public string CI_NAME { get; set; }
            public string CI_COLNAME { get; set; }

            public override string ToString()
            {
                if (CI_NAME.IndexOf('[') == 0)
                {
                    return CI_ID.ToString() + ";" + CI_NAME.Substring(CI_NAME.IndexOf("]")+1) + ";" + CI_COLNAME;
                }
                else
                {
                    return CI_ID.ToString() + ";" + CI_NAME + ";" + CI_COLNAME;
                }
            }
        }

        private string funName = "公司模板管理";
        private TAXDBDataContext db = new TAXDBDataContext();
        public int tiidtmp;
        public string flag;
        public frmTemplateAssitant()
        {
            InitializeComponent();
            lbxColsLeft.ValueMember = "CI_ID";
            lbxColsLeft.DisplayMember = "CI_NAME";

            lbxColsRight.ValueMember = "CI_ID";
            lbxColsRight.DisplayMember = "CI_NAME";
        }

        private void frmTemplateAgent_Load(object sender, EventArgs e)
        {
            loadTemplateInfo();
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void ClearCols()
        {
            lbxColsLeft.Items.Clear();
            lbxColsRight.Items.Clear();

            var data = db.COLUMNS_INFOs.Select(ci => new COLINFO { CI_ID = ci.CI_ID, CI_NAME = ci.CI_NAME , CI_COLNAME = ci.CI_COLNAME}).ToList();

            foreach (var i in data)
            {
                lbxColsLeft.Items.Add(i);
            }
        }

        private void cmbAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 显示空模板或加载模板信息
        /// </summary>
        private void loadTemplateInfo()
        {
            lbxColsLeft.Items.Clear();
            lbxColsRight.Items.Clear();

            if (this.flag == "NEW")
            {
                var data = db.COLUMNS_INFOs.Where(a=>a.CI_ISVALID==true).Select(ci => new COLINFO { CI_ID = ci.CI_ID, CI_NAME = ci.CI_NAME, CI_COLNAME = ci.CI_COLNAME }).ToList();
                
                foreach (var i in data)
                {
                    lbxColsLeft.Items.Add(i);
                }
            }
            else
            {
                var ti = db.TEMPLATE_INFOs.Where(a => a.TI_ID == this.tiidtmp).SingleOrDefault();

                //所有列与已配置列
                var colall = db.COLUMNS_INFOs.Where(a=>a.CI_ISVALID==true).Select(ci => new COLINFO  { CI_ID = ci.CI_ID, CI_NAME = ci.CI_NAME }).ToList();
                var colright = db.v_TEMPLATECOLs.Where(tc => (tc.TC_TIID == this.tiidtmp)).OrderBy(tcc=>tcc.TC_ORDER).Select(c => new COLINFO { CI_ID = c.CI_ID, CI_NAME = c.CI_NAME, CI_COLNAME = c.CI_COLNAME}).ToList();
                
                
                txtTempName.Text = ti.TI_NAME;
                txtSalF.Text = ti.TI_SALFRAW;
                txtBonusCol.Text = ti.TI_BONUSCOL;
                txtNameCol.Text = ti.TI_NAMECOL;
                txtMonthCol.Text = ti.TI_MONTHCOL;
                txtRealCol.Text = ti.TI_REALCOL;
                txtVerify.Text = ti.TI_VERFYFRAW;
                txtYearAnnu.Text = ti.TI_ANNUCOL;

                cbxHeaderRow.SelectedIndex = (int)ti.TI_HEARDERROW - 1;
                cbxDataRow.SelectedIndex = (int)ti.TI_DATAROW - 1;

                foreach (var i in colall)
                {
                    if (!colright.Any(cr=>(cr.CI_ID==i.CI_ID)))
                    {
                        lbxColsLeft.Items.Add(i);
                    }
                    
                    
                }

                foreach (var i in colright)
                {
                    lbxColsRight.Items.Add(i);
                }
                UpdatePrex(); //已配置列添加EXCEL列号
                btnUpdate.Text = "修改";
            }
        }

        //添加EXCEL列号(A,B,C...)
        private void UpdatePrex()
        { 
            int i = 0;
            for (;i<lbxColsRight.Items.Count;i++)
            {
                COLINFO cl = (COLINFO) lbxColsRight.Items[i];
                if (cl.CI_NAME.IndexOf("]") != -1)
                {
                    cl.CI_NAME = cl.CI_NAME.Substring(cl.CI_NAME.IndexOf("]")+1);
                }
                lbxColsRight.Items[i] = new COLINFO { CI_ID = cl.CI_ID, CI_COLNAME = cl.CI_COLNAME, CI_NAME = "[" + SysUtil.GetExcelColumnName(i + 1) + "]" + cl.CI_NAME };
                if (cl.CI_NAME == "效益年终奖")
                { 
                
                }
            }

            
            for (i = 0; i < lbxColsLeft.Items.Count; i++) 
            {
                COLINFO cl = (COLINFO)lbxColsLeft.Items[i];
                if (cl.CI_NAME.IndexOf("]") != -1)
                {
                    cl.CI_NAME = cl.CI_NAME.Substring(cl.CI_NAME.IndexOf("]")+1);
                }
                lbxColsLeft.Items[i] = new COLINFO { CI_ID = cl.CI_ID, CI_COLNAME = cl.CI_COLNAME, CI_NAME =  cl.CI_NAME };

            }
        }

        /// <summary>
        /// 修改模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckInput())
                {
                    MessageBox.Show("请检查输入项", this.funName, MessageBoxButtons.OK);
                }

                int tmpid = 0;

                if (this.flag == "NEW") //insert
                {
                    InsertNewTemplate(ref tmpid);  //新建
                    if (tmpid != 0)
                    {
                        MessageBox.Show("添加成功");
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    
                }
                else if (this.flag == "UPDATE")
                {

                    TEMPLATE_INFO lti = db.TEMPLATE_INFOs.SingleOrDefault(t => t.TI_ID == this.tiidtmp); //TODO ,should be single or default
                    lti.TI_ISVALID = false;
                    List<TEMPLATE_COLUMN> lTC = db.TEMPLATE_COLUMNs.Where(tc => tc.TC_TIID == lti.TI_ID).Select(a => a).ToList();
                    foreach (var detail in lTC)
                    {
                        db.TEMPLATE_COLUMNs.DeleteOnSubmit(detail);
                    }
                    

                    db.SubmitChanges();

                    InsertNewTemplate(ref tmpid);

                    //update ac tmpid to the new one
                    var lac = db.AGENT_COUNTRies.Where(a => a.AC_TIID == lti.TI_ID).Select(l => l);
                    foreach (var d in lac)
                    {
                        d.AC_TIID = tmpid;
                    }
                    db.SubmitChanges();
                    MessageBox.Show("保存成功", this.funName, MessageBoxButtons.OK);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.funName, MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private bool InsertNewTemplate(ref int tmpid)
        {
            try
            {

                List<COLINFO> lRight = lbxColsRight.Items.Cast<COLINFO>().ToList();
                var x =  lRight.SingleOrDefault(a => a.CI_NAME.IndexOf(txtNameCol.Text.Trim() + "]") == 1);
                string s = lRight.Where(a => a.CI_NAME.IndexOf(txtNameCol.Text.Trim() + "]") == 1).SingleOrDefault().ToString();
               
                //模板相关信息
                TEMPLATE_INFO ti = new TEMPLATE_INFO
                {
                    TI_NAME = txtTempName.Text.Trim(),
                    TI_ISVALID = true,
                    TI_COLSINORDER = String.Join(",", lRight.Select(o => o.CI_COLNAME)),
                    TI_COLHEADERINORDER  = String.Join(",", lRight.Select(o => o.CI_NAME.Substring(o.CI_NAME.IndexOf("]")+1))),
                    TI_SALF = FormulaC.GetFormula(txtSalF.Text.Trim()),
                    TI_SALFRAW = txtSalF.Text.Trim(), 
                    TI_ANNUCOL = txtYearAnnu.Text.Trim(),
                    TI_VERFYFRAW = txtVerify.Text.Trim(),
                    TI_HEARDERROW = cbxHeaderRow.SelectedIndex+1,
                    TI_DATAROW = cbxDataRow.SelectedIndex+1,
                    
                    TI_COLSCOUNT = lbxColsRight.Items.Count,
                    TI_NAMECOL = txtNameCol.Text.Trim(),
                    TI_BONUSCOL = txtBonusCol.Text.Trim() ,
                    TI_MONTHCOL = txtMonthCol.Text.Trim(),
                    TI_REALCOL = txtRealCol.Text.Trim()
                };
                if (txtVerify.Text.Trim()!=string.Empty)
                {
                    ti.TI_VERFYF =  FormulaC.GetFormula( txtVerify.Text.Trim());
                }
                db.TEMPLATE_INFOs.InsertOnSubmit(ti);
                db.SubmitChanges();

                tmpid = ti.TI_ID;

                //模板列信息
                int idex = 0;
                foreach (var i in lbxColsRight.Items)
                {
                    idex++;
                    //insert tmplate cols
                    COLINFO ci = (COLINFO)i;

                    TEMPLATE_COLUMN tc = new TEMPLATE_COLUMN
                    {
                        TC_CIID = ci.CI_ID,
                        TC_TIID = ti.TI_ID,
                        TC_ORDER = idex,
                    };
                    db.TEMPLATE_COLUMNs.InsertOnSubmit(tc);
                }

                //update agentcurrcy set tmpID to new ID


                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.funName, MessageBoxButtons.OK);
                return false;
            }

        }

        private bool CheckInput()
        {
            try
            {
                if (FormulaC.IsValid(txtSalF.Text.Trim()) == false)
                {
                    return false;
                }

                string s = FormulaC.GetFormula(txtSalF.Text.Trim());
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 列调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            
            lbxColsRight.Items.Add(lbxColsLeft.SelectedItem);
            lbxColsLeft.Items.Remove(lbxColsLeft.SelectedItem);
            UpdatePrex();
        }

        /// <summary>
        /// 列调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            lbxColsLeft.Items.Add(lbxColsRight.SelectedItem);
            lbxColsRight.Items.Remove(lbxColsRight.SelectedItem);
            UpdatePrex();
        }

        /// <summary>
        /// 列调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRightAll_Click(object sender, EventArgs e)
        {
            for (int x = lbxColsLeft.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = lbxColsLeft.SelectedIndices[0];
                lbxColsRight.Items.Add(lbxColsLeft.Items[idx]);
                lbxColsLeft.Items.RemoveAt(idx);
                
            }
            UpdatePrex();
           
        }

        /// <summary>
        /// 列调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            for (int x = lbxColsRight.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = lbxColsRight.SelectedIndices[0];
                lbxColsLeft.Items.Add(lbxColsRight.Items[idx]);
                lbxColsRight.Items.RemoveAt(idx);
            }
            UpdatePrex();
        }

        /// <summary>
        /// 列顺序调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            int index = lbxColsRight.SelectedIndex;
            if (index > 0)
            {
                // add a duplicate item up in the listbox
                lbxColsRight.Items.Insert(index - 1, lbxColsRight.SelectedItem);
                lbxColsRight.Items.RemoveAt(index+1);
                lbxColsRight.SelectedIndex = index - 1;
                
            }
            UpdatePrex();
        }

        /// <summary>
        /// 列顺序调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = lbxColsRight.SelectedIndex;
            if (index < lbxColsRight.Items.Count-1 && index != -1)
            {
                // add a duplicate item up in the listbox
                lbxColsRight.Items.Insert(index + 2, lbxColsRight.SelectedItem);
                lbxColsRight.Items.RemoveAt(index );
                lbxColsRight.SelectedIndex = index+1;

            }
            UpdatePrex();
        }

       
        
    }
}
