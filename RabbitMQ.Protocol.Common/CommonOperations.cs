using RabbitMQ.Client;

namespace RabbitMQ.Protocol.Common;

public abstract class CommonOperations
{
    public static async Task<IChannel> CreateConnectedChannelFactory(string hostName)
    {
        var factory = new ConnectionFactory { HostName = hostName };
        var connection = await factory.CreateConnectionAsync();
        return await connection.CreateChannelAsync();
    }

    public static async Task CreateQueueFromChannel(IChannel channel, string queueName)
    {
        await channel.QueueDeclareAsync(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
}