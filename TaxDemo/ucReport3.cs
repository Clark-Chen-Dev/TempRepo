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
    /// <summary>
    /// 自定义报表3控件,
    /// </summary>
    public partial class ucReport3 : UserControl
    {
        public REPORT_INFO RI;
        public AGENT_INFO AI;

        /* Modified by cyq 20160331 */
        /************  Begin  *********/
        public decimal InternalTotal { get; set; }
        public decimal ExternalTotal { get; set; }
        /************  End  *********/

        public ucReport3()
        {
            InitializeComponent();
            InitialC1DGV3();
        }


        private void InitialC1DGV3()
        {
            c1FlexGrid3[0, 0] = "境内";
            c1FlexGrid3[0, 1] = "境外";
            c1FlexGrid3[0, 2] = "合计";
            c1FlexGrid3[0, 3] = "应纳税所得额";
            c1FlexGrid3[0, 4] = "应纳税额";
            c1FlexGrid3[0, 5] = "已缴(扣)税额";
            c1FlexGrid3[0, 6] = "抵扣税额";

        }

        private void ucReport3_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 报表3数据加载
        /// </summary>
        public void LoadData()
        {
            if (RI != null)
            {
                //txtInfo.Text = txtData.Text = string.Empty;
                if (RI.RI_MOUNTHCOUNT == 12 && RI.RI_SUMS >= 120000)
                {
                    c1FlexGrid3.Rows.Add();
                    int index = c1FlexGrid3.Rows.Count - 1;


                    /* Modified by cyq 20160331 */
                    /************  Begin  *********/
                    //c1FlexGrid3[index, 0] = 0;
                    //c1FlexGrid3[index, 1] = RI.RI_SUMS.ToString("F2");
                    c1FlexGrid3[index, 0] = InternalTotal.ToString("F2");
                    //c1FlexGrid3[index, 1] = ExternalTotal.ToString("F2");
                    c1FlexGrid3[index, 1] = (RI.RI_SUMS - InternalTotal).ToString("F2");
                    /************  End  *********/

                    c1FlexGrid3[index, 2] = RI.RI_SUMS.ToString("F2");
                    c1FlexGrid3[index, 3] = RI.RI_SUMTAXSALARY.ToString("F2");
                    c1FlexGrid3[index, 4] = RI.RI_SUMTAXRMB;
                    c1FlexGrid3[index, 5] = RI.RI_NEEDPACKREAL>0?RI.RI_NEEDPACKREAL:0;
                    if (RI.RI_SUMTAXRMB < (RI.RI_SUMTAXALREADYRMB + RI.RI_USEALL))
                    {
                        c1FlexGrid3[index, 6] = RI.RI_SUMTAXRMB;
                    }
                    else
                    {
                        c1FlexGrid3[index, 6] = RI.RI_SUMTAXALREADYRMB + RI.RI_USEALL;
                    }
                    
                }
                else
                {
                    lblMsg.Visible = true;
                }

                

            }
        }

        /// <summary>
        /// 刷新可用抵扣
        /// </summary>
        /// <param name="use"></param>
        /// <param name="real"></param>
        internal void AddCanUse(decimal use,decimal real)
        {
            c1FlexGrid3[1, 5] = real;
            c1FlexGrid3[1, 6] = use;
        }
    }
}
