using System;
using System.Collections.Generic;
using System.Linq;
using NosSharp.ECS.Components;

namespace NosSharp.ECS.Entities
{
    public class Entity : IEntity
    {
        protected readonly Dictionary<Type, IComponent> Components;

        public Entity(long id)
        {
            Id = id;
            Components = new Dictionary<Type, IComponent>();
        }

        public long Id { get; }

        public Type EntityType => typeof(Entity);

        public bool HasComponent<T>()
        {
            return HasComponent(typeof(T));
        }

        public bool HasComponent(Type type)
        {
            return Components.ContainsKey(type);
        }

        /// <inheritdoc />
        public T GetComponent<T>() where T : IComponent
        {
            return (T) GetComponent(typeof(T));
        }

        public IComponent GetComponent(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return !Components.TryGetValue(type, out IComponent value) ? null : value;
        }

        public IComponent[] GetComponents()
        {
            return Components.Values.ToArray();
        }

        public IComponent[] GetComponents<T>() where T : IComponent
        {
            return GetComponents(typeof(T));
        }

        public IComponent[] GetComponents(Type type)
        {
            return Components.Values.ToArray();
        }

        public void AddComponent(IComponent component, Type type)
        {
            Components.TryAdd(type, component);
        }

        public void AddComponent<T>(IComponent component) where T : class
        {
            AddComponent(component, typeof(T));
        }

        public bool RemoveComponent<T>()
        {
            return RemoveComponent(typeof(T));
        }

        public bool RemoveComponent(Type type)
        {
            return Components.Remove(type);
        }

        public void Destroy()
        {
        }
    }
}