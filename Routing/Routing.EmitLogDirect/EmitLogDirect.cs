//using RabbitMQ.Client;
//using RabbitMQ.Protocol.Common;

//using var channel = await Producer.CreateConnectedChannelFactory("localhost");


//await channel.ExchangeDeclareAsync(exchange: "direct_logs", type: ExchangeType.Direct);

//var severity_RoutingKey = "info";
////var severity_RoutingKey = (args.Length > 0) ? args[0] : "info";
//var message = "Hello World!";
////var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";

//await Producer.ProducerPayloadAsync(channel, "direct_logs", severity_RoutingKey, message);

//Console.WriteLine($" [x] Sent '{severity_RoutingKey}':'{message}'");

//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();

using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "direct_logs", type: ExchangeType.Direct);

var severity = (args.Length > 0) ? args[0] : "info";
var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";
var body = Encoding.UTF8.GetBytes(message);
await channel.BasicPublishAsync(exchange: "direct_logs", routingKey: severity, body: body);
Console.WriteLine($" [x] Sent '{severity}':'{message}'");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
