using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskWebApplication.Models;

namespace TaskWebApplication.Services
{
    public class TaskQueue
    {
        public Queue<ATask> theTasks = new Queue<ATask>();
    }
}