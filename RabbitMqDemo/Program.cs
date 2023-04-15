using System.Text.Json;
using Forca.Datamodel.Entiteter;
using NServiceBus.MessageMutator;
using RabbitMqDemo;

const string conn = "amqp://guest:guest@localhost:5672";
const string clientProviderName = "RabbitMQ -message sender";

const string exchangeName = "PensionsdataExchange";
const string routingKey = "Pensionsdata-routing-key";
const string queue = "PensionsdataQueue";

var endpointConfiguration = new NServiceBus.EndpointConfiguration("myNservicebusEndpoint");
endpointConfiguration.RegisterMessageMutator(new MutateOutgoingMessages());

var queueConnect = new QueueConnection(conn, clientProviderName, exchangeName, routingKey, queue);

// TESTDATA
// Returner enten data fra køen eller indlæser json fil - TODO: skal rettes så der altid hentes fra REDIS
var testdata = File.ReadAllText(@"~\..\..\..\..\..\RabbitMqDemo\Data\0101010202_pbu.json");
var message = JsonSerializer.Deserialize<Pensionsdata>(testdata) ?? new Pensionsdata();

queueConnect.PublishMessage(message);