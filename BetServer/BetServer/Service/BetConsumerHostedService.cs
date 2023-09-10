using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BetServer.Service
{
    public class BetConsumerHostedService : BackgroundService
    {
        private readonly IModel _channel;

        public BetConsumerHostedService(IModel channel)
        {
            _channel = channel;

            // Declare an exchange
            _channel.ExchangeDeclare("bet_exchange", "direct");

            // Declare a queue
            _channel.QueueDeclare(queue: "bet_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Bind the queue to the exchange with multiple routing keys
            _channel.QueueBind("bet_queue", "bet_exchange", "routingKey1");
            _channel.QueueBind("bet_queue", "bet_exchange", "routingKey2");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received bet with routing key {ea.RoutingKey}: {message}");
            };
            _channel.BasicConsume(queue: "bet_queue",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
