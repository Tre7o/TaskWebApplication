using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskWebApplication.Models;
using TaskWebApplication.Services.Data;

namespace TaskWebApplication.Controllers
{
    // controller for creating appropriate task view
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskView()
        {
            // return a taskview.cshtml - view maps to the action method name
            return View();
        }

        public ActionResult AddTask()
        {
            // return a taskview.cshtml - view maps to the action method name
            return View();
        }

        public ActionResult TaskResult(Task task)
        {
            //return "Results: "+task.task_name;
            TaskService taskService = new TaskService();
            task.task_status = "Pending";
            Boolean success = taskService.AddTaskService(task);

            if (success)
            {
                return View("ViewTask", task);
            }
            else
            {
                return View("TaskFail");
            }
        }

        //public string Welcome(string name, int numOfTimes = 1)
        //{
        //    return "hello "+ name +"Number of times = " + numOfTimes;
        //}

        //public string Welcome2(string name, int ID = 1)
        //{
        //    return "hello " + name + " ID = " + ID;
        //}

        //public string PrintTask()
        //{
        //    return "<h2>Welcome</h2><p>You printed a task</p>";
        //}

        //public string Play()
        //{
        //    return "<h2>Let's play</h2><p>You played a task</p>";
        //}
    }
}