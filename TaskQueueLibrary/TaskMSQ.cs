using System;
using System.Messaging;
using TaskWebApplication.Models;

namespace TaskQueueLibrary
{
    public class TaskMSQ
    {
        private static readonly string queuePath = @"suubijohnson\private$\taskqueue";

        public TaskMSQ()
        {
            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);
            }
        }

        public void SendTaskAsMessage(ATask task)
        {
            using (MessageQueue messageQueue = new MessageQueue(queuePath))
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ATask) });
                messageQueue.Send(task);
            }
        }

        public ATask ReceiveMessageAsTask()
        {
            using (MessageQueue messageQueue = new MessageQueue(queuePath))
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ATask) });
                Message message = messageQueue.Receive();
                return (ATask)message.Body;
            }
        }

        public string GetTaskStatus(string taskName)
        {
            using (MessageQueue messageQueue = new MessageQueue(queuePath))
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ATask) });
                foreach (Message message in messageQueue.GetAllMessages())
                {
                    ATask retrievedTask = (ATask)message.Body;
                    if (retrievedTask.task_name == taskName)
                    {
                        return "Pending";
                    }
                    else
                    {
                        return "Executed";
                    }
                }
            }
            return "No task found";
        }
    }
}


