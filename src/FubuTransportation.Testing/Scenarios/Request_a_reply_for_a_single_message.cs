﻿using FubuTransportation.Testing.ScenarioSupport;

namespace FubuTransportation.Testing.Scenarios
{
    public class Request_a_reply_for_a_single_message : Scenario
    {
        public Request_a_reply_for_a_single_message()
        {
            Website1.Registry.Channel(x => x.Service1)
                    .PublishesMessage<OneMessage>();

            Service1.Handles<OneMessage>()
                .Raises<TwoMessage>();

            Website1.Requests<OneMessage>("original request")
                .ExpectReply<TwoMessage>().From(Service1);
        }


    }
}