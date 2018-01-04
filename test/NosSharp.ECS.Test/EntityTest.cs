using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosSharp.ECS.Components;
using NosSharp.ECS.Contexts;
using NosSharp.ECS.Entities;

namespace NosSharp.ECS.Test
{
    [TestClass]
    public class EntityTest
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
        public void GetComponentTest()
        {
            _entity.AddComponent<HealthComponent>(_component);
            Assert.IsNotNull(_entity.GetComponent<HealthComponent>());
            Assert.IsNotNull(_entity.GetComponent(_component.Type));
        }
    }
}