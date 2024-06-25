using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerService.DB
{
    public class DatabaseService
    {
        private static DatabaseService instance;
        private SqlConnection sqlConnection;
        private string _connectionString;

        private DatabaseService()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static DatabaseService GetInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseService();
            }
            return instance;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
