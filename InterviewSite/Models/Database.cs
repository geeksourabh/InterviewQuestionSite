﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewSite.Models
{
    public class Database
    {
        public SqlConnection Sql_Connection{ get; set; }
        public Database()
        {
            Sql_Connection = new SqlConnection()
            {
                ConnectionString = "Server=" + ConfigurationManager.AppSettings["Server"] + ";database=" + ConfigurationManager.AppSettings["Database"] + ";UID=" + ConfigurationManager.AppSettings["UID"] + ";Password=" + ConfigurationManager.AppSettings["pwd"]
            };
        }
        
        public void RunProcedure(string ProcedureName,SqlParameter[] param,out DataSet ds)
        {
            SqlConnection con = Sql_Connection;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCountries", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                if (!object.Equals(param, null))
                {
                    cmd.Parameters.AddRange(param);
                }
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adpt.Fill(ds);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }
        }
    }
}