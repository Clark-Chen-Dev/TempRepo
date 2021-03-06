﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Data.Linq;

namespace TaxDemo
{
    /// <summary>
    /// 报表查看窗口,需加载传入报表数据
    /// </summary>
    public partial class frmReportNew : Form
    {
        private TAXDBDataContext db = new TAXDBDataContext();
        public Report1Wrapper RW1;
        public Report2Wrapper RW2;

        /* Modified by cyq 20160331 */
        /* ***********  Begin  *********/
        public TAX_CREDIT AddedTC { get; set; }
        public decimal InternalTotal { get; set; }
        public decimal ExternalTotal { get; set; }
        /* ***********  End  ******** */


        private string funName = "报表确认";
        public frmReportNew()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载传入的数据,显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReportNew_Load(object sender, EventArgs e)
        {

            ucReport11.RW1 = RW1;
            ucReport21.RW2 = RW2;
            ucReport31.RI = RW1.Base;
            ucReport11.LoadData();
            ucReport21.LoadData();

            /* Modified by CYQ 2018-07-13 */
            // 解决通过点击报表记录查询的报表详细按钮访问报表详细信息时，未计算境内外合计的问题
            /* ***********  Begin  *********/
            if (InternalTotal == 0 && ExternalTotal == 0)
            {
                decimal internalTotal = 0; // 境内合计
                decimal externalTotal = 0; // 境外合计
                foreach (REPORT2_DETAIL detail in RW2.Details)
                {
                    if (detail.R2_MINUS == 3500)
                    {
                        internalTotal += detail.R2_SALARY;
                    }
                    else
                    {
                        externalTotal += detail.R2_SALARY;
                    }
                } // end foreach

                InternalTotal = internalTotal;
                ExternalTotal = externalTotal;
            } // end if
            /* ***********  End  ******** */

            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            ucReport31.InternalTotal = InternalTotal;
            ucReport31.ExternalTotal = ExternalTotal;
            /* ***********  End  ******** */

            ucReport31.LoadData();

            /* Added by cyq 20160331 */
            // 初始化报表二的编表人、复核人
            /* ***********  Begin  *********/
            InitReport2();
            /* ***********  End  ******** */
        }

        private void InitReport2()
        {
            string lastParamsFile = "lastParams.ini";
            if (File.Exists(lastParamsFile))
            {
                using (StreamReader reader = new StreamReader(lastParamsFile, Encoding.UTF8))
                {
                    string lastParamStr = reader.ReadLine();
                    string[] lastParams = lastParamStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    txtScheduler.Text = lastParams[0].Trim();
                    txtReviewer.Text = lastParams[1].Trim();
                }
            } // end if
        } // end method InitReport2

        /// <summary>
        /// 报表数据入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //入库前检查是否已存在
            var tmp = db.REPORT_INFOs.SingleOrDefault(a => a.RI_AYNAME == RW1.Base.RI_AYNAME && a.RI_ACID == RW1.Base.RI_ACID && a.RI_TPID == RW1.Base.RI_TPID);
            if (tmp != null)
            {
                MessageBox.Show("相同记录已存在，请检查", this.funName);
                return;
            }

            //若使用留抵数据,先进行留抵记录的操作
            if (RW1.Base.RI_USEALL != null && RW1.Base.RI_USEALL > 0)
            {
                if (CheckCredit(RW1.Base.RI_TPID, RW1.Base.RI_AYNAME, (decimal)RW1.Base.RI_USEALL, (decimal)RW1.Base.RI_USE2) == false)
                {
                    MessageBox.Show(string.Format("可用抵扣额不足{0}", (decimal)RW1.Base.RI_USEALL));
                    return;
                }
                else
                {
                    var list = db.TAX_CREDITs.Where(a => a.TP_ID == RW1.Base.RI_TPID && (RW1.Base.RI_AYNAME - a.TC_YEAR) <= 5 && a.TC_CREDITBALANCE > 0).OrderBy(l => l.TC_YEAR).ToList();
                    decimal tmpBlance = (decimal)RW1.Base.RI_USEALL - (decimal)RW1.Base.RI_USE2;
                    foreach (var l in list)
                    {
                        decimal tt = l.TC_CREDITBALANCE;
                        if (tmpBlance >= l.TC_CREDITBALANCE)
                        {

                            l.TC_CREDITBALANCE = 0;
                            l.TC_CREDITUSED += tt;
                            tmpBlance = tmpBlance - tt;

                        }
                        else
                        {
                            l.TC_CREDITBALANCE -= tmpBlance;
                            l.TC_CREDITUSED += tmpBlance;
                            tmpBlance = 0;
                        }
                    }
                    db.SubmitChanges();
                }
            }

            //若需补税,计算倒推数据
            if (RW1.Base.RI_NEEDPACKREAL > 0)
            {
                TAX_RATE tr = db.TAX_RATEs.Where(r => RW1.Base.RI_NEEDPACKREAL >= r.TR_MIN && RW1.Base.RI_NEEDPACKREAL < r.TR_MAX).SingleOrDefault();

                RW1.Base.RI_BACKRATEID = tr.TR_ID;
                RW1.Base.RI_BACKMINUS = RW1.MINUS;
                RW1.Base.RI_BACKTAXQUICK = tr.TR_QUICH;
                RW1.Base.RI_BACKTAXRATE = tr.TR_RATE;
                RW1.Base.RI_BACKSUMTAXSALARY = ((decimal)(RW1.Base.RI_NEEDPACKREAL + tr.TR_QUICH) * 100 / tr.TR_RATE);
                RW1.Base.RI_BACKSUMSALARY = RW1.Base.RI_BACKSUMTAXSALARY + RW1.Base.RI_BACKMINUS;

            }

            //报表入库
            int BID = ((frmImportSalary)(this.Owner)).WriteToDB();
            RW1.Base.RI_BIID = BID;
            db.REPORT_INFOs.InsertOnSubmit(RW1.Base); //入库
            db.SubmitChanges();

            //计算本年度的可用抵扣 insert到credit历史中
            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            //if (RW1.Base.RI_SUMTAXALREADYRMB > (RW1.Base.RI_SUMTAXRMB + RW1.Base.RI_USEALL))
            //{
            //    TAX_CREDIT tc = new TAX_CREDIT
            //    {
            //        TC_YEAR = RW1.Base.RI_AYNAME,
            //        TC_RIID = RW1.Base.RI_ID,
            //        TC_CREDITALL = (decimal)(RW1.Base.RI_SUMTAXALREADYRMB + RW1.Base.RI_USEALL) - (RW1.Base.RI_SUMTAXRMB), //(decimal)(RW1.Base.RI_SUMTAXALREADYRMB - (RW1.Base.RI_SUMTAXRMB + RW1.Base.RI_USEALL)),
            //        TC_CREDITUSED = 0,
            //        TC_CREDITBALANCE = (decimal)(RW1.Base.RI_SUMTAXALREADYRMB + RW1.Base.RI_USEALL) - (RW1.Base.RI_SUMTAXRMB)//(decimal)(RW1.Base.RI_SUMTAXALREADYRMB - (RW1.Base.RI_SUMTAXRMB + RW1.Base.RI_USEALL))
            //    };
            //    db.TAX_CREDITs.InsertOnSubmit(tc);
            //    db.SubmitChanges();
            //}

            if (AddedTC != null)
            {
                AddedTC.TC_RIID = RW1.Base.RI_ID;

                if (RW2.Base.RI_USE2.HasValue)
                {
                    if (AddedTC.TC_CREDITALL != 0)
                    {
                        AddedTC.TC_CREDITALL += RW2.Base.RI_USE2.Value;
                        AddedTC.TC_CREDITBALANCE += RW2.Base.RI_USE2.Value;
                    }
                    else
                    {
                        if (RW2.Base.RI_USE2.Value - RW2.Base.RI_NEEDPACK > 0)
                        {
                            AddedTC.TC_CREDITALL += RW2.Base.RI_USE2.Value - RW2.Base.RI_NEEDPACK;
                            AddedTC.TC_CREDITBALANCE += RW2.Base.RI_USE2.Value - RW2.Base.RI_NEEDPACK;
                        }
                    }
                }

                db.TAX_CREDITs.InsertOnSubmit(AddedTC);
                db.SubmitChanges();
            }
            /* ***********  End  *********/


            foreach (var d in RW1.Details)
            {
                d.R1_RIID = RW1.Base.RI_ID;

            }

            foreach (var d in RW2.Details)
            {
                d.R2_RIID = RW1.Base.RI_ID;
            }
            db.REPORT1_DETAILs.InsertAllOnSubmit(RW1.Details); //入库报表1
            db.REPORT2_DETAILs.InsertAllOnSubmit(RW2.Details); //入库报表2

            db.SubmitChanges();


            ((frmImportSalary)(this.Owner)).RemoveData(); //入库后,原窗口将数据从表格中删除

            MessageBox.Show("入库成功", this.funName);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            /* Modified by cyq 20160331 */
            /* ***********  Begin  *********/
            Close();
            /* ***********  End  *********/
        }

        /// <summary>
        /// 报表导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEm_Click(object sender, EventArgs e)
        {

            //*** Modified by CYQ on 2018-07-26 ***//
            // 根据财通嘉鑫要求，增加有关无身份证号的报税申请人处理
            //*** Begin ***//
            TAX_PLAYER currTP = db.TAX_PLAYERs.SingleOrDefault(ai => ai.TP_ID == RW1.Base.RI_TPID);

            #region 用户输入无身份证号的报税申请人的身份证号
            string id = string.Empty;  // 申报人身份证号
            if (string.IsNullOrEmpty(currTP.TP_IDNUMBER))
            {
                string s = "请输入申报人身份证号";
                if (InputDialog.ShowInputDialog(ref s) == DialogResult.OK)
                {
                    id = s.Trim();
                }
            }
            else
            {
                id = currTP.TP_IDNUMBER;
            }

            if (id != string.Empty)
            {
                currTP.TP_IDNUMBER = id;
                db.SubmitChanges();
            }
            else
            {
                MessageBox.Show("未获取到有效的身份证号信息，请重新输入", "消息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion
            //*** End ***//

            fbdPath.Description = "将导出文件保存到...";
            DialogResult result = fbdPath.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK) return;

            Assembly assembly = Assembly.GetExecutingAssembly();
            Assembly asm = Assembly.GetExecutingAssembly();
            string file = string.Format("{0}.Resources.ReportTemplate.xlsx", asm.GetName().Name);

            Stream fileStream = asm.GetManifestResourceStream(file);

            string filename = fbdPath.SelectedPath + string.Format("\\{0}-{1}_{2}.xlsx", RW1.ACNAME, RW1.TPNAME, DateTime.Now.ToString("yyyyMMddHHmm"));

            SaveStreamToFile(filename, fileStream);

            ExcelHelper excel = new ExcelHelper();

            //报表一
            excel.OpenFile(filename, 1);
            excel.SetValue(1, 2, string.Format("{0}-{1}{2}工资单", RW1.ACNAME, RW1.TPNAME, RW1.Base.RI_YEARTITLE1));
            excel.SetValue(4, 6, RW1.Base.RI_CURRENTNAME);
            var data = RW1.Details.OrderBy(a => a.R1_MONTH).ToList();
            //load data;
            int i = 0;
            foreach (var d in data)
            {
                string[] tmp = new string[] { d.R1_MONTH, d.R1_FORTAX.ToString(), d.R1_REALDOLLOR.ToString(), d.R1_REALRMB.ToString(), d.R1_TAXALREADY.ToString(), d.R1_TAXALREADYRMB.ToString(), d.R1_SALARYRMB.ToString() };
                excel.SetValue(5 + i, 2, tmp);
                i++;
            }
            excel.SetValue(17, 3, RW1.Base.RI_BONUS.ToString());
            excel.SetValue(18, 2, RW1.Base.RI_NOTE1);
            excel.SetValue(19, 2, RW1.Base.RI_NOTE2);

            /* Modified by cyq 20160331 */
            /************  Begin  *********/
            excel.SetFontNameSize(21, 2, "宋体", 11);
            excel.SetFontNameSize(21, 4, "宋体", 11);
            excel.SetFontNameSize(23, 2, "宋体", 11);
            excel.SetFontNameSize(23, 4, "宋体", 11);
            excel.SetValue(21, 2, "编制日期： " + RW1.Base.RI_TABLETIME.ToLongDateString().ToString());
            excel.SetValue(21, 4, "复核日期： " + RW1.Base.RI_TABLETIME.AddDays(2).ToLongDateString().ToString());
            excel.SetValue(23, 2, "编制人： " + txtScheduler.Text.Trim());
            excel.SetValue(23, 4, "复核人： " + txtReviewer.Text.Trim());
            /************  End  *********/

            //报表二
            excel.ChangeSheet(2);
            excel.SetValue(3, 1, string.Format("{0}--工资个税               {1}", RW2.TPNAME, RW2.Base.RI_YEARTITLE2));
            var data2 = RW2.Details.OrderBy(a => a.R2_MONTH).ToList();
            i = 0;
            foreach (var d in data2)
            {
                string[] tmp = new string[] { d.R2_MONTH, d.R2_SALARY.ToString(), d.R2_MINUS.ToString(), d.R2_TAXSALARY.ToString(), string.Format("{0}%", d.R2_TAXRATE), d.R2_QUICK.ToString(), d.R2_NEED.ToString() };
                excel.SetValue(7 + i, 1, tmp);
                i++;
            }
            excel.SetValue(19, 2, RW2.Base.RI_BONUS.ToString());
            excel.SetValue(19, 4, RW2.Base.RI_BONUS.ToString());
            TAX_RATE tr = SysUtil.GetTaxRate((int)RW2.Base.RI_BONUSTAXRATEID);
            excel.SetValue(19, 5, string.Format("{0}%", tr.TR_RATE));
            excel.SetValue(19, 6, tr.TR_QUICH.ToString());
            excel.SetValue(19, 7, RW2.Base.RI_BONUSTAX.ToString());
            excel.SetValue(20, 2, RW2.Base.RI_SUMS.ToString());
            excel.SetValue(20, 4, RW2.Base.RI_SUMTAXSALARY.ToString());
            excel.SetValue(20, 7, RW2.Base.RI_SUMTAXRMB.ToString());
            excel.SetValue(20, 8, RW2.Base.RI_SUMTAXALREADYRMB.ToString());

            // Modified by CYQ on 2018-09-10
            // 根据财通嘉鑫要求，将“本年已缴/以前留抵”拆分为“境内已纳税额”和“以前留抵税额”
            // Begin
            //excel.SetValue(20, 9, RW2.Base.RI_USEALL.ToString());
            excel.SetValue(20, 9, (RW2.Base.RI_USE2).ToString()); // 境内已纳税额
            excel.SetValue(20, 10, (RW2.Base.RI_USEALL - RW2.Base.RI_USE2).ToString()); // 以前留抵税额


            //*** Modified by CYQ on 2018-07-13 ***//
            //
            //*** Begin ***//
            //excel.SetValue(20, 10, RW2.Base.RI_NEEDPACKREAL.ToString());

            //if (RW2.Base.RI_NEEDPACKREAL.HasValue &&
            //    RW2.Base.RI_NEEDPACKREAL.Value > 0)
            //{
            //    excel.SetValue(20, 10, RW2.Base.RI_NEEDPACKREAL.ToString());
            //} // end if

            //if (RW2.Base.RI_NEEDPACKREAL.HasValue)
            //{
            //    if (RW2.Base.RI_NEEDPACKREAL.Value > 0)
            //    {
            //        excel.SetValue(20, 10, RW2.Base.RI_NEEDPACKREAL.ToString());
            //    } // end if
            //    else
            //    {
            //        excel.SetValue(20, 10, "0.00");
            //    } // end else
            //}

            if (RW2.Base.RI_NEEDPACKREAL.HasValue)
            {
                if (RW2.Base.RI_NEEDPACKREAL.Value > 0)
                {
                    excel.SetValue(20, 11, RW2.Base.RI_NEEDPACKREAL.ToString());
                } // end if
                else
                {
                    excel.SetValue(20, 11, "0.00");
                } // end else
            }
            // End
            //*** End ***//

            excel.SetValue(22, 1, RW2.Base.RI_NOTE1);
            excel.SetValue(23, 1, string.Format("   {0}", RW2.Base.RI_NOTE2));

            /* Modified by cyq 20160331 */
            /************  Begin  *********/
            excel.SetFontNameSize(25, 2, "宋体", 11);
            excel.SetFontNameSize(25, 4, "宋体", 11);
            excel.SetFontNameSize(27, 2, "宋体", 11);
            excel.SetFontNameSize(27, 4, "宋体", 11);
            excel.SetValue(25, 2, "编制日期： " + RW1.Base.RI_TABLETIME.ToLongDateString().ToString());
            excel.SetValue(25, 4, "复核日期： " + RW1.Base.RI_TABLETIME.AddDays(2).ToLongDateString().ToString());
            excel.SetValue(27, 2, "编制人： " + txtScheduler.Text.Trim());
            excel.SetValue(27, 4, "复核人： " + txtReviewer.Text.Trim());
            /************  End  *********/

            excel.Close();

            //生成报表三
            if (RW2.Base.RI_MOUNTHCOUNT == 12)
            {

                filename = fbdPath.SelectedPath + string.Format("\\{0}-{1}_{2}.doc", RW1.ACNAME, RW1.TPNAME, DateTime.Now.ToString("yyyyMMddHHmm"));
                File.WriteAllBytes(filename, TaxDemo.Properties.Resources.Template3);

                AGENT_INFO ai = SysUtil.GetAgentInfo(RW1.Base.RI_ACID);
                TAX_PLAYER tp = SysUtil.GetTaxPlayer(RW1.Base.RI_TPID);
                WordUtil word = new WordUtil();
                {
                    word.Open(filename);

                    /* Modified by cyq 20160331 */
                    /************  Begin  *********/
                    word.WriteData(RW1.Base, ai, tp, InternalTotal, ExternalTotal);
                    /************  End  *********/
                    word.Close();
                }
            }

            MessageBox.Show("导出成功", this.funName);
            if (cbxOpen.Checked)
            {
                Process.Start(fbdPath.SelectedPath);
            }
        }

        public void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        /// <summary>
        /// 可用抵扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal canUse = Convert.ToDecimal(txtCanUse.Text);

            if (CheckCredit(RW1.Base.RI_TPID, RW1.Base.RI_AYNAME, canUse, 0) == false)
            {
                MessageBox.Show("可用抵扣额不足所输入的值");
                return;

            }
            if (canUse > RW1.Base.RI_NEEDPACK)
            {
                MessageBox.Show("输入值大于所需抵扣" + RW1.Base.RI_NEEDPACK);
                return;
            }
            if (RW1.Base.RI_NEEDPACK <= 0)
            {
                MessageBox.Show("不需要抵扣");
                return;
            }

            RW1.Base.RI_USEALL += canUse; // 使用可抵税总额


            if (RW1.Base.RI_NEEDPACK < 0)
            {
                RW1.Base.RI_NEEDPACKREAL = RW1.Base.RI_NEEDPACK;
            }
            else
            {
                RW1.Base.RI_NEEDPACKREAL = RW1.Base.RI_NEEDPACK - (decimal)RW1.Base.RI_USEALL;
            }

            //更新显示
            // Modified by CYQ on 2018-09-10
            // 根据财通嘉鑫要求，将“本年已缴/以前留抵”拆分为“境内已纳税额”和“以前留抵税额”
            // Begin
            //ucReport21.AddCanUse((decimal)RW1.Base.RI_USEALL, (decimal)RW1.Base.RI_NEEDPACKREAL);

            ucReport21.AddCanUse(RW1.Base.RI_USEALL.Value - canUse, canUse, RW1.Base.RI_NEEDPACKREAL.Value);
            // End

            // Modified by CYQ on 2018-09-14
            // 修复月份不足12个月更新报表3的bug
            // Begin
            if (RW1.Base.RI_MOUNTHCOUNT == 12)
            {
                ucReport31.AddCanUse((decimal)RW1.Base.RI_SUMTAXALREADYRMB + (decimal)(RW1.Base.RI_USEALL), (decimal)RW1.Base.RI_NEEDPACKREAL);
            }
            // End
        }

        private bool CheckCredit(int tpid, int year, decimal canUseall, decimal canuse2)
        {
            var list = db.TAX_CREDITs.Where(a => a.TP_ID == tpid && (year - a.TC_YEAR) <= 5 && a.TC_CREDITBALANCE > 0).ToList();
            if (list == null) return false;
            decimal amount = list.Sum(a => a.TC_CREDITBALANCE);
            if (amount >= (canUseall - canuse2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnTaxCredit_Click(object sender, EventArgs e)
        {
            frmCredit frm = new frmCredit();
            frm.TPID = RW1.Base.RI_TPID;
            frm.CURRENTYEAR = RW1.Base.RI_AYNAME;
            frm.ShowDialog();
        }

        /// <summary>
        /// 其它可用抵扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCanUse2_Click(object sender, EventArgs e)
        {
            decimal canUse = Convert.ToDecimal(txtCanUse2.Text);


            RW1.Base.RI_USEALL += canUse;
            RW1.Base.RI_USE2 = canUse;

            RW1.Base.RI_NEEDPACKREAL = RW1.Base.RI_NEEDPACK - (decimal)RW1.Base.RI_USEALL;

            // Added by CYQ on 2018-09-10
            // 根据财通嘉鑫要求，同步更新可留抵税额
            // Begin
            decimal left = RW1.Base.RI_NEEDPACK - canUse; // 还应补缴税额

            if (left > 0)
            {
                var list = db.TAX_CREDITs.Where(a => a.TP_ID == RW1.Base.RI_TPID && (RW1.Base.RI_AYNAME - a.TC_YEAR) <= 5 && a.TC_CREDITBALANCE > 0).ToList();
                if (list == null)
                {
                    txtCanUse.Text = "0";
                }
                else
                {
                    decimal amount = list.Sum(a => a.TC_CREDITBALANCE); // 可用留抵总和
                    if (amount >= left)
                    {
                        txtCanUse.Text = left.ToString();
                    }
                    else
                    {
                        txtCanUse.Text = amount.ToString();
                    }
                }
            }
            // End

            //更新显示
            // Modified by CYQ on 2018-09-10
            // 根据财通嘉鑫要求，将“本年已缴/以前留抵”拆分为“境内已纳税额”和“以前留抵税额”
            // Begin
            //ucReport21.AddCanUse((decimal)RW1.Base.RI_USEALL, (decimal)RW1.Base.RI_NEEDPACKREAL);
            ucReport21.AddCanUse(canUse, RW1.Base.RI_USEALL.Value - canUse, RW1.Base.RI_NEEDPACKREAL.Value);
            // End

            // Modified by CYQ on 2018-09-14
            // 修复月份不足12个月更新报表3的bug
            if (RW1.Base.RI_MOUNTHCOUNT == 12)
            {
                ucReport31.AddCanUse((decimal)RW1.Base.RI_SUMTAXALREADYRMB + ((decimal)RW1.Base.RI_USEALL), (decimal)RW1.Base.RI_NEEDPACKREAL);
            }
            // End
        }

        private void btnChangeMinus_Click(object sender, EventArgs e)
        {

        }

        private void frmReportNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            string lastParamsFile = "lastParams.ini";
            using (StreamWriter writer = new StreamWriter(new FileStream(
                lastParamsFile, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                writer.WriteLine(txtScheduler.Text + "," + txtReviewer.Text);
                writer.Flush();
                writer.Close();
            }
        }
    }
}
