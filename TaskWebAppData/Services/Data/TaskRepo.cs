using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using TaskWebApplication.Models;
using System.ComponentModel;
using System.Data.Common;

namespace TaskWebApplication.Services.Data
{
    public class TaskRepo
    {
        private static TaskRepo _instance;
        public SqlConnection sqlConnection;
        string connectionString = "Server=SUUBIJOHNSON;Database=task_scheduler;User Id=SA;Password=#Trevknight8528;";

        public static TaskRepo Instance()
        {
                
                    if (_instance == null)
                    {
                        _instance = new TaskRepo();
                    }
              
            return _instance;
        }

        public bool ConnectToDB()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        internal bool SaveTask(ATask task)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    sqlConnection.Open();
                    string insertTaskQuery = "INSERT INTO task(task_name,task_priority,task_deadline) VALUES(@value1,@value2,@value3)";
                    SqlCommand addingTaskToDB = new SqlCommand(insertTaskQuery, sqlConnection);

                    addingTaskToDB.Parameters.AddWithValue("@value1", task.task_name);
                    addingTaskToDB.Parameters.AddWithValue("@value2", task.task_priority);
                    addingTaskToDB.Parameters.AddWithValue("@value3", task.task_deadline);

                    addingTaskToDB.ExecuteNonQuery();

                    sqlConnection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}