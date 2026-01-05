//using RabbitMQ.Client;
//using System.Text;
//using System.Text.Json;


//namespace MultiShop.Catalog.Infrastructure.Messaging
//{
//    public class RabbitMqEventBus : IEventBus, IDisposable
//    {
//        private readonly IConnection _connection;

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

//                _connection = factory.CreateConnection();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public Task PublishAsync<T>(T @event) where T : class
//        {
//            Console.WriteLine("RabbitMQ PUBLISH START");

//            using var channel = _connection.CreateModel();

//            channel.ExchangeDeclare(
//                exchange: "catalog.events",
//                type: ExchangeType.Fanout,
//                durable: true
//            );

//            Console.WriteLine("Exchange declared");

//            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));

//            channel.BasicPublish(
//                exchange: "catalog.events",
//                routingKey: "",
//                basicProperties: null,
//                body: body
//            );

//            Console.WriteLine("RabbitMQ PUBLISH END");

//            return Task.CompletedTask;
//        }


//        public void Dispose()
//        {
//            _connection?.Dispose();
//        }
//    }
//}