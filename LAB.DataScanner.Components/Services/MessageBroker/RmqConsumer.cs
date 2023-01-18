using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

using LAB.DataScanner.Components.Interfaces;
namespace LAB.DataScanner.Components.Services.MessageBroker
{
    class RmqConsumer : IRmqConsumer
    {
        private IModel _channel { get; set; }
        private EventingBasicConsumer _consumer { get; set; }
        private string _queueName { get; }

        public RmqConsumer(IModel amqpChannel, string queueName)
        {
            _channel = amqpChannel;
            _queueName = queueName;
        }
        public void Ack(BasicDeliverEventArgs args)
        {
            _channel.BasicAck(args.DeliveryTag, true);
        }

        public void StartListening(EventHandler<BasicDeliverEventArgs> onReceiveHandler)
        {
            if (_consumer is null)
            {
                _consumer = new EventingBasicConsumer(_channel);
                //passing received event into the consumer
                _consumer.Received += onReceiveHandler;
            }

            _channel.BasicConsume(_queueName, autoAck: false, _consumer);
            
        }
        public void StopListening()
        {
            foreach(string t in _consumer.ConsumerTags)
            {
                _channel.BasicCancel(t);
            }

        }

        public void SetQueue(string queueName)
        {
            //Deleting old queue before setting a new one
            _channel.QueueDelete(_queueName);
            _channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

        }
    }
}
