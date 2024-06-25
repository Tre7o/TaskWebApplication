using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebApplication.Models;

namespace TaskSchedulerService.DB
{
    public class ServiceRepo
    {
        DatabaseService databaseService = DatabaseService.GetInstance();
        private string filepath = @"E:\C-Sharp-Projects\ASP Web apps\TaskWebApplication\TaskSchedulerService\ServiceLogs.txt";

        private void Log(string message)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filepath, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToShortTimeString()} - {message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("From TaskSchedulerService: " + ex.Message);
            }
        }

        public bool SaveIntoDB(ATask task)
        {
            try
            {
                using (SqlConnection con = databaseService.GetConnection())
                {
                    con.Open();
                    string insertTaskQuery = "INSERT INTO task(task_name,task_priority,task_deadline) VALUES(@value1,@value2,@value3)";
                    SqlCommand addingTaskToDB = new SqlCommand(insertTaskQuery, con);

                    addingTaskToDB.Parameters.AddWithValue("@value1", task.task_name);
                    addingTaskToDB.Parameters.AddWithValue("@value2", task.task_priority);
                    addingTaskToDB.Parameters.AddWithValue("@value3", task.task_deadline);

                    addingTaskToDB.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return false;
            }
            
        }
    }
}
