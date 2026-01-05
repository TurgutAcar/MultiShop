//using System.Text;
//using System.Text.Json;
//using Elastic.Clients.Elasticsearch;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Configuration;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using MultiShop.Shared.Events;
//namespace Catalog.ElasticSyncWorker;

//public class ElasticSyncWorker : BackgroundService
//{
//    private readonly ILogger<ElasticSyncWorker> _logger;
//    private readonly IConfiguration _configuration;
//    private readonly ElasticsearchClient _elasticClient;

//    private IConnection _connection;
//    private IModel _channel;

//    public ElasticSyncWorker(
//     ILogger<ElasticSyncWorker> logger,
//     IConfiguration configuration, ElasticsearchClient es)
//    {
//        _logger = logger;
//        _configuration = configuration;
//        _elasticClient = es;
      
//    }


//    private void InitializeRabbitMq()
//    {
        
//            var portStr = _configuration["RabbitMQ:Port"];
//            var port = string.IsNullOrEmpty(portStr) ? 15672 : int.Parse(portStr);
//            var factory = new ConnectionFactory
//            {
//                HostName = _configuration["RabbitMQ:Host"],
//                Port= 5673,
//                UserName = _configuration["RabbitMQ:Username"],
//                Password = _configuration["RabbitMQ:Password"],
//                DispatchConsumersAsync = true 
//            };
//            _logger.LogInformation("Trying to connect to RabbitMQ...");

//            _connection = factory.CreateConnection();

//            _logger.LogInformation("RabbitMQ connection SUCCESS");
        
       

      

//        _channel = _connection.CreateModel();

      
//        _channel.ExchangeDeclare(
//            exchange: "catalog.events",
//            type: ExchangeType.Fanout,
//            durable: true
//        );

     
//        _channel.QueueDeclare(
//            queue: "test.catalog.queue",
//            durable: true,
//            exclusive: false,
//            autoDelete: false
//        );

       
//        _channel.QueueBind(
//            queue: "test.catalog.queue",
//            exchange: "catalog.events",
//            routingKey: ""
//        );

//        // Fair dispatch
//        _channel.BasicQos(0, 1, false);
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        InitializeRabbitMq();

//        var consumer = new AsyncEventingBasicConsumer(_channel);

//        consumer.Received += async (sender, ea) =>
//        {
           
//                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

//                var @event = JsonSerializer.Deserialize<ProductCreatedEvent>(message);

//                if (@event != null)
//                {
//                    await _elasticClient.IndexAsync(@event, idx => idx
//                        .Index("products")
//                        .Id(@event.EventId)
//                    );

                  
//                }

//                _channel.BasicAck(ea.DeliveryTag, false);
            
            
//        };

//        _channel.BasicConsume(
//            queue: "test.catalog.queue",
//            autoAck: false,
//            consumer: consumer
//        );
//        await Task.Delay(Timeout.Infinite, stoppingToken);

//    }

//    public override void Dispose()
//    {
//        _channel?.Close();
//        _connection?.Close();
//        base.Dispose();
//    }
//}