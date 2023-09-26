using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMq.Common.Services
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
        public void SendMessage<T>(T message, string exchange, string queue);
    }


    public class RabbitMqService : IRabbitMqService
    {

        ConnectionFactory connection = new ConnectionFactory()
        {

            HostName = "localhost",
            Port = 5672,
            UserName = "oom",
            Password = "Password"

        };

        public IConnection CreateChannel()
        {

            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }

        public void SendMessage<T>(T message, string exchange, string queue)
        {
       
                //Create the RabbitMQ connection using connection factory details as i mentioned above
                using var model = connection.CreateConnection();
                //Here we create channel with session and model
                using
                var channel = model.CreateModel();

                //declare the queue after mentioning name and a few property related to that
                channel.QueueDeclare(queue, exclusive: false);
                //Serialize the message

                var json = JsonConvert.SerializeObject(message);

                var body = Encoding.UTF8.GetBytes(json);
                //put the data on to the product queue

                //สำคัญ exchange 

                channel.BasicPublish(exchange: exchange, routingKey: queue, basicProperties: null, body: body);
              
        }
    }
}
