using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Protocol.Common;

public class Consumer : CommonOperations
{
    public static async Task ConsumePayloadAsync(IChannel channel, string queue)
    {
        Console.WriteLine(" [*] Waiting for messages.");
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
            await Task.CompletedTask;
        };
        await channel.BasicConsumeAsync(queue, autoAck: true, consumer: consumer);
    }
}