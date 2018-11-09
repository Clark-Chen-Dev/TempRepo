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
    public partial class frmImportSalary : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        private string funName = "数据处理";
        private int ACID; // 国别ID Added by CYQ 2018-07-12
        private int TIID; // 国别模板ID Added by CYQ 2018-07-12
        private int AYNAME; // 获取当前年度 Added by CYQ 2018-07-12
        private decimal rate1; // 个税所在国货币与美元的汇率（1外国货币兑换多少美元） Added by CYQ 2018-07-12
        private decimal rate2; // 美元和人民币汇率（1美元兑换多少人民币） Added by CYQ 2018-07-12

        private List<string> Names;
        private Dictionary<string, decimal> TaxAlready;

        public frmImportSalary()
        {
            InitializeComponent();

            //数据绑定
            cbxAgents.DisplayMember = "AI_NAME";
            cbxAgents.ValueMember = "AI_ID";

            cbxAgentCs.DisplayMember = "AC_NAME";
            cbxAgentCs.ValueMember = "AC_ID";
        }

        private void frmImportSalary_Load(object sender, EventArgs e)
        {
            loadAgents(); // 加载公司信息 Added by CYQ 2018-07-12
            AGENT_YEAR ay = db.AGENT_YEARs.SingleOrDefault(a => a.AY_ISCURRENT == true);
            txtYear.Text = ay.AY_NAME.ToString(); // 申报年度设置为当前年度 Added by CYQ 2018-07-12
            this.AYNAME = ay.AY_NAME; // 获取当前年度 Added by CYQ 2018-07-12
            cbxOthers.SelectedIndex = 0; // 设置其它数据导入为已纳税额(总额) Added by CYQ 2018-07-12
            LoadXML(); //加载上次使用的参数数据
        }

        /// <summary>
        /// 加载公司信息
        /// </summary>
        private void loadAgents()
        {
            cbxAgents.Items.Clear();
            cbxAgents.DataSource = db.AGENT_INFOs.Where(a => a.AI_ISDEL == false).Select(p => p);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Microsoft Excel files (*.xls)|*.xls;*.xlsx";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = choofdlog.FileName;
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowOther_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Microsoft Excel files (*.xls)|*.xls;*.xlsx";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                txtDirOther.Text = choofdlog.FileName;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            btnImport.Enabled = false; //防止误操作

            dgvSals.Rows.Clear(); //清空原数据

            //检查数据是否填写
            if (checkInput() == false)
            {
                MessageBox.Show("请检查输入项", this.funName, MessageBoxButtons.OK);
                btnImport.Enabled = true;
                return;
            }

            if (cbxAgentCs.SelectedItem == null)
            {
                MessageBox.Show("请选择国别", this.funName, MessageBoxButtons.OK);
                btnImport.Enabled = true;
                return;
            }

            //参数缓存保存至文件
            TmpData td = new TmpData
            {
                Title1 = txtYearTitle1.Text.Trim(),
                Title2 = txtYearTitle2.Text.Trim(),
                Annu = txtYearAnnu.Text.Trim(),
                Rate1 = txtERate1.Text.Trim(),
                Rate2 = txtERate2.Text.Trim(),
                Date1 = dtDeclare.Value,
                Date2 = dtTableOn.Value

            };
            SaveToFile(td);

            //获取模板
            int tmpID = (int)db.AGENT_COUNTRies.SingleOrDefault(a => a.AC_ID == ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_ID).AC_TIID;
            TEMPLATE_INFO ti = db.TEMPLATE_INFOs.SingleOrDefault(a => a.TI_ID == tmpID);

            //获取年金限额
            decimal YearAnnuLimit = Convert.ToDecimal(txtYearAnnu.Text.Trim());

            ExcelHelper excel = new ExcelHelper();
            try
            {
                excel.OpenFile(txtDir.Text, 1, true);
                int rowCount = excel.GetDataRowsCount((int)ti.TI_DATAROW);

                //检查列数是否一致
                if (excel.GetColumnsCount() != ti.TI_COLSCOUNT)
                {
                    MessageBox.Show("文件输入项与模板列数不一致，请检查后再导入");
                    btnImport.Enabled = true;
                    return;
                }

                //检查表头是否一致
                if (!cbxNeedCheck.Checked)
                {
                    if (TemplateUtil.CheckTitle(tmpID, excel.GetHeader((int)ti.TI_HEARDERROW)) == false)
                    {
                        MessageBox.Show("导入文件表头与模板不一致，请检查", this.funName, MessageBoxButtons.OK);
                        btnImport.Enabled = true;
                        return;
                    }
                }

                //获取EXCEL薪资数据,姓名为KEY,各月数据LIST
                Dictionary<string, List<List<string>>> lists = excel.GetRows((int)ti.TI_DATAROW, rowCount, SysUtil.GetExcelColumnIntValue(ti.TI_NAMECOL));

                //检查纳税人是否存在
                List<string> namenotexsist = new List<string>();
                foreach (var name in lists.Keys)
                {
                    if (namenotexsist.Contains(name) == false)
                    {
                        if (db.TAX_PLAYERs.SingleOrDefault(a => a.TP_NAME == name && a.TP_ACID == this.ACID) == null)
                        {
                            namenotexsist.Add(name);
                        }
                    }
                }

                if (namenotexsist.Count > 0)
                {
                    MessageBox.Show("以下员工信息尚不存在：" + string.Join<string>(";", namenotexsist), this.funName);
                    btnImport.Enabled = true;
                    return;
                }


                Names = lists.Keys.ToList(); //需要导入的已纳税人姓名,下述按姓名
                TaxAlready = new Dictionary<string, decimal>(); //已纳税数据缓存
                decimal rate2 = Convert.ToDecimal(txtERate2.Text); //美元人民币汇率

                foreach (var item in lists) //每个纳税人
                {
                    string name = item.Key;

                    var value = item.Value;

                    foreach (var v in value) //每个纳税人,每月记录
                    {
                        var newv = v.ToList(); //每个纳税人,每月每项数据
                        string sBonus = v[SysUtil.GetExcelColumnIntValue(ti.TI_BONUSCOL) - 1]; //年度奖金
                        decimal annu = Convert.ToDecimal(v[SysUtil.GetExcelColumnIntValue(ti.TI_ANNUCOL) - 1]); //企业年金
                        decimal fortax = FormulaC.GetCal(ti.TI_SALF, v); //计算税前
                        if (Math.Abs(annu) > Math.Abs(YearAnnuLimit)) //企业年金与年金限额比较
                        {
                            fortax += (Math.Abs(annu) - Math.Abs(YearAnnuLimit));
                        }

                        newv.Insert(0, name); //姓名
                        newv.Insert(1, v[SysUtil.GetExcelColumnIntValue(ti.TI_MONTHCOL) - 1]); //月份
                        newv.Insert(2, fortax.ToString()); //税前
                        newv.Insert(3, v[SysUtil.GetExcelColumnIntValue(ti.TI_REALCOL) - 1]); //实发美元
                        newv.Insert(4, (Convert.ToDecimal(newv[3]) * rate2).ToString(""));  //实发RMB
                        newv.Insert(5, ""); //已纳税美元
                        newv.Insert(6, ""); //已纳税RMB
                        newv.Insert(7, v[SysUtil.GetExcelColumnIntValue(ti.TI_BONUSCOL) - 1]); //奖金
                        object[] oA = new object[] { false }; //首项为是否选中
                        object[] oB = oA.Concat(newv.ToArray()).ToArray();
                        dgvSals.Rows.Add(oB.ToArray()); //加入数据表显示
                    }
                }

                //显示数据统计信息
                UpdateInfo();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Close();
                btnImport.Enabled = true;
            }

        }

        /// <summary>
        /// 保存当前用户输入信息 CYQ 2018-07-12
        /// </summary>
        /// <param name="td">用户输入参数</param>
        private void SaveToFile(TmpData td)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(TmpData));

                System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory +
                    @"\tmp.xml");
                writer.Serialize(file, td);
                file.Close();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 加载上次使用的参数文件
        /// </summary>
        public void LoadXML()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader =
                    new System.Xml.Serialization.XmlSerializer(typeof(TmpData));
                System.IO.StreamReader file = new System.IO.StreamReader(System.Environment.CurrentDirectory +
                @"\tmp.xml");
                TmpData tmp = new TmpData();
                tmp = (TmpData)reader.Deserialize(file);

                txtERate1.Text = tmp.Rate1;
                txtERate2.Text = tmp.Rate2;
                txtYearAnnu.Text = tmp.Annu;
                txtYearTitle1.Text = tmp.Title1;
                txtYearTitle2.Text = tmp.Title2;

                dtDeclare.Value = tmp.Date1;
                dtTableOn.Value = tmp.Date2;
                file.Close();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 数据统计信息
        /// </summary>
        private void UpdateInfo()
        {
            lblInfo.Text = string.Format("共计{0}人，{1}条记录", Names.Count, dgvSals.Rows.Count);
        }

        /// <summary>
        /// 检查输入项
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (txtERate1.Text.Trim() == string.Empty ||
                txtERate2.Text.Trim() == string.Empty ||
                txtDir.Text.Trim() == string.Empty ||
                txtYearAnnu.Text.Trim() == string.Empty ||
                txtYearTitle1.Text.Trim() == string.Empty ||
                txtYearTitle2.Text.Trim() == string.Empty)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 根据模板建数据表
        /// </summary>
        /// <param name="ti"></param>
        private void BuildColumns(TEMPLATE_INFO ti)
        {
            dgvSals.Rows.Clear();
            dgvSals.Columns.Clear();

            //是否选中
            DataGridViewCheckBoxColumn dtCheck = new DataGridViewCheckBoxColumn();
            dtCheck.DataPropertyName = "check";
            dtCheck.HeaderText = "";
            dgvSals.Columns.Add(dtCheck);
            dgvSals.Columns[0].Width = 30;


            dgvSals.Columns.Add("姓名", "姓名");
            dgvSals.Columns.Add("月份", "月份");
            dgvSals.Columns.Add("应纳税工资", "应纳税工资");
            dgvSals.Columns.Add("实发工资", "实发工资(美元)");
            dgvSals.Columns.Add("实发工资", "实发工资(RMB)");
            dgvSals.Columns.Add("已纳税额", "已纳税额");
            dgvSals.Columns.Add("已纳税额", "已纳税额(RMB)");

            dgvSals.Columns.Add("年终奖", "年终奖");

            string[] s = ti.TI_COLHEADERINORDER.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                dgvSals.Columns.Add(s[i], s[i]);
            }

            for (int i = 0; i < dgvSals.Columns.Count; i++)
            {
                if (i != 6 && i != 0)
                {
                    dgvSals.Columns[i].ReadOnly = true;
                }
            }
            dgvSals.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSals.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSals.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSals.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSals.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSals.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvSals.Columns[5].DefaultCellStyle.Format = "F2";
            dgvSals.Columns[6].DefaultCellStyle.Format = "F2";
            dgvSals.Columns[7].DefaultCellStyle.Format = "F2";
        }

        /// <summary>
        /// 国别选中后，更新模板，货币与数据表项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAgentCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            AGENT_COUNTRY ac = (AGENT_COUNTRY)cbxAgentCs.SelectedItem; // 国别信息 Added by CYQ 2018-07-12
            lblCurrent.Text = "1" + ac.AC_CURRENCY + "="; // 设置国别对应的货币

            ACID = ac.AC_ID; // 国别ID Added by CYQ 2018-07-12
            TIID = (int)db.AGENT_COUNTRies.SingleOrDefault(a => a.AC_ID == ac.AC_ID).AC_TIID; // 国别模板ID Modified by CYQ 2018-07-12

            TEMPLATE_INFO ti = db.TEMPLATE_INFOs.SingleOrDefault(a => a.TI_ID == TIID); // 获取国别对应的模板信息  Added by CYQ 2018-07-12
            if (ti == null)
            {
                MessageBox.Show("此国别尚未设置导入模板", this.funName, MessageBoxButtons.OK);
                return;
            }
            BuildColumns(ti); // 根据模板构建表格显示的列结构 Added by CYQ 2018-07-12
        }

        private void cbxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxAgentCs.Items.Clear();

            var data = db.AGENT_COUNTRies.Where(a => a.AC_AIID == ((AGENT_INFO)cbxAgents.SelectedItem).AI_ID).Select(p => p);
            foreach (var d in data)
            {
                cbxAgentCs.Items.Add(d);
            }
        }

        /// <summary>
        /// 间隔色显示数据
        /// </summary>

        private void dgvSals_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSals.Rows.Count > 1)
            {
                for (int i = 1; i < dgvSals.Rows.Count; i++)
                {
                    if (dgvSals.Rows[i].Cells[1].Value.ToString() != dgvSals.Rows[i - 1].Cells[1].Value.ToString())
                    {
                        dgvSals.Rows[i].DefaultCellStyle.BackColor = getAnotherColor(dgvSals.Rows[i - 1].DefaultCellStyle.BackColor);
                    }
                    else
                    {
                        dgvSals.Rows[i].DefaultCellStyle.BackColor = dgvSals.Rows[i - 1].DefaultCellStyle.BackColor;
                    }
                }
            }
        }

        private Color getAnotherColor(Color color)
        {
            if (color == Color.LightGray)
            {
                return Color.White;
            }
            else
            {
                return Color.LightGray;
            }
        }

        private void dgvSals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击显示此人的报表一

        }

        private void cmsRight_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查看报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            decimal internalTotal = 0; // 境内合计 Added by CYQ 2018-07-12
            decimal externalTotal = 0; // 境外合计 Added by CYQ 2018-07-12

            try
            {
                if (dgvSals.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选择一行记录后进行操作", this.funName, MessageBoxButtons.OK);
                    return;
                }

                List<string> months = new List<string>();
                for (int i = 0; i < dgvSals.SelectedRows.Count; i++)
                {
                    months.Add(dgvSals.SelectedRows[i].Cells[2].Value.ToString());
                }
                List<string> mons = months.OrderBy(a => a).ToList();

                //修改每月建费用额
                string x = string.Empty;
                frmInput frm = new frmInput();
                frm.monthList = mons;

                if (frm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    Dictionary<string, decimal> minusList = frm.minusList; //每月减费用额，计算税级时使用

                    TEMPLATE_INFO ti = db.TEMPLATE_INFOs.SingleOrDefault(a => a.TI_ID == this.TIID);
                    string playerName = (string)dgvSals.SelectedRows[0].Cells[1].Value; // 个税申报人

                    using (frmReportNew fa = new frmReportNew())
                    {
                        Report1Wrapper rw1 = new Report1Wrapper(); //报表1数据
                        Report2Wrapper rw2 = new Report2Wrapper(); //报表2数据
                        REPORT_INFO ri = new REPORT_INFO(); //报表数据

                        rw1.ACNAME = ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_NAME; // 国别名称 Added by CYQ 2018-07-12
                        rw1.TPNAME = rw2.TPNAME = playerName; // 个税申报人姓名 Added by CYQ 2018-07-12
                        rw1.MINUS = 4800;

                        ri.RI_ACID = this.ACID; // 国别ID Added by CYQ 2018-07-12

                        decimal bonus = getBonus();
                        if (bonus == -1)
                        {
                            string s = "年终奖多于1行，请输入年终奖最终值";
                            if (InputDialog.ShowInputDialog(ref s) == DialogResult.OK)
                            {
                                bonus = Convert.ToDecimal(s);
                            }
                            else
                            {
                                return;
                            }
                        }

                        //报表基础信息
                        ri.RI_BONUS = bonus; // 年终奖金 
                        ri.RI_DECLARETIME = dtDeclare.Value; // 报税时间 
                        ri.RI_TABLETIME = dtTableOn.Value; // 填表时间 
                        ri.RI_ERATE1 = Convert.ToDecimal(txtERate1.Text); // 汇率 1 
                        ri.RI_ERATE2 = Convert.ToDecimal(txtERate2.Text); // 汇率 2
                        ri.RI_MOUNTHCOUNT = dgvSals.SelectedRows.Count;   // 纳税月数  

                        ri.RI_MONTH = mons[0] + "-" + mons[months.Count - 1]; // 月数说明 
                        ri.RI_CREATETIME = dtTableOn.Value; // 生成时间 
                        ri.RI_UIID = SysUtil.CurrentUserID(); // 操作用户 
                        ri.RI_YEARTITLE1 = txtYearTitle1.Text; // 报表1年度说明 
                        ri.RI_YEARTITLE2 = txtYearTitle2.Text; // 报表2年度说明 
                        ri.RI_AYNAME = AYNAME; // 纳税年度
                        ri.RI_YEARANNU = Convert.ToDecimal(txtYearAnnu.Text.Trim()); // 年金限额 
                        ri.RI_CURRENTNAME = lblCurrent.Text.Substring(1, lblCurrent.Text.Length - 2); // 币种名称 
                        int tpid = 0;
                        if (SysUtil.GetPlayerID(ACID, playerName, ref tpid))
                        {
                            ri.RI_TPID = tpid;
                        }

                        for (int i = 0; i < dgvSals.SelectedRows.Count; i++)
                        {
                            //报表1数据
                            var td = new REPORT1_DETAIL
                            {
                                R1_MONTH = dgvSals.SelectedRows[i].Cells[2].Value.ToString(), // 月份
                                R1_FORTAX = Convert.ToDecimal(dgvSals.SelectedRows[i].Cells[3].Value.ToString()), // 应纳税工资
                                R1_REALDOLLOR = Convert.ToDecimal(dgvSals.SelectedRows[i].Cells[4].Value.ToString()), // 实发工资(美元)
                                R1_REALRMB = Convert.ToDecimal(dgvSals.SelectedRows[i].Cells[5].Value.ToString()), // 实发工资(RMB)
                                R1_TAXALREADY = Convert.ToDecimal(dgvSals.SelectedRows[i].Cells[6].Value.ToString()), // 已纳税额
                                R1_TAXALREADYRMB = Convert.ToDecimal(dgvSals.SelectedRows[i].Cells[7].Value.ToString()), // 已纳税额（RMB）
                            };
                            td.R1_SALARYRMB = td.R1_FORTAX + td.R1_REALRMB + td.R1_TAXALREADYRMB; // 应纳税工资合计

                            /* Modified by cyq 20160331 */
                            /************  Begin  *********/
                            if (minusList[td.R1_MONTH] == 3500)
                            {
                                internalTotal += td.R1_SALARYRMB;
                            }
                            else
                            {
                                externalTotal += td.R1_SALARYRMB;
                            }
                            /************  End  *********/

                            //报表2数据
                            var td2 = new REPORT2_DETAIL();
                            td2.R2_MONTH = dgvSals.SelectedRows[i].Cells[2].Value.ToString();
                            td2.R2_SALARY = td.R1_SALARYRMB; // 应纳税工资（报表2）
                            td2.R2_MINUS = minusList[td2.R2_MONTH]; // 减费用额（报表2）
                            td2.R2_TAXSALARY = td2.R2_SALARY - td2.R2_MINUS; // 应纳税所得额（报表2）
                            TAX_RATE ta = SysUtil.GetTaxRate(td2.R2_SALARY, minusList[td2.R2_MONTH]);
                            td2.R2_TAXRATE = ta.TR_RATE; // 税率（报表2）
                            td2.R2_QUICK = ta.TR_QUICH; // 速算扣除数（报表2）
                            td2.R2_NEED = Convert.ToDecimal((td2.R2_TAXSALARY * td2.R2_TAXRATE / 100 - td2.R2_QUICK).ToString("F2")); // 应纳税额（报表2）

                            rw1.Details.Add(td);
                            rw2.Details.Add(td2);
                        }

                        /* Modified by cyq 20160331 */
                        /************  Begin  *********/
                        fa.InternalTotal = internalTotal;
                        fa.ExternalTotal = externalTotal;
                        /************  End  *********/

                        decimal min = rw2.Details.Min(a => a.R2_TAXSALARY);
                        if (min > 0) min = 0;

                        //*** Modified by CYQ on 2018-07-26 ***//
                        // 根据财通佳鑫要求：
                        // 1.当应纳税工资＜=0时 年终奖调整额根据min所在行减费用额，调减3500或4800。
                        // 2.如果“应纳税所得额”值min不唯一，且“减费用额”列对应数值不唯一，则取值4800对年终奖进行调整。
                        //*** Begin ***//
                        else
                        {
                            if (min < -3500)
                            {
                                int minTSCnt = rw2.Details.Where(r => r.R2_TAXSALARY == min).Count(); // 应纳税所得额唯一值统计

                                decimal[] r2_Minus = rw2.Details.Where(r => r.R2_TAXSALARY == min).Select(r => r.R2_MINUS).ToArray();
                                if (minTSCnt > 1) // 存在重复的应纳税所得额
                                {
                                    if (r2_Minus.Distinct().Count() > 1)
                                    {
                                        min = -4800;
                                    }
                                    else
                                    {
                                        min = (-1) * r2_Minus[0];
                                    } // end else
                                }
                                else // 无重复的应纳税所得额
                                {
                                    min = (-1) * r2_Minus[0];
                                }
                            }
                        } // end else
                        //*** End ***//

                        TAX_RATE trbonus = SysUtil.GetTaxRate((ri.RI_BONUS + min) / 12, 4800, true);
                        ri.RI_BONUSTAXRATEID = trbonus.TR_ID; // 年终奖金税率
                        ri.RI_BONUSTAX = Convert.ToDecimal(((ri.RI_BONUS * trbonus.TR_RATE / 100) - trbonus.TR_QUICH).ToString("F2")); // 应纳税额（年终奖）
                        ri.RI_SUMS = rw2.Details.Sum(a => a.R2_SALARY) + ri.RI_BONUS; // 收入额（合计）
                        ri.RI_SUMTAXSALARY = rw2.Details.Sum(a => a.R2_TAXSALARY) + ri.RI_BONUS; // 应纳税所得额（合计）
                        ri.RI_SUMTAXRMB = rw2.Details.Sum(a => a.R2_NEED) + (decimal)ri.RI_BONUSTAX; // 应纳税额（合计）
                        ri.RI_SUMTAXALREADY = TaxAlready[playerName];
                        ri.RI_SUMTAXALREADYRMB = Convert.ToDecimal((TaxAlready[playerName] * ri.RI_ERATE1 * ri.RI_ERATE2).ToString("F2")); // rw1.Details.Sum(a => a.R1_TAXALREADYRMB); // 境外已纳税额
                        ri.RI_NEEDPACK = (decimal)ri.RI_SUMTAXRMB - (decimal)ri.RI_SUMTAXALREADYRMB; // 境内应补税额【计算公式：应纳税额（合计） - 境外已纳税额】
                        ri.RI_USEALL = 0;
                        ri.RI_USE2 = 0;

                        /* Modified by cyq 20160331 */
                        /* ***********  Begin  *********/
                        if (ri.RI_NEEDPACK < 0)
                        {
                            TAX_CREDIT tc = new TAX_CREDIT
                            {
                                TP_ID = tpid,
                                TC_YEAR = AYNAME,
                                TC_CREDITALL = Math.Abs(ri.RI_NEEDPACK),
                                TC_CREDITUSED = 0,
                                TC_CREDITBALANCE = Math.Abs(ri.RI_NEEDPACK)
                            };

                            fa.AddedTC = tc;
                        }
                        else
                        {
                            TAX_CREDIT tc = new TAX_CREDIT
                            {
                                TP_ID = tpid,
                                TC_YEAR = AYNAME,
                                TC_CREDITALL = 0,
                                TC_CREDITUSED = 0,
                                TC_CREDITBALANCE = 0
                            };

                            fa.AddedTC = tc;
                        }
                        /* ***********  End  *********/

                        if (ri.RI_NEEDPACK < 0)
                        {
                            ri.RI_NEEDPACKREAL = 0;
                        }
                        else
                        {
                            ri.RI_NEEDPACKREAL = ri.RI_NEEDPACK; // 境内实需补税额 
                        }
                        ri.RI_NOTE1 = string.Format("注：1. {0}{1}{2}元人民币", dtDeclare.Value.ToString("yyyy年MM月dd日"), lblCurrent.Text, (ri.RI_ERATE1 * ri.RI_ERATE2).ToString("f6")); // 备注1 
                        ri.RI_NOTE2 = string.Format("2. {0}年{1}已纳税额 {2} {3},折合人民币 {4} 元", txtYear.Text, rw1.ACNAME, ri.RI_SUMTAXALREADY.ToString("N2"), ((AGENT_COUNTRY)cbxAgentCs.SelectedItem).AC_CURRENCY, ((decimal)ri.RI_SUMTAXALREADYRMB).ToString("N2")); // 备注2

                        ri.RI_USEALL = 0; // 使用可抵税总额 

                        rw1.Base = rw2.Base = ri;
                        fa.RW1 = rw1;
                        fa.RW2 = rw2;
                        fa.Owner = this;
                        //展示报表
                        if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误类型：" + ex.GetType().ToString() + "\n" +
                                    "错误消息：" + ex.Message + "\n" +
                                    "对象名称：" + ex.Source + "\n" +
                                    "引发异常的方法：" + ex.TargetSite + "\n" +
                                    "调用堆栈：" + ex.StackTrace + "\n");
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ";是否已纳税额尚未输入");
                return;
            }


        }

        /// <summary>
        /// 获取年终奖金数据
        /// </summary>
        /// <returns></returns>
        private decimal getBonus()
        {
            decimal result = 0;
            int count = 0;
            decimal tmp;
            for (int i = 0; i < dgvSals.SelectedRows.Count; i++)
            {
                string strTmp = dgvSals.SelectedRows[i].Cells[8].Value.ToString().Trim();
                if (strTmp == string.Empty)
                {
                    tmp = decimal.Zero;
                }

                else
                {
                    tmp = Convert.ToDecimal(strTmp);
                }
                if (tmp != 0)
                {
                    count++;
                    result = tmp;
                }
            }
            if (count > 1)
            {
                //show dialog
                return -1;
            }
            else if (count == 1)
            {
                return result;
            }
            return result;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 已纳税额输入,均分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            string x = string.Empty;
            if (dgvSals.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一行记录后进行操作", this.funName, MessageBoxButtons.OK);
                return;
            }
            string name = (string)dgvSals.SelectedRows[0].Cells[1].Value;
            x = (string)dgvSals.SelectedRows[0].Cells[1].Value;
            if (InputDialog.ShowInputDialog(ref x) == System.Windows.Forms.DialogResult.OK)
            {
                decimal sum = Convert.ToDecimal(x); //TODO 小数点位数
                decimal sumRMB = Convert.ToDecimal((sum * rate1 * rate2).ToString("f2")); //f4
                if (sum >= 0)
                {
                    if (TaxAlready.ContainsKey(name))
                    {
                        TaxAlready.Remove(name);
                    }
                    TaxAlready.Add(name, sum);
                    int count = dgvSals.SelectedRows.Count;
                    List<decimal> lrate = getRateList(); //按比例均分已纳税
                    List<decimal> tm = new List<decimal>();
                    for (int i = 0; i < dgvSals.SelectedRows.Count; i++)
                    {
                        if ((bool)(dgvSals.SelectedRows[i].Cells[0].Value) == true)
                        {
                            dgvSals.SelectedRows[i].Cells[6].Value = sum * lrate[i];
                            dgvSals.SelectedRows[i].Cells[7].Value = sumRMB * lrate[i];
                            tm.Add(sumRMB * lrate[i]);
                        }
                        else
                        {
                            dgvSals.SelectedRows[i].Cells[6].Value = 0;
                            dgvSals.SelectedRows[i].Cells[7].Value = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取已纳税均分比例
        /// </summary>
        /// <returns></returns>
        private List<decimal> getRateList()
        {
            List<decimal> need = new List<decimal>();
            List<decimal> realrmb = new List<decimal>();
            List<decimal> lrate = new List<decimal>();
            for (int i = 0; i < dgvSals.SelectedRows.Count; i++)
            {
                if ((bool)(dgvSals.SelectedRows[i].Cells[0].Value) == true) // 有不参与均分的行数据
                {
                    need.Add(Convert.ToDecimal(dgvSals.SelectedRows[i].Cells[3].Value));
                    realrmb.Add(Convert.ToDecimal(dgvSals.SelectedRows[i].Cells[5].Value));
                }
                else
                {
                    //不参与的置0
                    need.Add(0);
                    realrmb.Add(0);
                }
            }
            decimal sum = need.Sum() + realrmb.Sum();

            for (int i = 0; i < need.Count; i++)
            {
                lrate.Add((need[i] + realrmb[i]) / sum);
            }

            return lrate;

        }

        /// <summary>
        /// 右键,选中当前纳税人的所有记录,并提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSals_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowSelected = e.RowIndex;
                if (e.RowIndex != -1)
                {
                    dgvSals.ClearSelection();


                    dgvSals.Rows[rowSelected].Selected = true;


                    string x = (string)dgvSals.Rows[rowSelected].Cells[1].Value;
                    bool flag = false;
                    if ((bool)(dgvSals.Rows[rowSelected].Cells[0].Value) == true)
                    {
                        flag = true;
                    }
                    for (int i = dgvSals.Rows.Count - 1; i >= 0; i--)
                    {
                        if (((string)dgvSals.Rows[i].Cells[1].Value) == x)
                        {
                            dgvSals.Rows[i].Selected = true;
                            if ((bool)(dgvSals.Rows[i].Cells[0].Value) == true)
                            {
                                flag = true;
                            }
                        }
                    }

                    if (flag == false)
                    {
                        clearCheck();
                        for (int i = dgvSals.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            dgvSals.SelectedRows[i].Cells[0].Value = true;
                        }
                    }

                    lblErrBonus.ForeColor = lblErrCheck.ForeColor = lblErrMonth.ForeColor = lblErrNo.ForeColor = Color.Black;
                    int err = getError();

                    if ((err & 0x0001) != 0)
                    {
                        lblErrNo.ForeColor = Color.Red;
                    }
                    if ((err & 0x0002) != 0)
                    {
                        lblErrMonth.ForeColor = Color.Red;
                    }
                    if ((err & 0x0004) != 0)
                    {
                        lblErrBonus.ForeColor = Color.Red;
                    }
                    if ((err & 0x0008) != 0)
                    {
                        lblErrCheck.ForeColor = Color.Red;
                    }

                }

            }
        }

        private void clearCheck()
        {
            int count = dgvSals.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgvSals.Rows[i].Cells[0].Value = false;
            }
        }

        private int getError()
        {
            int result = 0;
            if (dgvSals.SelectedRows[0].Cells[6].Value.ToString() == string.Empty)
            {
                result += 1;
            }
            if (dgvSals.SelectedRows.Count != 12)
            {
                result += 2;
            }
            if (getBonus() == -1)
            {
                result += 4;
            }
            if (checkErr() == -1)
            {
                result += 8;
            }

            return result;
        }

        private int checkErr()
        {
            return 0;
        }

        private void dgvSals_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 5)
            //{

            //    if (dgvSals.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //    {
            //        dgvSals.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = Convert.ToDecimal(Convert.ToSingle(dgvSals.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * rate1 * rate2).ToString("F2");
            //    }
            //}
        }

        private void txtERate1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtERate1.Text.Trim() != "")
                {
                    rate1 = Convert.ToDecimal(txtERate1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入数据不合法");
            }
        }

        private void txtERate2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtERate2.Text.Trim() != "")
                {
                    rate2 = Convert.ToDecimal(txtERate2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入数据不合法");
            }
        }

        /// <summary>
        /// 原始数据放入数据库,返回批次ID,由报表窗口调用
        /// </summary>
        /// <returns></returns>
        public int WriteToDB()
        {
            //create BathNUM first 
            BATCH_INFO bi = new BATCH_INFO
            {
                BI_IMPORTTIME = DateTime.Now,
                BI_ISDEL = false,
                BI_TIID = this.TIID,
                BI_UIID = SysUtil.CurrentUserID(),
            };
            db.BATCH_INFOs.InsertOnSubmit(bi);
            db.SubmitChanges();

            for (int i = 0; i < dgvSals.SelectedRows.Count; i++)
            {
                TEMPLATE_INFO ti = db.TEMPLATE_INFOs.SingleOrDefault(a => a.TI_ID == this.TIID);
                string cols = string.Format("SI_BIID,{0}", ti.TI_COLSINORDER);
                var value = new List<string>();
                value.Add(bi.BI_ID.ToString());
                for (int index = 0; index < ti.TI_COLSCOUNT; index++)
                {
                    value.Add("'" + dgvSals.SelectedRows[i].Cells[index + 9].Value.ToString() + "'");
                }

                ColumnsDAL cd = new ColumnsDAL();

                cd.OpenConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TaxDemo.Properties.Settings.testdbConnectionString"].ConnectionString);
                cd.WriteRawData(value, cols);
                cd.CloseConnection();

            }

            return bi.BI_ID;
        }

        public void RemoveData()
        {
            foreach (DataGridViewRow row in dgvSals.SelectedRows)
            {
                dgvSals.Rows.Remove(row);
            }

        }

        private void 删除记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("确定删除选中的记录吗", funName, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Names.Remove(dgvSals.SelectedRows[0].Cells[1].Value.ToString());
                RemoveData();
                UpdateInfo();
            }
        }

        /// <summary>
        /// 已纳税数据,导入方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (checkItemsInFile())
            {
                ExcelHelper excel = new ExcelHelper();
                try
                {
                    excel.OpenFile(txtDirOther.Text, 1, true);
                    int rows = excel.GetRowsCount();
                    List<string> title = excel.GetValue(1, 1, 1, 3);
                    if (title[0] != "姓名" || title[1] != "月份" || title[2] != "境外已交税款")
                    {
                        MessageBox.Show("模板错误，请检查", this.funName);
                        return;
                    }

                    int i = 2;
                    TaxAlready.Clear();
                    if (cbxOthers.SelectedIndex == 0) //已纳税数据(总额方式)
                    {
                        for (; i <= rows; i++) //check first
                        {
                            List<string> sl = excel.GetValue(i, 1, i, 3);
                            decimal sum = Convert.ToDecimal(sl[2]);
                            decimal sumRMB = Convert.ToDecimal((sum * rate1 * rate2).ToString("f2"));
                            TaxAlready.Add(sl[0], sum);
                            List<int> rowsfortax = new List<int>();
                            dgvSals.ClearSelection();
                            for (int ii = 0; ii < dgvSals.Rows.Count; ii++)
                            {
                                if (dgvSals.Rows[ii].Cells[1].Value.ToString() == sl[0])
                                {
                                    rowsfortax.Add(ii);
                                }
                            }
                            if (rowsfortax.Count > 0)
                            {
                                List<decimal> lrate = getRateList(rowsfortax);
                                foreach (var ii in rowsfortax)
                                {
                                    dgvSals.Rows[ii].Cells[6].Value = sum * lrate[i];
                                    dgvSals.Rows[ii].Cells[7].Value = sumRMB * lrate[i];
                                }
                            }

                        }


                        MessageBox.Show("导入成功,导入记录" + (rows - 1) + "条", this.funName);
                    }
                    else if (cbxOthers.SelectedIndex == 1) //已纳税(按月)
                    {
                        i = 2;
                        for (; i <= rows; i++) //check first
                        {
                            List<string> sl = excel.GetValue(i, 1, i, 3);
                            string name = sl[0];
                            string month = sl[1];
                            decimal tax = Convert.ToDecimal(sl[2]);
                            if (TaxAlready.ContainsKey(name))
                            {
                                TaxAlready[name] += tax;
                            }
                            else
                            {
                                TaxAlready.Add(sl[0], tax);
                            }

                            for (int ii = 0; ii < dgvSals.Rows.Count; ii++)
                            {

                                if (dgvSals.Rows[ii].Cells[1].Value.ToString() == name && dgvSals.Rows[ii].Cells[2].Value.ToString() == month)
                                {
                                    dgvSals.Rows[ii].Cells[6].Value = tax;
                                    dgvSals.Rows[ii].Cells[7].Value = Convert.ToDecimal((tax * rate1 * rate2).ToString("f2"));
                                }
                            }

                        }

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    excel.Close();
                }
            }
        }

        private class TaxListItem
        {
            public string name { set; get; }
            public string month { set; get; }
            public decimal tax { set; get; }
            public TaxListItem(string n, string m, decimal t)
            {
                name = n;
                month = m;
                tax = t;
            }
        }
        private List<decimal> getRateList(List<int> rowsforTax)
        {
            List<decimal> need = new List<decimal>();
            List<decimal> realrmb = new List<decimal>();
            List<decimal> lrate = new List<decimal>();
            foreach (var i in rowsforTax)
            {
                need.Add(Convert.ToDecimal(dgvSals.Rows[i].Cells[3].Value));
                realrmb.Add(Convert.ToDecimal(dgvSals.Rows[i].Cells[5].Value));
            }
            decimal sum = need.Sum() + realrmb.Sum();

            for (int i = 0; i < need.Count; i++)
            {
                lrate.Add((need[i] + realrmb[i]) / sum);
            }

            return lrate;

        }


        private bool checkItemsInFile()
        {
            return true;
        }

        private void txtYearTitle1_TextChanged(object sender, EventArgs e)
        {
            txtYearTitle2.Text = txtYearTitle1.Text;
        }

        private void dgvSals_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvSals.IsCurrentCellDirty)
            {
                dgvSals.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
