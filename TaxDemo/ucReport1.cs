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
    /// 报表1自定义控件
    /// </summary>
    public partial class ucReport1 : UserControl
    {
        public Report1Wrapper RW1;

        public ucReport1()
        {
            InitializeComponent();
            InitialC1DGV1();
            RefreshStyle1();
        }

        private void InitialC1DGV1()
        {
            lblTitle.Text = lblNote1.Text = lblNote2.Text = string.Empty;
            c1FlexGrid1.AllowMerging = AllowMergingEnum.Custom;
            c1FlexGrid1.AllowDragging = AllowDraggingEnum.Both;
            c1FlexGrid1.AllowFreezing = AllowFreezingEnum.Both;
            foreach (Column c in c1FlexGrid1.Cols)
            {
                c.AllowMerging = true;
            }


            CellRange cr = new CellRange();
            //Add the grouped heading row
            c1FlexGrid1.Rows[0].Move(1);
            c1FlexGrid1[0, 0] = "月份";
            c1FlexGrid1[1, 0] = "月份";
            cr = c1FlexGrid1.GetCellRange(0, 0, 1, 0);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlexGrid1.MergedRanges.Add(cr);

            c1FlexGrid1[0, 1] = "应纳税工资";
            c1FlexGrid1[1, 1] = "应纳税工资";
            cr = c1FlexGrid1.GetCellRange(0, 1, 1, 1);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlexGrid1.MergedRanges.Add(cr);

            c1FlexGrid1[0, 2] = "实发工资";
            c1FlexGrid1[0, 3] = "实发工资";
            cr = c1FlexGrid1.GetCellRange(0, 2, 0, 3);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlexGrid1.MergedRanges.Add(cr);
            c1FlexGrid1[1, 2] = "美元";
            c1FlexGrid1[1, 3] = "折合人民币";

            c1FlexGrid1[0, 4] = "已纳税额";
            c1FlexGrid1[0, 5] = "已纳税额";
            cr = c1FlexGrid1.GetCellRange(0, 4, 0, 5);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlexGrid1.MergedRanges.Add(cr);
            c1FlexGrid1[1, 4] = "美元";
            c1FlexGrid1[1, 5] = "折合人民币";

            c1FlexGrid1[0, 6] = "应纳税工资合计\n(人民币)";
            c1FlexGrid1[1, 6] = "应纳税工资合计\n(人民币)";
            cr = c1FlexGrid1.GetCellRange(0, 6, 1, 6);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            
            c1FlexGrid1.MergedRanges.Add(cr);

        }

        private void ucReport1_Load(object sender, EventArgs e)
        {
            
            
        }

        /// <summary>
        /// 报表1加载数据显示
        /// </summary>
        public void LoadData()
        {
            if (RW1 != null)
            {
                //REPORT1
                lblTitle.Text = string.Format("{0}-{1}{2}工资单", RW1.ACNAME, RW1.TPNAME, RW1.Base.RI_YEARTITLE1);

                lblNote1.Text = RW1.Base.RI_NOTE1;
                lblNote2.Text = RW1.Base.RI_NOTE2;

                var data = RW1.Details.OrderBy(a => a.R1_MONTH).ToList();
                //load data;
                foreach (var d in data)
                {
                    c1FlexGrid1.AddItem(new string[] { d.R1_MONTH, d.R1_FORTAX.ToString("F2"), d.R1_REALDOLLOR.ToString(), d.R1_REALRMB.ToString("F2"), d.R1_TAXALREADY.ToString("F2"), d.R1_TAXALREADYRMB.ToString("F2"), d.R1_SALARYRMB.ToString("F2") });
                }
                AddBonus1(RW1.Base.RI_BONUS);
                //add lbl
                RefreshStyle1();
            }

        }

        

        private void AddBonus1(decimal bonus)
        {
            CellRange cr = new CellRange();
            string strbonus = bonus.ToString("F2");
            c1FlexGrid1.Rows.Add();
            int index = c1FlexGrid1.Rows.Count - 1;
            c1FlexGrid1[index, 0] = "年终奖";
            c1FlexGrid1[index, 1] = strbonus;
            c1FlexGrid1[index, 2] = strbonus;
            c1FlexGrid1[index, 3] = strbonus;
            c1FlexGrid1[index, 4] = strbonus;
            c1FlexGrid1[index, 5] = strbonus;
            c1FlexGrid1[index, 6] = strbonus;

            cr = c1FlexGrid1.GetCellRange(index, 1, index, 6);
            cr.StyleNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlexGrid1.MergedRanges.Add(cr);
        }

        private void RefreshStyle1()
        {
            c1FlexGrid1.Styles.Add("White");
            c1FlexGrid1.Styles["White"].BackColor = Color.White;
            c1FlexGrid1.Styles["White"].Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Flat;
            c1FlexGrid1.Styles["White"].Border.Color = Color.Black;

            for (int i = 0; i < c1FlexGrid1.Rows.Count; i++)
            {
                c1FlexGrid1.Rows[i].Style = c1FlexGrid1.Styles["White"];

            }
        }
    }
}
