

namespace MultiShop.Order.Persistence.Messaging
{
    //public class OrderRabbitConsumer : BackgroundService
    //{
    //    private readonly IServiceProvider _serviceProvider;
    //    private IConnection _connection;
    //    private IModel _channel;

    //    public OrderRabbitConsumer(IServiceProvider serviceProvider)
    //    {
    //        _serviceProvider = serviceProvider;
    //    }

    //    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        var factory = new ConnectionFactory
    //        {
    //            HostName = "localhost",
    //            Port = 5673,
    //            UserName = "guest",
    //            Password = "guest"
    //        };

    //        _connection = factory.CreateConnection();
    //        _channel = _connection.CreateModel();

    //        _channel.ExchangeDeclare(
    //            exchange: "order.saga.events",
    //            type: ExchangeType.Fanout,
    //            durable: true
    //        );

    //        _channel.QueueDeclare(
    //            queue: "order.saga.queue",
    //            durable: true,
    //            exclusive: false,
    //            autoDelete: false
    //        );

    //        _channel.QueueBind(
    //            queue: "order.saga.queue",
    //            exchange: "order.saga.events",
    //            routingKey: ""
    //        );

    //        var consumer = new AsyncEventingBasicConsumer(_channel);

    //        consumer.Received += OnEventReceived;

    //        _channel.BasicConsume(
    //            queue: "order.saga.queue",
    //            autoAck: false,
    //            consumer: consumer
    //        );

    //        return Task.CompletedTask;
    //    }

    
    //private async Task OnEventReceived(object sender, BasicDeliverEventArgs ea)
    //    {
    //        var json = Encoding.UTF8.GetString(ea.Body.ToArray());

    //        var baseEvent = JsonDocument.Parse(json);
    //        var eventType = baseEvent.RootElement.GetProperty("EventType").GetString();

    //        using var scope = _serviceProvider.CreateScope();

    //        //switch (eventType)
    //        //{
    //          //  case "StockReservedEvent":
    //                var reservedEvent = JsonSerializer.Deserialize<OrderRequestEvent>(json);
    //                var reservedConsumer = scope.ServiceProvider
    //                    .GetRequiredService<OrderRequestedEventConsumer>();
    //                await reservedConsumer.HandleAsync(reservedEvent!);
    //              //  break;

    //            //case "StockReservationFailedEvent":
    //            //    var failedEvent = JsonSerializer.Deserialize<StockReservationFailedEvent>(json);
    //            //    var failedConsumer = scope.ServiceProvider
    //            //        .GetRequiredService<StockReservationFailedEventConsumer>();
    //            //    await failedConsumer.HandleAsync(failedEvent!);
    //            //    break;
    //       // }

    //        _channel.BasicAck(ea.DeliveryTag, false);
    //    }

    //}
}