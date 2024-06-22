using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using TaskWebApplication.Models;

namespace TaskWebApplication.Services
{
    public class TaskQueue
    {
        private static readonly Lazy<TaskQueue> instance = new Lazy<TaskQueue>(() => new TaskQueue());

        private static ConcurrentQueue<ATask> queue = new ConcurrentQueue<ATask>();
        private ConcurrentDictionary<string, ATask> taskDict = new ConcurrentDictionary<string, ATask>();

        // Private constructor to prevent instantiation from outside
        private TaskQueue() { }

        // Public property to provide global access to the instance
        public static TaskQueue Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public bool Enqueue(ATask task)
        {
            task.task_status = "pending";
            queue.Enqueue(task);
            try
            {
                taskDict.TryAdd(task.task_name, task);
                Debug.WriteLine("Task in dict");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Task has not been added to dict");
                return false;
            }
        }

        public ATask Dequeue()
        {
            if (queue.TryDequeue(out ATask task))  // Remove task from the queue
            {
                return task;
            }
            return null;
        }

        public string GetTaskStatus(string taskName)
        {
            if (taskDict.TryGetValue(taskName, out ATask task))  // Retrieve task by name
            {
                return task.task_status;
            }
            return "task not found";
        }

        public void UpdateTaskStatus(string taskName, string status)
        {
            if (taskDict.TryGetValue(taskName, out ATask task))  // Retrieve task by name
            {
                task.task_status = status;  // Update task status
                taskDict[taskName] = task;  // Save updated task back to dictionary
            }
        }
    }
}
