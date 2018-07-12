using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxDemo
{
    /// <summary>
    /// 缓存参数信息,方便操作,不用每次填写
    /// </summary>
    [Serializable]
    public class TmpData
    {
        public string Title1{get;set;}
        public string Title2 { get; set; }
        public string Rate1 { get; set; }
        public string Rate2 { get; set; }
        public string Annu { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public string ExportDir { get; set; }
    }
}
