//using Microsoft.Extensions.Configuration;
//using MultiShop.Services.Messaging;
//using RabbitMQ.Client;
//using System.Text;
//using System.Text.Json;


//namespace MultiShop.Services.Stock.Infrastructure.Messaging
//{
//    public class RabbitMqEventBus : IEventBus, IDisposable
//    {
//        private IConnection _connection;

//        public RabbitMqEventBus(IConfiguration configuration)
//        {
//            try
//            {
//                var portStr = configuration["RabbitMQ:Port"];
//                var port = string.IsNullOrEmpty(portStr) ? 15672 : int.Parse(portStr);
//                var host = configuration["RabbitMQ:Host"];

//                var factory = new ConnectionFactory
//                {
//                    HostName = host,
//                    Port = 5673,
//                    UserName = configuration["RabbitMQ:Username"],
//                    Password = configuration["RabbitMQ:Password"]
//                };

//                _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public async Task PublishAsync<T>(T @event) where T : class
//        {
//            Console.WriteLine("RabbitMQ PUBLISH START");

//            using var channel = await _connection.CreateChannelAsync();

//            await channel.ExchangeDeclareAsync(
//                 exchange: "catalog.events",
//                 type: ExchangeType.Fanout,
//                 durable: true
//             );

//            Console.WriteLine("Exchange declared");

//            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));

//            await channel.BasicPublishAsync(
//                exchange: "catalog.events",
//                routingKey: "",
//                mandatory: true,
//                body: body
//            );

//            Console.WriteLine("RabbitMQ PUBLISH END");

//        }


//        public void Dispose()
//        {
//            _connection?.Dispose();
//        }
//    }
//}