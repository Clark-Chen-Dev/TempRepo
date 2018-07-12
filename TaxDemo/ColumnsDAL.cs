using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TaxDemo
{
    /// <summary>
    /// 保留原始薪资的系统表SALARY_IMPORT需要使用原始SQL方式管理，可增加列，增加数据
    /// </summary>
    class ColumnsDAL
    {

        private SqlConnection sqlcn = null;
        public void OpenConnection(string constr)
        {
            sqlcn = new SqlConnection();
            sqlcn.ConnectionString = constr;
            sqlcn.Open();
        }
        public void CloseConnection()
        {
            sqlcn.Close();
        }

        /// <summary>
        /// 增加列
        /// </summary>
        /// <param name="ci"></param>
        public void InsertCol(COLUMNS_INFO ci)
        {

            string sql = string.Empty;
            if(ci.CI_TYPEID == 0)
            {
                sql = string.Format("ALTER TABLE SALARY_IMPORT ADD {0} nvarchar(50)  ", ci.CI_COLNAME); //MONEY ==> nvarchar(50)
            }
            else
            {
                sql = string.Format("ALTER TABLE SALARY_IMPORT ADD {0} nvarchar(50) ", ci.CI_COLNAME);
            }
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlcn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 获取所有列
        /// </summary>
        /// <returns></returns>
        public DataTable GetColsAsDataTable()
        {
            DataTable inv = new DataTable();
            string sql = "";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlcn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                inv.Load(dr);
                dr.Close();
            }
            return inv;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public bool WriteRawData(List<string> data, string cols)
        {
            string sql = string.Format("INSERT INTO SALARY_IMPORT ({0}) values ({1})", cols, string.Join(",",data));

            using (SqlCommand cmd = new SqlCommand(sql, this.sqlcn))
            {
                cmd.ExecuteNonQuery();
            }
            return true;
        }
    }

    
    
}
