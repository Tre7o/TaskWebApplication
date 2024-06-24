using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
//using TaskWebApplication.Models;
using Newtonsoft.Json;

namespace QueueLibrary
{
    public static class LibraryTaskQueue
    {
        //public static bool Enqueue(ATask task, string queuePath)
        //{
        //    try
        //    {
        //        using (MessageQueue queue = new MessageQueue(queuePath))
        //        {
        //            string jsonTask = JsonConvert.SerializeObject(task);
        //            queue.Send(jsonTask, task.task_name); // Using TaskName as the label
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error
        //        return false;
        //    }
        //}

        //public static ATask Dequeue(string queuePath)
        //{
        //    try
        //    {
        //        using (MessageQueue queue = new MessageQueue(queuePath))
        //        {
        //            var message = queue.Receive(TimeSpan.FromSeconds(1));
        //            string jsonTask = message.Body.ToString();
        //            return JsonConvert.DeserializeObject<ATask>(jsonTask);
        //        }
        //    }
        //    catch (MessageQueueException mqex)
        //    {
        //        if (mqex.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
        //        {
        //            // Log error
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error
        //        return null;
        //    }
        //}

        //public static string GetTaskStatus(string taskName, string queuePath)
        //{
        //    try
        //    {
        //        using (MessageQueue queue = new MessageQueue(queuePath))
        //        {
        //            var enumerator = queue.GetMessageEnumerator2();
        //            while (enumerator.MoveNext())
        //            {
        //                var message = enumerator.Current;
        //                if (message.Label == taskName)
        //                {
        //                    string jsonTask = message.Body.ToString();
        //                    var task = JsonConvert.DeserializeObject<ATask>(jsonTask);
        //                    return task.task_status;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error
        //        Console.WriteLine(ex.Message);
        //    }
        //    return null;
        //}
    }
}
