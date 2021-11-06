using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration; 
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string name = "MVCDemoDb")
        {
            name = "EmployeeDatabase";
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static List<T> Query<T>(string sql,T data)
        {
            IDbConnection connection = new SqlConnection(GetConnectionString());
            return connection.Query<T>(sql,data).AsList<T>();
        }
        public static List<T> LoadData<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(GetConnectionString());
            return connection.Query<T>(sql).AsList<T>();
        }

        public static int SaveData<T>(string sql,T Data)
        {
            IDbConnection connection = new SqlConnection(GetConnectionString());
            return connection.Execute(sql, Data);
        }
    }
}
