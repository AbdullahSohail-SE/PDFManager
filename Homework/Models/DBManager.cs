using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Homework.Models
{
    public class DBManager
    {
        public static int InsertData(string query)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["PDF_Connect"].ConnectionString);
            using (var cmd = new SqlCommand(query, con))
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static SqlDataReader GetData(string query)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["PDF_Connect"].ConnectionString);
            using (var cmd = new SqlCommand(query, con))
            {
                con.Open();
                return cmd.ExecuteReader();
            }
        }
    }
}