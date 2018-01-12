using System;
using System.Linq;
using System.Reflection;

namespace NosSharp.ECS.Systems
{
    public abstract class EventableSystem<TClassType> where TClassType : class, IEventableSystem
    {
        public void SubscribeEvent<TArgs>(EventHandler<TArgs> callback)
        {
            EventInfo @event = typeof(TClassType).GetEvents(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(s => s.EventHandlerType == typeof(TArgs));
            if (@event == null)
            {
                // NO EVENT OF TYPE T in context
                return;
            }
            @event.AddMethod.Invoke(callback, null);
        }

        public void UnsubscribeEvent<TArgs>(EventHandler<TArgs> callback)
        {
            EventInfo @event = typeof(TClassType).GetEvents(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(s => s.EventHandlerType == typeof(TArgs));
            if (@event == null)
            {
                // NO EVENT OF TYPE T in context
                return;
            }
            @event.RemoveMethod.Invoke(callback, null);
        }

        public void RaiseEvent<TArgs>(object sender, TArgs args)
        {
            EventInfo @event = typeof(TClassType).GetEvents(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(s => s.EventHandlerType == typeof(TArgs));
            if (@event == null)
            {
                // NO EVENT OF TYPE T in context
                return;
            }
            @event.RaiseMethod.Invoke(sender, new object[] { new []{args} });
        }
    }
}