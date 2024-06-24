using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskWebApplication.Controllers;
using TaskWebApplication.Models;

namespace TaskWebApplication.Services.Data
{
    public class TaskService
    {
        TaskRepo taskRepo = new TaskRepo();
        private static readonly TaskQueue taskQueue = TaskQueue.Instance;

        // to get tasks from queue and store it into a database
        public bool ProcessTaskFromQueue()
        {
            ATask task = taskQueue.Dequeue();
            if (task != null)
            {
                return taskRepo.SaveTask(task);
            }
            return false;
        }

        public List<ATask> FetchTasksFromDB()
        {
            return taskRepo.RetrieveTasks();
        }
    }
}