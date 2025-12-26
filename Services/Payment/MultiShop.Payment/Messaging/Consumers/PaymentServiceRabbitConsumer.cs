//using MultiShop.Shared.Events;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;
//using System.Text.Json;


//namespace MultiShop.Services.Messaging
//{
//    public class PaymentServiceRabbitConsumer : BackgroundService
//    {
//        private readonly IConfiguration _configuration;
//        private readonly IServiceProvider _serviceProvider;

//        private IConnection _connection;
//        private IModel _channel;

//        public PaymentServiceRabbitConsumer(
//         IConfiguration configuration, IServiceProvider serviceProvider)
//        {
//            _configuration = configuration;

//            _serviceProvider = serviceProvider;
//        }


//        private void InitializeRabbitMq()
//        {

//            var portStr = _configuration["RabbitMQ:Port"];
//            var port = string.IsNullOrEmpty(portStr) ? 15672 : int.Parse(portStr);
//            var factory = new ConnectionFactory
//            {
//                HostName = _configuration["RabbitMQ:Host"],
//                Port = 5673,
//                UserName = _configuration["RabbitMQ:Username"],
//                Password = _configuration["RabbitMQ:Password"],
//                DispatchConsumersAsync = true
//            };

//            _connection = factory.CreateConnection();






//            _channel = _connection.CreateModel();


//            _channel.ExchangeDeclare(
//                exchange: "catalog.events",
//                type: ExchangeType.Fanout,
//                durable: true
//            );


//            _channel.QueueDeclare(
//                queue: "stock.reserve",
//                durable: true,
//                exclusive: false,
//                autoDelete: false
//            );


//            _channel.QueueBind(
//                queue: "stock.reserve",
//                exchange: "catalog.events",
//                routingKey: ""
//            );

//            // Fair dispatch
//            _channel.BasicQos(0, 1, false);
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            InitializeRabbitMq();

//            var consumer = new AsyncEventingBasicConsumer(_channel);

//            consumer.Received += async (sender, ea) =>
//            {

//                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

//                var @event = JsonSerializer.Deserialize<PaymentServiceRequestedEvent>(message);

//                if (@event != null)
//                {
//                    using var scope = _serviceProvider.CreateScope();
//                    var handler = scope.ServiceProvider
//                        .GetRequiredService<PaymentServiceRequestedConsumer>();

//                    await handler.HandleAsync(@event);


//                }

//                _channel.BasicAck(ea.DeliveryTag, false);


//            };

//            _channel.BasicConsume(
//                queue: "test.catalog.queue",
//                autoAck: false,
//                consumer: consumer
//            );
//            await Task.Delay(Timeout.Infinite, stoppingToken);

//        }

//        public override void Dispose()
//        {
//            _channel?.Close();
//            _connection?.Close();
//            base.Dispose();
//        }
//    }
//}
