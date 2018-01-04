﻿using System;
using System.Collections.Generic;
using System.Linq;
using NosSharp.ECS.Components;

namespace NosSharp.ECS.Entity
{
    public class Entity : IEntity
    {
        private readonly Dictionary<Type, IComponent> _components;

        public Entity(long id)
        {
            Id = id;
            _components = new Dictionary<Type, IComponent>();
        }

        public long Id { get; }

        public bool HasComponent<T>()
        {
            return HasComponent(typeof(T));
        }

        public bool HasComponent(Type type)
        {
            return _components.ContainsKey(type);
        }

        public IComponent GetComponent<T>()
        {
            return GetComponent(typeof(T));
        }

        public IComponent GetComponent(Type type)
        {
            return !_components.TryGetValue(type, out IComponent value) ? null : value;
        }

        public IComponent[] GetComponents()
        {
            return _components.Values.ToArray();
        }

        public IComponent[] GetComponents<T>()
        {
            return GetComponents(typeof(T));
        }

        public IComponent[] GetComponents(Type type)
        {
            return _components.Values.ToArray();
        }

        public void AddComponent(Type type, IComponent component)
        {
            _components.TryAdd(type, component);
        }

        public void AddComponent<T>(IComponent component)
        {
            AddComponent(typeof(T), component);
        }

        public bool RemoveComponent<T>()
        {
            return RemoveComponent(typeof(T));
        }

        public bool RemoveComponent(Type type)
        {
            return _components.Remove(type);
        }

        public void Destroy()
        {
            // DONT KNOW WHAT HAS TO BE DONE YET
        }
    }
}