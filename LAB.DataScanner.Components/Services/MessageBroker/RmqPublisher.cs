using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using LAB.DataScanner.Components.Interfaces;

namespace LAB.DataScanner.Components.Services.MessageBroker
{
    class RmqPublisher : IRmqPublisher
    {
        private IModel _channel { get; }
        private string _route { get; }
        private string _exchange { get; }

        public RmqPublisher(IModel amqpChannel, string exchange, string routingKey)
        {
            _channel = amqpChannel;
            _route = routingKey;
            _exchange = exchange;
        }

        public void Publish(byte[] message)
        {
                _channel.BasicPublish(exchange: _exchange,
                               routingKey: _route,
                               basicProperties: null,
                               body: message);
        }

        public void Publish(byte[] message, string routingKey)
        {
            _channel.BasicPublish(exchange: _exchange,
                           routingKey: routingKey,
                           basicProperties: null,
                           body: message);
        }
        public void Publish(byte[] message, string exchange, string routingKey)
        {
            _channel.BasicPublish(exchange: exchange,
                           routingKey: routingKey,
                           basicProperties: null,
                           body: message);
        }
        public void Publish(byte[] outputData, string exchangeName, string[] routingKeys)
        {
            //Should we try to bind exchange instead?
            /*_channel.QueueBind(exchange = 'logs',
                   queue = result.method.queue)*/

            foreach (string r in routingKeys)
            {
                _channel.BasicPublish(exchange: exchangeName,
                                     routingKey: r,
                                     basicProperties: null,
                                     body: outputData);
            }

        }

        public void Dispose()
        {
            //closing and disposing
            _channel.Close();
            _channel.Dispose();
        }
    }

}
