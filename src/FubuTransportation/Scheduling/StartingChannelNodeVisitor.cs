﻿using FubuTransportation.Configuration;
using FubuTransportation.Runtime;

namespace FubuTransportation.Scheduling
{
    public class StartingChannelNodeVisitor : IChannelNodeVisitor
    {
        private readonly IReceiver _receiver;

        public StartingChannelNodeVisitor(IReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Visit(ChannelNode node)
        {
            node.Scheduler.Start(() => StartReceive(ReceivingState.CanContinueReceiving, node.Channel));
        }

        public void StartReceive(ReceivingState receivingState, IChannel channel)
        {
            while (receivingState == ReceivingState.CanContinueReceiving)
            {
                receivingState = channel.Receive(_receiver);
            }
        }
    }
}