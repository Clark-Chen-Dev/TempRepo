using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxDemo
{
    
    class ColumnTI
    {
        public int CIID { set; get; }
        public string CICOLName { set; get; }
        public short CIType {set;get;}

    }

    /// <summary>
    /// 模板相关工具类
    /// </summary>
    class TemplateUtil
    {
        /// <summary>
        /// 模板列插入
        /// </summary>
        /// <param name="data"></param>
        /// <param name="templateID"></param>
        /// <param name="IBID"></param>
        /// <returns></returns>
        public static string GetInsertString(List<string> data,int templateID, int IBID)
        {
            string result = string.Empty;

            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                List<v_TEMPLATECOL> tcs = db.v_TEMPLATECOLs.Where(v => v.TC_TIID == templateID).OrderBy(vt => vt.TC_ORDER).Select(a => a).ToList();
                string s = string.Empty;
                int i = 0;
                foreach (var v in tcs)
                {
                    if (v.CI_TYPEID == 1)
                    {
                        data[i] = "'" + data[i] + "'";
                    }

                    i++;
                }

                string cols = String.Join(",", tcs.Select(a=>a.CI_COLNAME).ToList());
                string datas = string.Join(",", data);

                result = string.Format("Insert into SALARY_IMPORT ({0},SI_TPID,SI_IBID) values({1},{2},{3})", cols, datas,templateID,IBID);

            }

            return result;
        }


        public static bool IsRowCanInsert(List<string> data, int templateID)
        { 
            //first col num should be equal;
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                 List<v_TEMPLATECOL> tcs = db.v_TEMPLATECOLs.Where(v => v.TC_TIID == templateID).OrderBy(vt => vt.TC_ORDER).Select(a => a).ToList();
                if (tcs.Count != data.Count) return false;
                try
                {
                    int i = 0;
                    foreach (var tc in tcs)
                    {
                        if (tc.CI_TYPEID == 0)
                        {
                            decimal x  = Convert.ToDecimal( data[i]);
                            i++;
                        }
                         
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

                return true;

            }
        }

        /// <summary>
        /// 检查表头
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool CheckTitle(int templateID,List<string> title)
        {
            try
            {
                Dictionary<int, List<string>> myDic = new Dictionary<int, List<string>>();
                
                using (TAXDBDataContext db = new TAXDBDataContext())
                {
                    //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
                    var cids = db.v_TEMPLATECOLs.Where(v => v.TC_TIID == templateID).OrderBy(vt => vt.TC_ORDER).Select(a => a).ToList(); 
                    int i = 0;
                    foreach (var cid in cids)
                    {
                        //default
                        
                        //add alis
                        List<string> cas =  db.COLUMNS_ALIAs.Where(v => v.CA_CIID == cid.CI_ID).Select(a => a.CA_ALIAS).ToList();
                        string t = title[i].Replace(" ", string.Empty);
                        if (cas.Contains(t) == false && cid.CI_NAME!=t)
                        {
                            return false;
                        }
                        i++;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        
    }
}

