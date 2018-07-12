using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.IO;


namespace TaxDemo
{
    /// <summary>
    /// Word操作工具类,主要为报表3使用
    /// </summary>
    class WordUtil
    {
        private Word.Application wordApp ;

        private Word.Document aDoc ;
        private object missing = System.Reflection.Missing.Value;

        /// <summary>
        /// word文件打开
        /// </summary>
        /// <param name="filename"></param>
        public void Open(string filename)
        {
            object readOnly = false;
            object isVisible = false;
            object file = filename;

            wordApp = new Word.Application();
            wordApp.Visible = false;

            if (File.Exists((string)filename))
            {

                aDoc = wordApp.Documents.Open(ref file, ref missing,
                    ref readOnly, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref isVisible, ref missing, ref missing,
                    ref missing, ref missing);

                aDoc.Activate();
            }
        }

        /// <summary>
        /// 向word文件写数据采用FindAndReplace方式处理
        /// </summary>
        /// <param name="findText"></param>
        /// <param name="replaceWithText"></param>
        private  void FindAndReplace( object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object nmatchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_obly = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike,
                ref nmatchAllWordForms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiacritics, ref matchAlefHamza,
                ref matchControl);
        }

        /* Modified by cyq 20160331 */
        /************  Begin  *********/
        ///// <summary>
        ///// 将报表3数据写入word文件
        ///// </summary>
        ///// <param name="ri"></param>
        ///// <param name="ai"></param>
        ///// <param name="tp"></param>
        //public void  WriteData( REPORT_INFO ri, AGENT_INFO ai,TAX_PLAYER tp)
        //{
        //    object replacetext;
        //    //replacetext = ai.AI_NAME;
        //    FindAndReplace("<Y>", ri.RI_TABLETIME.ToString("yyyy"));
        //    FindAndReplace("<M>", ri.RI_TABLETIME.ToString("MM"));
        //    FindAndReplace("<D>", ri.RI_TABLETIME.ToString("dd"));
        //    FindAndReplace("<YEAR>", ri.RI_AYNAME);

        //    FindAndReplace("<EM>", ai.AI_NAME);
        //    FindAndReplace("<TAXID>", ai.AI_TAXID);
        //    FindAndReplace("<INDUS>", ai.AI_INDUSTRY);
        //    FindAndReplace("<ADDR>", ai.AI_ADDRESS);
        //    FindAndReplace("<POSTCODE>", ai.AI_POSTCODE);
        //    FindAndReplace("<TEL>", ai.AI_TEL);

        //    FindAndReplace("<WIT>", "0");
        //    FindAndReplace("<OUT>", ri.RI_SUMS.ToString("N2"));
        //    FindAndReplace("<TOT>", ri.RI_SUMS.ToString("N2"));
        //    FindAndReplace("<SAS>", ri.RI_SUMTAXSALARY.ToString("N2"));
        //    FindAndReplace("<TAX>", ri.RI_SUMTAXRMB.ToString("N2"));
        //    FindAndReplace("<PRE>", ((ri.RI_NEEDPACKREAL < 0) ? decimal.Zero : (decimal)ri.RI_NEEDPACKREAL).ToString("N2"));//已扣缴税额

        //    if (ri.RI_SUMTAXRMB < ri.RI_SUMTAXALREADYRMB + ri.RI_USEALL)
        //    {
        //        FindAndReplace("<CRE>", (ri.RI_SUMTAXRMB).ToString("N2")); //TODO
        //    }
        //    else
        //    {
        //        FindAndReplace("<CRE>", ((decimal)(ri.RI_SUMTAXALREADYRMB + ri.RI_USEALL)).ToString("N2")); //TODO
        //    }
        //    FindAndReplace("<NAME>", tp.TP_NAME);
        //    string sHolder = "!\"#$%&*+=|{}[]`~_/";
            
        //    object findtext;
             
        //    for (int i = 0; i < sHolder.Length; i++)
        //    {
        //        replacetext = tp.TP_IDNUMBER.Substring(i, 1);
        //        findtext = sHolder.Substring(i, 1);
        //        FindAndReplace(findtext, replacetext);
        //    }
        //    aDoc.Save();
                
        //}

        /// <summary>
        /// 将报表3数据写入word文件
        /// </summary>
        /// <param name="ri"></param>
        /// <param name="ai"></param>
        /// <param name="tp"></param>
        public void WriteData(REPORT_INFO ri, AGENT_INFO ai, TAX_PLAYER tp, decimal internalTotal, decimal externalTotal)
        {
            object replacetext;
            //replacetext = ai.AI_NAME;
            FindAndReplace("<Y>", ri.RI_TABLETIME.ToString("yyyy"));
            FindAndReplace("<M>", ri.RI_TABLETIME.ToString("MM"));
            FindAndReplace("<D>", ri.RI_TABLETIME.ToString("dd"));
            FindAndReplace("<YEAR>", ri.RI_AYNAME);

            FindAndReplace("<EM>", ai.AI_NAME);
            FindAndReplace("<TAXID>", ai.AI_TAXID);
            FindAndReplace("<INDUS>", ai.AI_INDUSTRY);
            FindAndReplace("<ADDR>", ai.AI_ADDRESS);
            FindAndReplace("<POSTCODE>", ai.AI_POSTCODE);
            FindAndReplace("<TEL>", ai.AI_TEL);

            /* Modified by cyq 20160331 */
            /************  Begin  *********/
            //FindAndReplace("<WIT>", "0");
            //FindAndReplace("<OUT>", ri.RI_SUMS.ToString("N2"));
            FindAndReplace("<WIT>", internalTotal.ToString("N2"));
            //FindAndReplace("<OUT>", externalTotal.ToString("N2"));
            FindAndReplace("<OUT>", (ri.RI_SUMS - internalTotal).ToString("N2"));
            /************  End  *********/

            FindAndReplace("<TOT>", ri.RI_SUMS.ToString("N2"));
            FindAndReplace("<SAS>", ri.RI_SUMTAXSALARY.ToString("N2"));
            FindAndReplace("<TAX>", ri.RI_SUMTAXRMB.ToString("N2"));
            FindAndReplace("<PRE>", ((ri.RI_NEEDPACKREAL < 0) ? decimal.Zero : (decimal)ri.RI_NEEDPACKREAL).ToString("N2"));//已扣缴税额

            if (ri.RI_SUMTAXRMB < ri.RI_SUMTAXALREADYRMB + ri.RI_USEALL)
            {
                FindAndReplace("<CRE>", (ri.RI_SUMTAXRMB).ToString("N2")); //TODO
            }
            else
            {
                FindAndReplace("<CRE>", ((decimal)(ri.RI_SUMTAXALREADYRMB + ri.RI_USEALL)).ToString("N2")); //TODO
            }
            FindAndReplace("<NAME>", tp.TP_NAME);
            string sHolder = "!\"#$%&*+=|{}[]`~_/";

            object findtext;

            for (int i = 0; i < sHolder.Length; i++)
            {
                replacetext = tp.TP_IDNUMBER.Substring(i, 1);
                findtext = sHolder.Substring(i, 1);
                FindAndReplace(findtext, replacetext);
            }
            aDoc.Save();

        }

        /************  End  *********/

        public void Close()
        {
            aDoc.Close(ref missing, ref missing, ref missing);
            
        }
    }
}
