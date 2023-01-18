using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using LAB.DataScanner.Components.Services.MessageBroker;
using System.Text;
using RabbitMQ.Client;


namespace LAB.DataScanner.Components.Tests
{
    [TestClass]
    public class RmqPublisherTests
    {
        [TestMethod]
        public void ShouldPublishMessageToDefaultExchange()
        {
            //arrange
            var channel = Substitute.For<IModel>();

            string defaultRoute = "default route";
            string defaultExchange = "default exchange";

            var message = Encoding.UTF8.GetBytes("test message");

            RmqPublisher publisher = new RmqPublisher(channel, defaultExchange, defaultRoute);

            //act
            publisher.Publish(message);

            //assert
            channel.Received().BasicPublish(exchange: defaultExchange,
                               routingKey: defaultRoute,
                               basicProperties: null,
                               body: message);
        }

        [TestMethod]
        public void ShouldPublishMessageWithRoutingKey()
        {
            //arrange
            var channel = Substitute.For<IModel>();

            string defaultRoute = "default route";
            string defaultExchange = "default exchange";

            var message = Encoding.UTF8.GetBytes("test message");
            string route = "test route";

            RmqPublisher publisher = new RmqPublisher(channel, defaultExchange, defaultRoute);

            //act
            //can we use "default" keyword?
            publisher.Publish(message, route);

            //assert
            channel.Received().BasicPublish(exchange: defaultExchange,
                               routingKey: route,
                               basicProperties: null,
                               body: message);
        }

        [TestMethod]
        public void ShouldPublishMessageToCertainExchangeAndRoutingKey()
        {
            //arrange
            var channel = Substitute.For<IModel>();

            string defaultRoute = "default route";
            string defaultExchange = "default exchange";

            var message = Encoding.UTF8.GetBytes("test message");
            string route = "test route";
            string exchange = "test exchange";

            RmqPublisher publisher = new RmqPublisher(channel, defaultExchange, defaultRoute);

            //act
            publisher.Publish(message, exchange, route );

            //assert
            channel.Received().BasicPublish(exchange: exchange,
                               routingKey: route,
                               basicProperties: null,
                               body: message);
        }
    }
}
