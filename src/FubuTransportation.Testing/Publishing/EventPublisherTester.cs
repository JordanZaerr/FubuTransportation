﻿using System;
using FubuMVC.Core.Ajax;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using FubuTransportation.Testing.Events;
using FubuTransportation.Web;
using NUnit.Framework;
using NUnit.Mocks;
using Rhino.Mocks;

namespace FubuTransportation.Testing.Publishing
{
    [TestFixture]
    public class publishing_a_message_successfully : InteractionContext<SendMessageBehavior<Message1>>
    {
        private Message1 theMessage;
        private InMemoryFubuRequest theRequest;

        protected override void beforeEach()
        {
            theMessage = new Message1();
            theRequest = new InMemoryFubuRequest();
            theRequest.Set(theMessage);
            Services.Inject<IFubuRequest>(theRequest);

            ClassUnderTest.InsideBehavior = MockFor<IActionBehavior>();

            ClassUnderTest.Invoke();
        }

        [Test]
        public void should_send_the_message_to_the_service_bus()
        {
            MockFor<IServiceBus>().AssertWasCalled(x => x.Send(theMessage));
        }

        [Test]
        public void should_call_through_to_the_inner_behavior()
        {
            MockFor<IActionBehavior>().AssertWasCalled(x => x.Invoke());
        }

        [Test]
        public void should_return_a_successful_ajax_continuation()
        {
            var continuation = theRequest.Get<AjaxContinuation>();
            continuation.Success.ShouldBeTrue();
        }
    }


}