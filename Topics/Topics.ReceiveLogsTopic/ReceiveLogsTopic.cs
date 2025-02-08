using RabbitMQ.Client;
using RabbitMQ.Protocol.Common;

if (args.Length < 1)
{
    Console.Error.WriteLine("Usage: {0} [binding_key...]",
                            Environment.GetCommandLineArgs()[0]);
    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
    Environment.ExitCode = 1;
    return;
}

using var channel = await Consumer.CreateConnectedChannelFactory("localhost");

await channel.ExchangeDeclareAsync(exchange: "topic_logs", type: ExchangeType.Topic);

// declare a server-named queue
QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
string queueName = queueDeclareResult.QueueName;

foreach (string? bindingKey in args)
{
    await channel.QueueBindAsync(queue: queueName, exchange: "topic_logs", routingKey: bindingKey);
}

Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");


await Consumer.ConsumePayloadAsync(channel, queueName);


Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
