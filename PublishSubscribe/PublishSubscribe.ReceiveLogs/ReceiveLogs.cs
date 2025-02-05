
using RabbitMQ.Client;
using RabbitMQ.Protocol.Common;

using var channel = await Consumer.CreateConnectedChannelFactory("localhost");

await channel.ExchangeDeclareAsync(exchange: "logs", type: ExchangeType.Fanout);


// declare a server-named queue
QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
string queueName = queueDeclareResult.QueueName;
await channel.QueueBindAsync(queue: queueName, exchange: "logs", routingKey: string.Empty);

Console.WriteLine($" [*] Waiting for logs. Queue: {queueName}");

await Consumer.ConsumePayloadAsync(channel, queueName);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();