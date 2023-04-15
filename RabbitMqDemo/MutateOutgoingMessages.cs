namespace RabbitMqDemo
{
    using NServiceBus.MessageMutator;

    public class MutateOutgoingMessages : IMutateOutgoingTransportMessages
    {
        public Task MutateOutgoing(MutateOutgoingTransportMessageContext context)
        {
            // the outgoing headers
            var outgoingHeaders = context.OutgoingHeaders;

            if (context.TryGetIncomingMessage(out var incomingMessage))
            {
                // do something with the incoming message
            }

            if (context.TryGetIncomingHeaders(out var incomingHeaders))
            {
                // do something with the incoming headers
            }

            // the outgoing message
            // optionally replace the message instance by setting context.OutgoingMessage
            var outgoingMessage = context.OutgoingMessage;

            return Task.CompletedTask;
        }
    }
}
