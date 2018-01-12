using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NosSharp.ECS.Contexts;
using NosSharp.ECS.Entities;
using NosSharp.ECS.Systems;

namespace NosSharp.ECS.Test
{
    public class HpBarArgs
    {
        public ulong Hp { get; set; }
        public ulong Mp { get; set; }

        public override string ToString()
        {
            return $"{Hp}:{Mp}";
        }
    }

    public class HpBarSystem : IExecuteSystem
    {
        private static event EventHandler<HpBarArgs> HpBarEvent;
        private readonly List<IEntityManager> _entityManagers = new List<IEntityManager>();

        /// <summary>
        /// This will subscribe a callback for the event when args are of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public static void SubscribeEvent<T>(EventHandler<T> callback)
        {
            EventInfo @event = typeof(HpBarSystem).GetEvents(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(s => s.EventHandlerType == typeof(T));
            if (@event == null)
            {
                // NO EVENT OF TYPE T in context
                return;
            }
            @event.AddMethod.Invoke(callback, null);
        }

        /// <summary>
        /// This will raise event where args are of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void RaiseEvent<T>(object sender, object[] args)
        {

            EventInfo @event = typeof(HpBarSystem).GetEvents(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(s => s.EventHandlerType == typeof(T));
            if (@event == null)
            {
                // NO EVENT OF TYPE T in context
                return;
            }

            @event.RaiseMethod.Invoke(sender, args);
        }

        public static void UnsubscribeEvent<T>(EventHandler<T> callback)
        {
            EventInfo @event = typeof(HpBarSystem).GetEvents(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(s => s.EventHandlerType == typeof(T));
            if (@event == null)
            {
                return;
            }

            @event.RemoveMethod.Invoke(callback, null);
        }


        public void Subscribe(IEntityManager manager)
        {
            SubscribeEvent<HpBarArgs>((sender, args) =>
            {
                Console.WriteLine($"On Event it will be raised");
            });
            _entityManagers.Add(manager);
        }

        public void Unsubscribe(IEntityManager manager)
        {
            _entityManagers.Add(manager);
        }

        public void Execute()
        {
            foreach (IEntityManager context in _entityManagers)
            {
                IEntity[] entities = context.GetEntities<HealthComponent>();
                if (entities == null || entities.Length < 1)
                {
                    continue;
                }

                foreach (IEntity entity in entities)
                {
                    HealthComponent hp = entity.GetComponent<HealthComponent>();
                    hp.Hp += 1;
                    hp.Mp += 1;
                    Console.WriteLine($"[{DateTime.Now}][ENTITY] : {entity.Id} | hp : {hp.Hp}/{hp.HpMax} | mp : {hp.Mp}/{hp.MpMax}");
                }
            }

            Console.WriteLine($"[LOG][SYSTEM] Successfully updated {typeof(HpBarSystem)}");
        }
    }
}