using RabbitMQ.Client;
using RabbitMQ.Protocol.Common;


using var channel = await Producer.CreateConnectedChannelFactory("localhost");

await channel.ExchangeDeclareAsync(exchange: "topic_logs", type: ExchangeType.Topic);


var routingKey = (args.Length > 0) ? args[0] : "anonymous.info";

var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";

await Producer.ProducerPayloadAsync(channel, exchangeName: "topic_logs", routingKey, message);

Console.WriteLine($" [x] Sent '{routingKey}':'{message}'");