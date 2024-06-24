using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TaskWebApplication.Services.Data.DB
{
    public class DBContext
    {
        private static DBContext dBInstance;
        private SqlConnection sqlConnection;
        string connectionString = "Server=SUUBIJOHNSON;Database=task_scheduler;User Id=SA;Password=#Trevknight8528;";

        private DBContext()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static DBContext GetInstance()
        {
            if (dBInstance == null)
            {
                dBInstance = new DBContext();
            }
            return dBInstance;
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

    }
}