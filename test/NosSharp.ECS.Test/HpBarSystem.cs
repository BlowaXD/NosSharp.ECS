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

    public class HpBarSystem : EventableSystem<HpBarSystem>, IExecuteSystem, IEventableSystem
    {
        public static event EventHandler<HpBarArgs> HpBarEvent;
        private readonly List<IEntityManager> _entityManagers = new List<IEntityManager>();
        
        public void Subscribe(IEntityManager manager)
        {
            SubscribeEvent<HpBarArgs>((sender, args) =>
            {
                Console.WriteLine($"HpBarEvent Has been raised");
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