using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosSharp.ECS.Components;
using NosSharp.ECS.Contexts;
using NosSharp.ECS.Entities;
using NosSharp.ECS.Systems;

namespace NosSharp.ECS.Test
{
    [TestClass]
    public class SystemTest
    {
        private readonly List<IEntity> _emptyentities = new List<IEntity>();
        private readonly List<IEntity> _entities = new List<IEntity>();
        private readonly IEntityManager _entityManager = new EntityManager();
        private HpBarSystem _system;

        [TestMethod]
        public void AddComponentTest()
        {
            _entities.AddRange(Enumerable.Range(0, 10).Select(s =>
            {
                var tmp = new Entity(s);
                tmp.AddComponent(new HealthComponent(), typeof(HealthComponent));
                return tmp;
            }));
            _emptyentities.AddRange(Enumerable.Range(0, 150000).Select(s => new Entity(s)));
            _entityManager.RegisterEntity(_entities.ToArray());
            _system = new HpBarSystem();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"[-------- {i} --------]");
                _system.Execute();
            }
        }
    }
}