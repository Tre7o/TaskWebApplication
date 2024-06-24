using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskWebApplication.Models;
using TaskWebApplication.Services.Interfaces;

namespace TaskWebApplication.Services
{
    public class TaskQueue : IObservable
    {
        private static readonly Lazy<TaskQueue> instance = new Lazy<TaskQueue>(() => new TaskQueue());

        private static ConcurrentQueue<ATask> queue = new ConcurrentQueue<ATask>();
        private static ConcurrentDictionary<string, ATask> taskDict = new ConcurrentDictionary<string, ATask>();

        private List<IObserver> observers = new List<IObserver>();

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
            NotifyObservers(task); // Notify observers of status change
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
                task.task_status = "executed";
                NotifyObservers(task); // Notify observers of status change
                return task;
            }
            return null;
        }

        public List<ATask> GetTasksInQueue()
        {
            return queue.ToList();
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
                NotifyObservers(task); // Notify observers of status change
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(ATask task)
        {
            foreach (var observer in observers)
            {
                observer.Update(task);
            }
        }
    }
}
