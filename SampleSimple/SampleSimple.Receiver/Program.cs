using RabbitMQ.Protocol.Common;

using var channel = await Consumer.CreateConnectedChannelFactory("localhost");

string queueName = "Telegram";


await Consumer.CreateQueueFromChannel(channel, queueName);

await Consumer.ConsumePayloadAsync(channel, queueName);


Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();