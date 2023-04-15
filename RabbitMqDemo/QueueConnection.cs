namespace RabbitMqDemo
{
    using RabbitMQ.Client;
    using System.Text;
    using System.Text.Json;

    public class QueueConnection
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly string _exchangeName;
        private readonly string _routingKey;
        private readonly string _queueName;

        public QueueConnection(string endpoint, string clientProviderName, string exchangeName, string routingKey, string queueName)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(endpoint),
                ClientProvidedName = clientProviderName
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _exchangeName = exchangeName;
            _routingKey = routingKey;
            _queueName = queueName;
        }

        public void PublishMessage<T>(T message) where T : class
        {
            OpenChannel();
            
            var data = JsonSerializer.Serialize(message);
            var messageBody = Encoding.UTF8.GetBytes(data);
            _channel.BasicPublish(_exchangeName, _routingKey, null, messageBody);

            CloseChannel();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenChannel()
        {
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_queueName, false, false, false, null);
            _channel.QueueBind(_queueName, _exchangeName, _routingKey, null);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseChannel()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
