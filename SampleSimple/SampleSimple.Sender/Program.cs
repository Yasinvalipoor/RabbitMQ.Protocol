﻿using RabbitMQ.Protocol.Common;


using var channel = await Producer.CreateConnectedChannelFactory("localhost");

string queueName = "Telegram";


await Producer.CreateQueueFromChannel(channel, queueName);

const string message = "Hello From IRAN";

await Producer.ProducerPayloadAsync(channel, queueName, message);


Console.WriteLine($" [x] Sent {message}");
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();