using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxDemo
{
    /// <summary>
    /// 常用工具类
    /// </summary>
    class SysUtil
    {
        static int currentID;
        static decimal minus=0;

        /// <summary>
        /// 获取当前年度
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentYear()
        {
            int result = 0;

            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                AGENT_YEAR ay = db.AGENT_YEARs.SingleOrDefault(v => v.AY_ISCURRENT == true);
                if (ay != null)
                {
                    result = ay.AY_NAME;
                }
                return result;

            }
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        public static int CurrentUserID()
        {
            return currentID;
        }

        /// <summary>
        /// 设置当前登录用户
        /// </summary>
        /// <param name="userid"></param>
        public static void setCurrentUserID(int userid)
        {
            currentID = userid;
        }

        /// <summary>
        /// 查询公司ID
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns></returns>
        public static int GetAgentID(string agentName)
        {
            int result =-1;

            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                AGENT_INFO ay = db.AGENT_INFOs.SingleOrDefault(v => v.AI_NAME == agentName);
                if (ay != null)
                {
                    result = ay.AI_ID;
                }
                return result;

            }
        }

        /// <summary>
        /// 查询公司国别ID
        /// </summary>
        /// <param name="agentName"></param>
        /// <param name="agentCName"></param>
        /// <returns></returns>
        public static int GetAgentCID(string agentName,string agentCName)
        {
            int result = -1;

            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                var ay = db.v_AgentCountries.SingleOrDefault(v => v.AI_NAME == agentName && v.AC_NAME == agentCName);
                if (ay != null)
                {
                    result = ay.AC_ID;
                }
                return result;

            }
        }

        internal static string GetTypeName(short p)
        {
            string result = string.Empty;
            if (p == 0)
            {
                result =  "数字";
            }
            else if (p == 1)
            {
                result = "文本";
            }
            else if (p == 2)
            {
                result =  "日期";
            }

            return result;
        }

        /// <summary>
        /// 将数字转为EXCEL列号
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        /// <summary>
        /// EXCEL列号转为数字
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int GetExcelColumnIntValue(string  columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("columnName");

            columnName = columnName.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }

        /// <summary>
        /// 获取纳税人ID
        /// </summary>
        /// <param name="ACID"></param>
        /// <param name="playerName"></param>
        /// <param name="playerID"></param>
        /// <returns></returns>
        internal static bool GetPlayerID(int ACID, string playerName, ref int  playerID)
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
                var ay = db.TAX_PLAYERs.SingleOrDefault(v => v.TP_ACID == ACID && v.TP_NAME == playerName);
                if (ay != null)
                {
                    playerID = ay.TP_ID;

                    return true;
                }
                return false;

            }
        }

        /// <summary>
        /// 获取纳税人ID
        /// </summary>
        /// <param name="ACID"></param>
        /// <param name="playerName"></param>
        /// <returns></returns>
        internal static int GetPlayerID(int ACID, string playerName)
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
                var ay = db.TAX_PLAYERs.SingleOrDefault(v => v.TP_ACID == ACID && v.TP_NAME == playerName);
                if (ay != null)
                {
                    
                    return ay.TP_ID;
                }
                return -1;

            }
        }

        /// <summary>
        /// 获取默认减费用
        /// </summary>
        /// <returns></returns>
        internal static decimal GetMinus()
        {
            if (minus != 0) return minus;

            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                //db.Connection.ConnectionString = Cryptogram.DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
                var ay = db.OPTIONs.SingleOrDefault(v => v.O_KEY=="MINUS");
                if (ay != null)
                {
                    minus = Convert.ToDecimal(ay.O_VALUE);
                }
                return minus;

            }
        }

        /// <summary>
        /// 获取税率等级
        /// </summary>
        /// <param name="p"></param>
        /// <param name="minus"></param>
        /// <param name="isBonus"></param>
        /// <returns></returns>
        public static TAX_RATE GetTaxRate(decimal p,decimal minus,bool isBonus=false)
        {
            decimal tmp = p;
            if (!isBonus)
            {
                 tmp = tmp - minus;
            }
            
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                return db.TAX_RATEs.Where(a => tmp > a.TR_LOW &&  tmp <= a.TR_HIGH ).SingleOrDefault();
            }
        }

        public static TAX_RATE GetTaxRate(int taxid)
        {
            
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                return db.TAX_RATEs.Where(a => a.TR_ID==taxid).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取公司国别信息
        /// </summary>
        /// <param name="acid"></param>
        /// <returns></returns>
        public static v_AgentCountry GetACInfo(int acid)
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                return db.v_AgentCountries.SingleOrDefault(a=>a.AC_ID==acid);
            }
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="acid"></param>
        /// <returns></returns>
        public static AGENT_INFO GetAgentInfo(int acid)
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                return db.AGENT_INFOs.SingleOrDefault(ai => ai.AI_ID == (db.AGENT_COUNTRies.SingleOrDefault(a => a.AC_ID == acid)).AC_AIID);
            }

        }

        /// <summary>
        /// 获取纳税人信息
        /// </summary>
        /// <param name="tpid"></param>
        /// <returns></returns>
        public static TAX_PLAYER GetTaxPlayer(int tpid)
        {
            using (TAXDBDataContext db = new TAXDBDataContext())
            {
                return db.TAX_PLAYERs.SingleOrDefault(ai => ai.TP_ID == tpid);
            }
        }
    }
}
