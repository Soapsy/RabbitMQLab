using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using LAB.DataScanner.Components.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using LAB.DataScanner.Components.Services.MessageBroker;
using System.Linq;

namespace LAB.DataScanner.Components.Tests.Unit.Services.MessageBroker
{
    [TestClass]
    public class RmqConsumerTests
    {
        [TestMethod]
        public void ShouldCall_AckMessage_OnceTheyArrivedAndHandled()
        {
            //arrange
            var channel = Substitute.For<IModel>();
            var eventConsumer = Substitute.For<EventingBasicConsumer>();
            eventConsumer += Raise.Event<Received>();
            //act

            //assert
        }

        [TestMethod]
        public void ShouldCall_BasicConsume_OnceStartListening()
        {
            //arrange
            var channelMock = Substitute.For<IModel>();
            var defaultQueueValue = "queue";


            RmqConsumer testConsumer = new RmqConsumer(channelMock, defaultQueueValue);


            var eventConsumerMock = Substitute.For<IBasicConsumer>();
            var eventHandlerMock = Substitute.For<EventHandler<BasicDeliverEventArgs>>();

            //act
            testConsumer.StartListening(eventHandlerMock);

            //assert
            channelMock.Received().BasicConsume(defaultQueueValue, autoAck: false, testConsumer._consumer);
        }

        [TestMethod]
        public void ShouldCall_BasicCancel_OnceStopListening()
        {
            //arrange
            var channelMock = Substitute.For<IModel>();
            var defaultQueueValue = "queue";

            RmqConsumer testConsumer = new RmqConsumer(channelMock, defaultQueueValue);

            var amountOfTags = testConsumer._consumer.ConsumerTags.Count();

            //act
            testConsumer.StopListening();

            //assert
            channelMock.Received(amountOfTags).BasicCancel(Arg.Any<String>());

        }
    }
}
