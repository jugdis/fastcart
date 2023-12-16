using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace project_Ecommerce.Models
{
    public class dblayer
    {
        SqlConnection connection;
        public dblayer() { 
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["webcon"].ConnectionString);
        }

        public int ExcuteDML(string proc, SqlParameter[] parameters)
        {
            SqlCommand command= new SqlCommand(proc,connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (SqlParameter p in parameters)
            {
                if(p.Value != null)
                {
                    command.Parameters.Add(p);
                }

            }
            connection.Open();
            int r= command.ExecuteNonQuery();
            connection.Close();
            return r;
        }

        //executeNonQuerry():int return type: insert ,update,del
        //ExecuteReader(): return multiple number of rows
        //ExecuteSclar():return one value

        public DataTable ExcuteGetTable(string proc, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(proc, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter p in parameters) 
            {
                if(p.Value != null)
                {
                    command.Parameters.Add(p);
                }
            }
            DataTable dt= new DataTable();
            SqlDataAdapter adapter= new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }


        public object ExcuteScalar(string proc, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(proc, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter p in parameters)
            {
                if (p.Value != null)
                {
                    command.Parameters.Add(p);
                }
            }
            connection.Open ();
            object r= command.ExecuteScalar();
            connection.Close();
            return r;
        }
    }
}