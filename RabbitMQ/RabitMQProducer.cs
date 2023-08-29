using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(T message, string ipAddress, string queue);
    }
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendMessage<T>(T message, string ipAddress, string queue)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = ipAddress
            };

            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare(queue, exclusive: false);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue

            channel.BasicPublish(exchange: "", routingKey: queue, body: body);
        }
    }
}
