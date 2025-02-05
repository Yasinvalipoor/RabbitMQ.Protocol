using RabbitMQ.Client;
using RabbitMQ.Protocol.Common;

using var channel = await Producer.CreateConnectedChannelFactory("localhost");

string queueName = "WorkQueues";


await Producer.CreateQueueFromChannel(channel, queueName);

var properties = new BasicProperties
{
    Persistent = true
};

for (int i = 1; i <= 50; i++)
{
    string message = $"Task {i}";
    await Producer.ProducerPayloadAsync(channel, string.Empty, queueName, message, properties);
    // Use BasicPublishAsync() Seconde Overload - Give one properties for {Persistent = true}
    // {Persistent = true} => RR - Messages are not lost if RabbitMQ is restarted.

    Console.WriteLine($" [x] Sent {message}");
    await Task.Delay(1000);
}