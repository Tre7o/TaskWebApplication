using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using TaskQueueLibrary;
using TaskWebApplication.Models;
using TaskWebApplication.Services.Data.DB;

namespace TaskSchedulerService
{
    public partial class ProcessTaskService : ServiceBase
    {

        private DBContext dbContext = DBContext.GetInstance();
        private TaskMSQ taskMSQ = new TaskMSQ();
        private Timer timer;
        private string filepath = @"E:\C-Sharp-Projects\ASP Web apps\TaskWebApplication\TaskSchedulerService\ServiceLogs.txt";

        public ProcessTaskService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log("Service started successfully");

            // Set up a timer to trigger the task processing every minute
            timer = new Timer(ProcessQueue, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        protected override void OnStop()
        {
            Log("TaskSchedulerService stopped.");
            timer?.Dispose();
        }

        private void ProcessQueue(object state)
        {
            try
            {
                Log("Checking for tasks in the queue.");
                ATask task = taskMSQ.ReceiveMessageAsTask();

                if (task != null)
                {
                    Log($"Processing task: {task.task_name}, {task.task_priority}, {task.task_deadline}");
                    SaveTaskToDatabase(task);
                }
                else
                {
                    Log("No tasks in the queue.");
                }
            }
            catch (Exception ex)
            {
                Log($"Error processing queue: {ex.Message}");
            }
        }

        private void SaveTaskToDatabase(ATask task)
        {
            try
            {
                using (SqlConnection sqlConnection = dbContext.GetConnection())
                {
                    sqlConnection.Open();
                    string insertTaskQuery = "INSERT INTO task(task_name, task_priority, task_deadline) VALUES(@value1, @value2, @value3)";
                    SqlCommand addingTaskToDB = new SqlCommand(insertTaskQuery, sqlConnection);

                    addingTaskToDB.Parameters.AddWithValue("@value1", task.task_name);
                    addingTaskToDB.Parameters.AddWithValue("@value2", task.task_priority);
                    addingTaskToDB.Parameters.AddWithValue("@value3", task.task_deadline);

                    addingTaskToDB.ExecuteNonQuery();
                    sqlConnection.Close();
                    Log("Task added to database successfully.");
                }
            }
            catch (Exception ex)
            {
                Log("Error adding to db: " + ex.Message);
            }
        }

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
    }
}

