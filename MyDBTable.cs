using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

/*
 * add 和 edit可以像asp的dataset一样
    //add
    MyDBTable t = new MyDBTable(conn, "select * from test");
    //DataRow dr = t.Table.NewRow();
    DataRow dr = t.NewRow();
    dr["name"] = "selecttest";
    //t.Table.Rows.Add(dr);
    t.AddRow(dr);// ==> t.Rows.Add(dr);
    Response.Write("newdr.id=" + dr["id"] + "<br>");//可以得到自增id,前提:select 全部数据
    t.Update();
    Response.Write("newdr.id=" + dr["id"] + "<br>");//可以得到自增id

    //update
    MyDBTable t2 = new MyDBTable(conn, "select * from test where id=23 ");
    if (t2.RowCount > 0) {
        t2.Rows[0]["name"] = "new named";
        t2.Update();
    }

    //delete 
    MyDBTable t3 = new MyDBTable(conn, "select * from test where id=26 ");
    if (t3.RowCount > 0)
    {
        t3.Rows[0].Delete();
        t3.Update();
    }
  

 
 
 */

namespace com.kissmett.Common
{
    public class MyDBTable
    {

        SqlConnection _conn = null;
        string _tableName = "";

        SqlDataAdapter _adapter = null;
        DataSet _ds = new DataSet();
        DataTable _dt = null;

        SqlCommandBuilder _scb = null;

        private DataTable Table{
            get{return this._dt;}
            //set { this._dt = value; }
        }


        public DataRowCollection Rows {
            get { return this._dt.Rows; }
        }

        public int RowCount {
            get { return this._dt.Rows.Count; }
        }

        public MyDBTable(SqlConnection conn, string sql)
        {
            this._conn = conn;
            if (_conn.State != ConnectionState.Open) _conn.Open();
            this._tableName = "_tft_";// tableName;
            //string sql = "select * from [" + _tableName + "] ";
            this._adapter = new SqlDataAdapter(sql, _conn);

            _adapter.FillSchema(_ds, SchemaType.Source, _tableName);
            _adapter.Fill(_ds, _tableName);
            _dt = _ds.Tables[_tableName];
        
        }

        public DataRow NewRow() {
            return this._dt.NewRow();
        }

        public void AddRow(DataRow dr) {
            this._dt.Rows.Add(dr);
        }


        public void Update() {
            //DataRow dr = _dt.NewRow();
            //dr["name"] = "test";
            //_dt.Rows.Add(dr);
            //_scb = new SqlCommandBuilder(_adapter);

            _scb = new SqlCommandBuilder(_adapter);
            _adapter.Update(_ds, _tableName);
        }


    }
}
