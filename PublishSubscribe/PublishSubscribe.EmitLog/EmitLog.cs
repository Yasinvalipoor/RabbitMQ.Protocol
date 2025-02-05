using RabbitMQ.Protocol.Common;

using var channel = await Producer.CreateConnectedChannelFactory("localhost");
