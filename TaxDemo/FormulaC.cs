using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;

namespace TaxDemo
{
    /// <summary>
    /// 工具类，提供字符串公式参数化计算功能
    /// </summary>
    class FormulaC
    {
        private static List<string> ExtractFromString(string text, string start, string end)
        {
            List<string> Matched = new List<string>();
            int index_start = 0, index_end = 0;
            bool exit = false;
            while (!exit)
            {
                index_start = text.IndexOf(start);
                index_end = text.IndexOf(end);
                if (index_start != -1 && index_end != -1)
                {
                    Matched.Add(text.Substring(index_start + start.Length, index_end - index_start - start.Length));
                    text = text.Substring(index_end + end.Length);
                }
                else
                    exit = true;
            }
            return Matched;
        }

        public static bool IsValid(string f)
        {
            bool result = false;
            try
            {
                Expression ep = new Expression(f);

                if (!ep.HasErrors())
                {
                    List<string> t = ExtractFromString(ep.ParsedExpression.ToString(), "[", "]");
                    foreach (var v in t)
                    {
                        ep.Parameters[v] = 1;
                    }

                    var x = ep.Evaluate();
                    result = true;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public static string GetParas(string f)
        {
            string result=string.Empty;
            Expression ep = new Expression(f);

            if (!ep.HasErrors())
            {
                List<string> t = ExtractFromString(ep.ParsedExpression.ToString(), "[", "]");
                result =  String.Join(",", t.Select(a => a));
            }

            return result;
        }

        public static string GetFormula(string f)
        { 
            string s = string.Empty;

            Expression ep = new Expression(f);

            if (!ep.HasErrors())
            {
                s  = ep.ParsedExpression.ToString();
            }

            return s;
        }

        public static decimal GetCal(string f,List<string> args)
        {
            
            decimal result = 0;

            Expression ep = new Expression(f);
            if (!ep.HasErrors())
            {
                List<string> t = ExtractFromString(ep.ParsedExpression.ToString(), "[", "]");
                foreach (var v in t)
                {
                    string tmp = args[SysUtil.GetExcelColumnIntValue(v) - 1].Trim();
                    if ( tmp== string.Empty)
                    {
                        ep.Parameters[v] = 0;
                    }
                    else
                    {
                        ep.Parameters[v] = Convert.ToDecimal(tmp);
                    }
                }

                return Convert.ToDecimal(ep.Evaluate());
            }
            else
            {
                throw new Exception("公式计算错误");
            }

            return result;
            
        }
    }
}
