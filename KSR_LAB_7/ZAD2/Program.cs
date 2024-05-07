using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ZAD2
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
                channel.QueueDeclare(
                queue: "message_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    string header1 = "-";
                    string header2 = "-";
                    if (ea.BasicProperties.Headers != null)
                    {
                        if (ea.BasicProperties.Headers.ContainsKey("header1"))
                        {
                            header1 = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["header1"]);
                        }
                        if (ea.BasicProperties.Headers.ContainsKey("header2"))
                        {
                            header2 = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["header2"]);
                        }
                    }



                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    // show message
                    Console.WriteLine("odebrana wiadomość:" + message);
                    // var header1_message = Encoding.UTF8.GetString(BitConverter.GetBytes(header2));
                    Console.WriteLine("z headerm 1:" + header1);

                    //var header2_message = Encoding.UTF8.GetString(BitConverter.GetBytes(header2));
                    Console.WriteLine("z headerm 2:" + header2);
                    
                     System.Threading.Thread.Sleep(2000);
                    channel.BasicAck(ea.DeliveryTag, false);
                    Console.WriteLine("wiadomość potwierdzona");
                };
                channel.BasicConsume(queue: "message_queue", autoAck: false, consumer: consumer);
                Console.ReadKey();
            }
        }
    }
}
