using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ZAD1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                VirtualHost = "/",
                Port = 5672
            };

            using (var connection = factory.CreateConnection()) 
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("message_queue", false, false, false, null);
                var reply = channel.QueueDeclare().QueueName;
                var consumer = new EventingBasicConsumer(channel);

                var properties = channel.CreateBasicProperties();
                properties.ReplyTo = reply;
                var corrId = Guid.NewGuid().ToString();
                properties.CorrelationId = corrId;
                channel.BasicConsume(queue: reply, autoAck: true,consumer: consumer);

                channel.BasicQos(0, 1, false);
                for (int i = 0 ; i < 10; i++){
                    string message = "my message number: " + i;
                    var body = Encoding.UTF8.GetBytes(message);
                    properties.Headers = new Dictionary<string, object>();
                    properties.Headers.Add("header1", "value: "+i);
                    properties.Headers.Add("header2", "value: "+(i+i));
                    channel.BasicPublish("", "message_queue", properties, body);
                }

                Console.WriteLine("odpowiedz:-----------------------------");
                consumer.Received += (model, ea) =>
                {
                    if (ea.BasicProperties.CorrelationId == corrId)
                    {
                        var body = ea.Body.ToArray();
                        Console.WriteLine(Encoding.UTF8.GetString(body));
                    }
                };
                Console.ReadKey();

            }



        }
    }
}
