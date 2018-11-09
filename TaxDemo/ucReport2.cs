using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace TaxDemo
{
    /// <summary>
    /// 自定义报表2控件
    /// </summary>
    public partial class ucReport2 : UserControl
    {
        public Report2Wrapper RW2;

        public ucReport2()
        {
            InitializeComponent();
            InitialC1DGV2();
            RefreshStyle2();
        }

        private void ucReport2_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 报表2数据加载
        /// </summary>
        public void LoadData()
        {
            if (RW2 != null)
            {
                //REPORT2
                lblTitle2.Text = string.Format("{0}--工资个税", RW2.TPNAME);
                lblYearTitle.Text = RW2.Base.RI_YEARTITLE2;
                lblNote1.Text = RW2.Base.RI_NOTE1;
                lblNote2.Text = RW2.Base.RI_NOTE2;


                //如果应纳税所得额<0,后续列直接填空，如果年终奖在此月发放，计算年终奖税时还需将此数据扣除
                var data = RW2.Details.OrderBy(a => a.R2_MONTH).ToList();
                foreach (var d2 in data)
                {
                    c1FlexGrid2.AddItem(new string[] { d2.R2_MONTH.ToString(), d2.R2_SALARY.ToString("F2"), d2.R2_MINUS.ToString("F2"), d2.R2_TAXSALARY.ToString("F2"), string.Format("{0}%", d2.R2_TAXRATE), d2.R2_QUICK.ToString(), d2.R2_NEED.ToString() });
                }

                //年终奖按12个月除，若存在月应纳税所得额为负，应先扣除后再计算税率

                c1FlexGrid2.Rows.Add();
                int index = c1FlexGrid2.Rows.Count - 1;
                c1FlexGrid2[index, 0] = "年终奖";
                TAX_RATE trbonus = SysUtil.GetTaxRate((int)RW2.Base.RI_BONUSTAXRATEID);
                c1FlexGrid2[index, 1] = RW2.Base.RI_BONUS;
                c1FlexGrid2[index, 3] = RW2.Base.RI_BONUS;
                c1FlexGrid2[index, 4] = string.Format("{0}%", trbonus.TR_RATE);
                c1FlexGrid2[index, 5] = trbonus.TR_QUICH;
                c1FlexGrid2[index, 6] = RW2.Base.RI_BONUSTAX;

                //总计
                c1FlexGrid2.Rows.Add();
                index = c1FlexGrid2.Rows.Count - 1;
                c1FlexGrid2[index, 0] = "总计";

                c1FlexGrid2[index, 1] = RW2.Base.RI_SUMS.ToString("F2");
                c1FlexGrid2[index, 3] = RW2.Base.RI_SUMTAXSALARY.ToString("F2");

                c1FlexGrid2[index, 6] = RW2.Base.RI_SUMTAXRMB; // 应缴税额(总计) Added by CYQ 2018-07-13
                c1FlexGrid2[index, 7] = RW2.Base.RI_SUMTAXALREADYRMB; // 境外已纳税额(总计) Added by CYQ 2018-07-13

                // Modified by CYQ on 2018-09-10
                // 根据财通嘉鑫要求，将“本年已缴/以前留抵”拆分为“境内已纳税额”和“以前留抵税额”
                // Begin
                //c1FlexGrid2[index, 8] = RW2.Base.RI_USEALL; // 本年已缴/以前留抵(总计) Added by CYQ 2018-07-13
                // End

                // Modify by CYQ 2018-07-13
                // 修复境内应补税额显示错误。
                // Begin
                //c1FlexGrid2[index, 9] = RW2.Base.RI_NEEDPACK; // 境内应补税额(总计) Added by CYQ 2018-07-13

                // Modified by CYQ on 2018-09-10
                // 根据财通嘉鑫要求，将“本年已缴/以前留抵”拆分为“境内已纳税额”和“以前留抵税额”
                // Begin
                //c1FlexGrid2[index, 9] = RW2.Base.RI_SUMTAXRMB - RW2.Base.RI_SUMTAXALREADYRMB - RW2.Base.RI_USEALL; // 境内应补税额(总计)
                c1FlexGrid2[index, 8] = RW2.Base.RI_USE2 != null ? RW2.Base.RI_USE2.Value : 0; // 境内已纳税额

                // 以前留抵税额 = 使用可抵税总额 - 境内已纳税额
                if (RW2.Base.RI_USEALL == null)
                {
                    c1FlexGrid2[index, 9] = 0;
                }
                else
                {
                    if (RW2.Base.RI_USE2 == null)
                    {
                        c1FlexGrid2[index, 9] = RW2.Base.RI_USEALL.Value;
                    }
                    else
                    {
                        c1FlexGrid2[index, 9] = RW2.Base.RI_USEALL.Value - RW2.Base.RI_USE2.Value;
                    }
                }

                c1FlexGrid2[index, 10] = RW2.Base.RI_SUMTAXRMB - RW2.Base.RI_SUMTAXALREADYRMB - RW2.Base.RI_USEALL; // 境内应补税额(总计)
                // End
                // End
            }
            RefreshStyle2();
        }

        private void RefreshStyle2()
        {
            c1FlexGrid2.Styles.Add("White");
            c1FlexGrid2.Styles["White"].BackColor = Color.White;
            c1FlexGrid2.Styles["White"].Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Flat;
            c1FlexGrid2.Styles["White"].Border.Color = Color.Black;

            for (int i = 0; i < c1FlexGrid2.Rows.Count; i++)
            {
                c1FlexGrid2.Rows[i].Style = c1FlexGrid2.Styles["White"];

            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitialC1DGV2()
        {
            lblYearTitle.Text = lblTitle2.Text = lblNote1.Text = lblNote2.Text = string.Empty;

            c1FlexGrid2.AllowMerging = AllowMergingEnum.Custom;
            c1FlexGrid2.AllowDragging = AllowDraggingEnum.Both;
            c1FlexGrid2.AllowFreezing = AllowFreezingEnum.Both;
            foreach (Column c in c1FlexGrid2.Cols)
            {
                c.AllowMerging = true;
            }


            CellRange cr = new CellRange();

            //Add the grouped heading row
            c1FlexGrid2.Rows[0].Move(1);
            c1FlexGrid2[0, 0] = "月份";
            c1FlexGrid2[1, 0] = "月份";
            cr = c1FlexGrid2.GetCellRange(0, 0, 1, 0);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlexGrid2.MergedRanges.Add(cr);

            c1FlexGrid2[0, 1] = "应纳税工资";
            c1FlexGrid2[1, 1] = "人民币(元)";

            c1FlexGrid2[0, 2] = "减费用额";
            c1FlexGrid2[1, 2] = "人民币(元)";

            c1FlexGrid2[0, 3] = "应纳税所得额";
            c1FlexGrid2[1, 3] = "人民币(元)";

            c1FlexGrid2[0, 4] = "税率";
            c1FlexGrid2[1, 4] = "税率";
            cr = c1FlexGrid2.GetCellRange(0, 4, 1, 4);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlexGrid2.MergedRanges.Add(cr);

            c1FlexGrid2[0, 5] = "速算扣除数";
            c1FlexGrid2[1, 5] = "人民币(元)";

            c1FlexGrid2[0, 6] = "应纳税额";
            c1FlexGrid2[1, 6] = "人民币(元)";

            c1FlexGrid2[0, 7] = "境外已纳税额";
            c1FlexGrid2[1, 7] = "人民币(元)";


            // Modified by CYQ on 20180910
            // 根据财通嘉鑫的要求，将“本年已缴/以前留抵”拆分为“境内已纳税额”和“以前留抵税额”
            // Begin
            //c1FlexGrid2[0, 8] = "本年已缴/以前留抵";
            //c1FlexGrid2[1, 8] = "人民币(元)";

            //c1FlexGrid2[0, 9] = "境内应补税额";
            //c1FlexGrid2[1, 9] = "人民币(元)";

            c1FlexGrid2[0, 8] = "境内已纳税额";
            c1FlexGrid2[1, 8] = "人民币(元)";

            c1FlexGrid2[0, 9] = "以前留抵税额";
            c1FlexGrid2[1, 9] = "人民币(元)";

            c1FlexGrid2[0, 10] = "境内应补税额";
            c1FlexGrid2[1, 10] = "人民币(元)";
            // End
        }

        /// <summary>
        /// 增加可用抵扣,刷新数据
        /// </summary>
        /// <param name="canUse"></param>
        /// <param name="real"></param>
        public void AddCanUse(decimal canUse, decimal real)
        {
            c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, 8] = canUse;
            c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, 10] = real;
        }

        /// <summary>
        /// 增加可用抵扣,刷新数据
        /// </summary>
        /// <param name="internalTax">境内已纳税额</param>
        /// <param name="credit">以前留抵税额</param>
        /// <param name="real">境内应补税额</param>
        public void AddCanUse(decimal internalTax, decimal credit, decimal real)
        {
            c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, 8] = internalTax;
            c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, 9] = credit;
            c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, 10] = real;
        }
    }
}
