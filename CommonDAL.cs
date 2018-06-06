using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace com.kissmett.Common
{
    public class CommonDAL
    {
        static string conString = Globals.ConnectionString;
        SqlConnection conn = null;

        public CommonDAL(SqlConnection conn) {
            this.conn = conn;
        }

        public  int ExecSQL(string sql)
        {
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }

        public DataRow GetDR(string sql) { 
            
            DataSet ds = GetDS(sql);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return dr;
        }
        
        

        public  DataSet GetDS(string sql)
        {
            return SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
        }
 


        public  string GetFieldValueStringBySQL( string sql, string field)
        {
            DataSet ds = GetDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][field].ToString();
            }
            return "";
        }
         

    }
}
