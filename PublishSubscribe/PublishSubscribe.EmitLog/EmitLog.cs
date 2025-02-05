using RabbitMQ.Client;
using RabbitMQ.Protocol.Common;

using var channel = await Producer.CreateConnectedChannelFactory("localhost");


await channel.ExchangeDeclareAsync(exchange: "logs", type: ExchangeType.Fanout);



for (int i = 1; i <= 50; i++)
{
    string message = $"Task {i}";
    await Producer.ProducerPayloadAsync(channel, "logs", string.Empty, message);
    // Use BasicPublishAsync() Seconde Overload - Give one properties for {Persistent = true}
    // {Persistent = true} => RR - Messages are not lost if RabbitMQ is restarted.

    Console.WriteLine($" [x] Sent {message}");
    await Task.Delay(1000);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
