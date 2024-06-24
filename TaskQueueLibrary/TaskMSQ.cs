using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
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
    }
}
