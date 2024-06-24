using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using TaskWebApplication.Models;
using System.ComponentModel;
using System.Data.Common;
using TaskWebApplication.Services.Data.DB;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TaskWebApplication.Services.Data
{
    public class TaskRepo
    {
        DBContext dbContext = DBContext.GetInstance();   

        internal bool SaveTask(ATask task)
        {
            try
            {
                using (SqlConnection sqlConnection = dbContext.GetConnection())
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
                Debug.WriteLine("From saveTasks: "+e.Message);
                return false;
            }
        }

        internal List<ATask> RetrieveTasks()
        {
            try
            {
                using (SqlConnection sqlConnection = dbContext.GetConnection())
                {

                    sqlConnection.Open();
                    string retrieveTasksQuery = "SELECT * FROM task";
                    SqlCommand getTaskFromDB = new SqlCommand(retrieveTasksQuery, sqlConnection);

                    List<ATask> tasks = new List<ATask>(); //store the tasks that were returned from db

                    // Execute the command and retrieve the data
                    using (SqlDataReader reader = getTaskFromDB.ExecuteReader())
                    {
                        // Check if there are rows returned
                        if (reader.HasRows)
                        {
                            // Loop through the rows
                            while (reader.Read())
                            {

                                ATask task = new ATask();
                                // Access the data in each column by index or name
                                task.task_id = reader.GetInt32(0);
                                task.task_name = reader.GetString(1);
                                task.task_priority = reader.GetInt32(2);
                                task.task_deadline = reader.GetDateTime(3);

                                //Add task to the list
                                tasks.Add(task);

                            }
                        }
                    }

                  
                    sqlConnection.Close();
                    return tasks;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("From retrieveTasks method: "+e.Message);
                return null;
            }
        }

    }
}