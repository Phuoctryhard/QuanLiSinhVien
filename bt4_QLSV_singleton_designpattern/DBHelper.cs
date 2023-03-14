using bt4_QLSV_singleton_designpattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bt4_QLSV_singleton_designpattern
{
    public class DBHelper
    {
        private static DBHelper _InStance;
        private SqlConnection sqlcon = null;
        public static DBHelper getInStance
        {
            get
            {

                if (_InStance == null)              
                    _InStance = new DBHelper(@"Data Source=DESKTOP-M4UKRGH\SQLEXPRESS01;Initial Catalog=QLSV_bt4;Integrated Security=True");
                    return _InStance;
                
            }
            private set {
            }

        }
        private DBHelper(string strcon)
        {
            sqlcon = new SqlConnection(strcon);
        }   
        public DataTable getInfo(string query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, sqlcon);
            sqlcon.Open();
            sqlAdapter.Fill(dt);
            sqlcon.Close();
            return dt;
        }
        // ham dung vs count 
        public int ExecuteScale(string query)
        {
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            sqlcon.Open();
            int result = (int)sqlcmd.ExecuteScalar();
            sqlcon.Close();
            return result;  
        }
        public void ExectuteNonQuery(string query)
        {
        
            SqlCommand sqlcmd = new SqlCommand(query,sqlcon);
            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();  
        }
        // Sort 
        public void ExecuteNonQuery(string query, SqlParameter[] list)
        {
            sqlcon.Open();
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
          
            if (list != null)
            {
                sqlcmd.Parameters.AddRange(list);
            }
            sqlcmd.ExecuteNonQuery();

            sqlcon.Close();

        }
        public DataTable getInfo(string query, SqlParameter[] list)
        {
            DataTable dt = new DataTable();
            // thuc thu truy van 
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            if (list != null)
            {
                cmd.Parameters.AddRange(list);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            sqlcon.Open();
            adapter.Fill(dt);
            sqlcon.Close();
            return dt;
        }

    }
}