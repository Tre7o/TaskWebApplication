﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using TaskWebApplication.Controllers;
using TaskWebApplication.Models;
using TaskWebApplication.Services.Data;
using TaskWebApplication.Services.Interfaces;
using TaskWebApplication.Services;
using System.Threading.Tasks;
using System.Net;

namespace TaskWebApp.Controllers
{
    // controller for creating appropriate task view
    public class TaskController : Controller, IObserver
    {

        private static readonly TaskQueue taskQueue = TaskQueue.Instance;

        public TaskController()
        {
            taskQueue.RegisterObserver(this); // Register as observer when TaskController is created
        }

        public void Update(ATask task)
        {
            // Update the task status
            Task = new ATask();
            Debug.WriteLine(task.task_status);
        }

        public ATask Task { get; set; }

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pending()
        {
            List<ATask> aTasks = new List<ATask>();
            aTasks = taskQueue.GetTasksInQueue();
            return View(aTasks);    
        }

        public async Task<ActionResult> TasksView()
        {
            // return a taskview.cshtml - view maps to the action method name

            List<ATask> tasks = new List<ATask>();

            using (var client = new HttpClient())
            {        
                // base address
                client.BaseAddress = new Uri("https://localhost:44393/api/taskapi");

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    tasks = JsonConvert.DeserializeObject<List<ATask>>(result);
                    Debug.WriteLine(tasks);
                }
                else
                {
                    Debug.WriteLine("No tasks found");
                }

            }
            return View(tasks);
        }

        public ActionResult AddTask()
        {
            // return a taskview.cshtml - view maps to the action method name
            return View();
        }

        public async Task<ActionResult> CheckStatus(string taskName)
        {
            using (var client = new HttpClient())
            {
                if (string.IsNullOrEmpty(taskName))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Task name cannot be null or empty");
                }

                client.BaseAddress = new Uri("https://localhost:44393/api/");

                HttpResponseMessage response = await client.GetAsync($"TaskApi/status?taskName={taskName}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    string taskStatus = JsonConvert.DeserializeObject<string>(json);
                    ViewBag.TaskStatus = taskStatus;
                    return View("TaskStatus");
                }
                else
                {
                    return new HttpStatusCodeResult(response.StatusCode, "Error fetching task status");
                }
            }
        }

        public ActionResult TaskResult(TaskWebApplication.Models.ATask task)
        {
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
                    task.task_status = "pending";
                    // task created successfully
                    return View("ViewTask", task);
                }
                else
                {
                    // api failed
                    return View("ApiErrorView");
                }
            }
        }

        
    }
}