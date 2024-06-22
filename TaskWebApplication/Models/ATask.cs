using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskWebApplication.Models
{
    public class ATask
    {
        public int task_id { get; set; }
        public string task_name { get; set; }
        public int task_priority { get; set; }
        public string task_status { get; set; }
        public DateTime task_deadline { get; set; }

        public ATask()
        {
        }

        public ATask(int task_id, string task_name, int task_priority, string task_status, DateTime task_deadline)
        {
            this.task_id = task_id;
            this.task_name = task_name;
            this.task_priority = task_priority;
            this.task_status = task_status;
            this.task_deadline = task_deadline;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}