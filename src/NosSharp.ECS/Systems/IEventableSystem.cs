using System;

namespace NosSharp.ECS.Systems
{
    public interface IEventableSystem : ISystem
    {
        /// <summary>
        /// Callback subscribes to Event with args of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        void SubscribeEvent<T>(EventHandler<T> callback);

        /// <summary>
        /// Callback is unregistered from event invocation list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        void UnsubscribeEvent<T>(EventHandler<T> callback);

        /// <summary>
        /// Raise an event of type T and call its invocation list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void RaiseEvent<T>(object sender, T args);
    }
}