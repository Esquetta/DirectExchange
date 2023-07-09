using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://nmlgtsid:Hl-ZajX0FHO3eG52BQnrPKc8aQpkYTsd@sparrow.rmq.cloudamqp.com/nmlgtsid");


using (IConnection connection = connectionFactory.CreateConnection())
using (IModel channel = connection.CreateModel())
{
    channel.ExchangeDeclare("directexchange", ExchangeType.Direct);

    string queueName = channel.QueueDeclare().QueueName;

    if (int.Parse(args[0])==1)
        channel.QueueBind(queueName, "directexchange", "teksayi");
    else
        channel.QueueBind(queueName, "directexchange", "ciftSayi");

    channel.BasicQos(0, 1, false);

    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    channel.BasicConsume(queueName, false, consumer);

    consumer.Received += (sender, e) =>
    {
        
        Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray()));
        channel.BasicAck(e.DeliveryTag, false);

    };
    Console.Read();
}
