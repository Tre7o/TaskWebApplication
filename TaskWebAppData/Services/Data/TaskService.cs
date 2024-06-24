using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskWebApplication.Models;

namespace TaskWebApplication.Services.Data
{
    public class TaskService
    {
        TaskRepo taskRepo = new TaskRepo();

        public bool AddTaskService(ATask task)
        {
            return taskRepo.SaveTask(task);
        }
    }
}