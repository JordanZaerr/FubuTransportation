﻿using System;
using System.Threading.Tasks;

namespace FubuTransportation
{
    public interface IServiceBus
    {
        /// <summary>
        /// Loosely-coupled Request/Reply pattern
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="timeout">Timespan to wait for a response before timing out</param>
        /// <returns></returns>
        Task<TResponse> Request<TResponse>(object request, TimeSpan? timeout = null);

        void Send<T>(T message);
        /// <summary>
        /// Send to a specific destination rather than running the routing rules
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination">The destination to send to</param>
        /// <param name="message"></param>
        void Send<T>(Uri destination, T message);

        /// <summary>
        /// Invoke consumers for the relevant messages managed by the current
        /// service bus instance. This happens immediately and on the current thread.
        /// Error actions will not be executed and the message consumers will not be retried
        /// if an error happens.
        /// </summary>
        void Consume<T>(T message);

        void DelaySend<T>(T message, DateTime time);
        void DelaySend<T>(T message, TimeSpan delay);

        /// <summary>
        /// Send a message and await an acknowledgement that the 
        /// message has been processed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAndWait<T>(T message);
    }
}