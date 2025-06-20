using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MultiShop.RabbitMQMessageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> CreateMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName ="localhost"
            };
            var connection = await connectionFactory.CreateConnectionAsync();



            var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync("Kuyruk1", false, false, false, arguments: null);
            var messageContent = "Merhaba bu bir RabbitMQ kuyruk mesajýdýr.";
            var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);
            await channel.BasicPublishAsync(exchange: "", routingKey: "Kuyruk1", body: byteMessageContent);

            return Ok("Mesajýnýz kuyruða alýnmýþtýr.");
        }
        private static string message;
        [HttpGet]
        public async Task<IActionResult> ReadMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = await connectionFactory.CreateConnectionAsync();



            var channel = await connection.CreateChannelAsync();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, response) =>
            {
                var byteMessage = response.Body.ToArray();
                message = Encoding.UTF8.GetString(byteMessage);
            };
            await channel.BasicConsumeAsync(queue: "Kuyruk1", autoAck: false, consumer: consumer);
            if(string.IsNullOrEmpty(message))
            {
                return NoContent();
            }
            return Ok(message);
        }
    }
}
