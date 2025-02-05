using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Protocol.Common;

public class Producer : CommonOperations
{
    public static async Task ProducerPayloadAsync(IChannel channel, string routingKey, string messageBody)
    {
        var payload = ConvertObjectOrMessageToByte(messageBody);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: routingKey, body: payload);
    }

    public static async Task ProducerPayloadAsync(IChannel channel, string routingKey, string messageBody, BasicProperties properties)
    {
        var payload = ConvertObjectOrMessageToByte(messageBody);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: routingKey, mandatory: true, basicProperties: properties, body: payload);
    }

    private static byte[] ConvertObjectOrMessageToByte<T>(T obj) => Encoding.UTF8.GetBytes(obj.ToString());
}