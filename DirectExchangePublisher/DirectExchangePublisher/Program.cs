
//Direct Exchange, mesajı direkt olarak ilgili adrese anahtar teslim yapmaktadır diyebiliriz

using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://nmlgtsid:Hl-ZajX0FHO3eG52BQnrPKc8aQpkYTsd@sparrow.rmq.cloudamqp.com/nmlgtsid");


using(IConnection connection=connectionFactory.CreateConnection())
using(IModel channel=connection.CreateModel())
{
    channel.ExchangeDeclare("directexchange", ExchangeType.Direct);
	for (int i = 0; i < 100; i++)
	{
		byte[] byteMessage = Encoding.UTF8.GetBytes($"sayı - {i}");

		IBasicProperties basicProperties = channel.CreateBasicProperties();
		basicProperties.Persistent = true;

		if (i % 2 == 0)
			channel.BasicPublish("directexchange", "ciftSayi", basicProperties, byteMessage);
		else
            channel.BasicPublish("directexchange", "teksayi", basicProperties, byteMessage);

    }
}