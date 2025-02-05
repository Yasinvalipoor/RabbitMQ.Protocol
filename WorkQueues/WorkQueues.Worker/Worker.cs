using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Protocol.Common;

using var channel = await Consumer.CreateConnectedChannelFactory("localhost");

string queueName = "WorkQueues";


await Consumer.CreateQueueFromChannel(channel, queueName);


await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");

    int dots = message.Split('.').Length - 1;
    await Task.Delay(dots * 1000);

    Console.WriteLine(" [x] Done");

    // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
    await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
};

// توزیع منصفانه پیام‌ها بین مصرف‌کنندگان
//await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);


await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();