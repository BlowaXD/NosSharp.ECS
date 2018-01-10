using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosSharp.ECS.Components;
using NosSharp.ECS.Contexts;
using NosSharp.ECS.Entities;

namespace NosSharp.ECS.Test
{
    [TestClass]
    public class ContextTest
    {
        private readonly IEntity _entity = new Entity(1);
        private readonly IComponent _component = new HealthComponent();

        [TestMethod]
        public void AddComponentTest()
        {
            _entity.AddComponent<HealthComponent>(_component);
            Assert.IsTrue(_entity.HasComponent(_component.Type));
            Assert.IsTrue(_entity.HasComponent<HealthComponent>());
        }

        [TestMethod]
        public void TestEventOnContextChangement()
        {
            _entity.AddComponent<HealthComponent>(_component);
            Assert.IsNotNull(_entity.GetComponent<HealthComponent>());
            Assert.IsNotNull(_entity.GetComponent(_component.Type));

            // EXTRAIT DE CODE
            Map map = new Map();
            Map.EntityKilled += (sender, args) => { Console.WriteLine($"[LOG] {args.KillerId} killed {args.EntityId}"); };
            Map.EntityKilled += (sender, args) => { Console.WriteLine($"[KILL] {args.KillerId} Rewarded of 1000 golds"); };

            map.SessionRegistered += (sender, args) => { Console.WriteLine($"[LOG] {args.EntityId} connected"); };
            map.SessionRegistered += (sender, args) => { Console.WriteLine($"[Broadcast] Bonjour tout le monde"); };

            EntityKilledEventArgs entityKilledArgs = new EntityKilledEventArgs
            {
                EntityType = 1,
                EntityId = 1,
                KillerId = 20
            };
            map.RegisterEntity(_entity);
        }

        public class Map : EntityManager
        {
            public static event EventHandler<EntityKilledEventArgs> EntityKilled;
            public event EventHandler<SessionRegisteredEventArgs> SessionRegistered;

            public new void RegisterEntity(IEntity entity)
            {
                base.RegisterEntity(entity);
                HealthComponent hp = entity.GetComponent<HealthComponent>();
                if (hp != null)
                {
                    Console.WriteLine($"[Entity] : {entity.Id}");
                    Console.WriteLine($"HP : {hp.Hp} / {hp.HpMax}");
                    Console.WriteLine($"MP : {hp.Mp} / {hp.MpMax}");
                }

                OnSessionRegistered(new SessionRegisteredEventArgs {EntityId = entity.Id});
            }

            protected virtual void OnSessionRegistered(SessionRegisteredEventArgs e)
            {
                SessionRegistered?.Invoke(this, e);
            }

            private static void OnEntityKilled(EntityKilledEventArgs e)
            {
                EntityKilled?.Invoke(null, e);
            }
        }

        public class SessionRegisteredEventArgs : EventArgs
        {
            public long EntityId { get; set; }
            public object Session { get; set; } // "Session Class" but lazy
            public long MapId { get; set; }
        }

        public class EntityKilledEventArgs : EventArgs
        {
            public int EntityType { get; set; }
            public long EntityId { get; set; }
            public int KillerType { get; set; }
            public long KillerId { get; set; }
        }
    }
}