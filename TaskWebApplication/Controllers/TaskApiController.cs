﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using TaskWebApplication.Models;
using TaskWebApplication.Services;
using TaskWebApplication.Services.Data;

namespace TaskWebApplication.Controllers
{
    public class TaskApiController : ApiController
    {
        private static readonly TaskQueue taskQueue = TaskQueue.Instance;
        TaskService taskService = new TaskService();

        // GET api/values?taskName=task1
        [HttpGet]
        [Route("api/TaskApi/status")]
        public IHttpActionResult GetTaskStatus(String taskName)
        {
            String taskStatus = taskQueue.GetTaskStatus(taskName);
            return Ok(taskStatus);
            //return recentlyAddedTasks;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            if (taskService.FetchTasksFromDB() != null)
            {
                return Ok(taskService.FetchTasksFromDB());
            }
            return Ok(false);
        }

        // to post task to queue
        // POST api/values
        [HttpPost]
        public IHttpActionResult Post([FromBody] ATask task)
        {         
            if (taskQueue.Enqueue(task))
            {
                return Ok("Task added");
            }
            return BadRequest("Failed to enqueue the task");
        }

        // to process the task from the queue to the db
        // POST api/values/process
        [HttpPost]
        [Route("api/TaskApi/process")]
        public IHttpActionResult ProcessTask()
        {
            bool isProcessed = taskService.ProcessTaskFromQueue();
            if (isProcessed)
            {
                return Ok("Task processed successfully");
            }
            return BadRequest("No task to process or failed to process");
        }

    }
}
