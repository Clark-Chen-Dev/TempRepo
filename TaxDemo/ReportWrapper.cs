using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxDemo
{
    /// <summary>
    /// 报表1
    /// </summary>
    public class Report1Wrapper
    {
        public REPORT_INFO Base { get; set; }
        public string ACNAME { set; get; }
        public string TPNAME { set; get; }
        public decimal MINUS { set; get; }

        public List<REPORT1_DETAIL> Details;

        public bool InsertToDB()
        {

            return true;
        }

        public Report1Wrapper()
        {
            Details = new List<REPORT1_DETAIL>();
        }

    }

    /// <summary>
    /// 报表2
    /// </summary>
    public class Report2Wrapper
    {
        public REPORT_INFO Base { get; set; }
        public string TPNAME { set; get; }

        public List<REPORT2_DETAIL> Details;

        public bool InsertToDB()
        {
            return true;
        }

         public Report2Wrapper()
        {
            
            Details = new List<REPORT2_DETAIL>();
        }

    }

    
}
