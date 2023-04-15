using System.Text.Json;
using Forca.Datamodel.Entiteter;
using RabbitMqDemo;

#region OldCode
//var factory = new ConnectionFactory
//{
//    Uri = new Uri("amqp://guest:guest@localhost:5672"),
//    ClientProvidedName = "RabbitMQ -message sender"
//};

//var connection = factory.CreateConnection();
//var channel = connection.CreateModel();

//var exchangeName = "PensionsdataExchange";
//var routingKey = "Pensionsdata-routing-key";
//var queueName = "PensionsdataQueue";

//channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
//channel.QueueDeclare(queueName, false, false, false, null);
//channel.QueueBind(queueName, exchangeName, routingKey, null);

//var jsonData = File.ReadAllText(@"~\..\Data\0101010202_pbu.json");

//var messageBody = Encoding.UTF8.GetBytes(jsonData);
//channel.BasicPublish(exchangeName, routingKey, null, messageBody);

//channel.Close();
//connection.Close(); 
#endregion

const string conn = "amqp://guest:guest@localhost:5672";
const string clientProviderName = "RabbitMQ -message sender";

const string exchangeName = "PensionsdataExchange";
const string routingKey = "Pensionsdata-routing-key";
const string queue = "PensionsdataQueue";

var queueConnect = new QueueConnection(conn, clientProviderName, exchangeName, routingKey, queue);

// TESTDATA
// Returner enten data fra køen eller indlæser json fil - TODO: skal rettes så der altid hentes fra REDIS
var testdata = File.ReadAllText(@"~\..\..\..\..\..\RabbitMqDemo\Data\0101010202_pbu.json");
var message = JsonSerializer.Deserialize<Pensionsdata>(testdata) ?? new Pensionsdata();

queueConnect.PublishMessage(message);