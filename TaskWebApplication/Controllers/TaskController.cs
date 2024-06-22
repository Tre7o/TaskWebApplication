using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskWebApplication.Controllers;
using TaskWebApplication.Models;
using TaskWebApplication.Services.Data;

namespace TaskWebApp.Controllers
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

        public ActionResult TaskResult(TaskWebApplication.Models.ATask task)
        {
            //return "Results: "+task.task_name;
            //TaskService taskService = new TaskService();
            //task.task_status = "Pending";
            //Boolean success = taskService.AddTaskService(task);

            //if (success)
            //{
            //    return View("ViewTask", task);
            //}
            //else
            //{
            //    return View("TaskFail");
            //}

            // creating an instance of HttpClient
            using (var client = new HttpClient())
            {
                // base address
                client.BaseAddress = new Uri("https://localhost:44393/api/");

                // serializing task object to json
                var json = JsonConvert.SerializeObject(task);

                // creating a string content object
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // sending the post request to the ApiController
                var response = client.PostAsync("TaskApi/TaskResult", content).Result;

                // checking the response status code
                if (response.IsSuccessStatusCode)
                {
                    // task created successfully
                    return View("ViewTask", task);
                }
                else
                {
                    // api failed
                    return View("ApiErrorView");
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
}