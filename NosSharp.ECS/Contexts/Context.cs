﻿using System;
using System.Collections.Generic;
using System.Linq;
using NosSharp.ECS.Components;
using NosSharp.ECS.Entities;

namespace NosSharp.ECS.Contexts
{
    public class Context : IContext
    {
        protected readonly Dictionary<long, IEntity> Entities;
        protected readonly Dictionary<Type, List<IEntity>> EntitiesByComponents;
        protected readonly Dictionary<Type, List<IEntity>> EntitiesByType;
        protected readonly List<IComponent> Components;

        public Context()
        {
            Entities = new Dictionary<long, IEntity>();
            EntitiesByComponents = new Dictionary<Type, List<IEntity>>();
            EntitiesByType = new Dictionary<Type, List<IEntity>>();
            Components = new List<IComponent>();
        }

        public IEntity GetEntity(long id)
        {
            return !Entities.TryGetValue(id, out IEntity entity) ? null : entity;
        }

        public IEntity[] GetEntities()
        {
            return Entities.Values.ToArray();
        }

        public IEntity[] GetEntities<T>()
        {
            return GetEntities(typeof(T));
        }

        public IEntity[] GetEntities(Type type)
        {
            return !EntitiesByType.TryGetValue(type, out List<IEntity> entities) ? null : entities.ToArray();
        }

        public IEntity[] GetEntitiesByComponent<T>()
        {
            return GetEntitiesByComponent(typeof(T));
        }

        public IEntity[] GetEntitiesByComponent(Type type)
        {
            return !EntitiesByComponents.TryGetValue(type, out List<IEntity> entities) ? null : entities.ToArray();
        }

        public void RegisterEntity(IEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            IComponent[] components = entity.GetComponents();
            Type entityType = entity.EntityType;

            List<IEntity> entities;
            foreach (IComponent component in components.Concat(Components))
            {
                if (!EntitiesByComponents.TryGetValue(component.Type, out entities))
                {
                    entities = new List<IEntity>();
                }

                entities.Add(entity);
                EntitiesByComponents[component.Type] = entities;
            }

            if (!EntitiesByType.TryGetValue(entityType, out entities))
            {
                entities = new List<IEntity>();
            }

            entities.Add(entity);
            EntitiesByComponents[entityType] = entities;
        }

        public void UnregisterEntity(IEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            IComponent[] components = entity.GetComponents();
            Type entityType = entity.EntityType;

            List<IEntity> entities;
            foreach (IComponent component in components.Concat(Components))
            {
                if (!EntitiesByComponents.TryGetValue(component.Type, out entities))
                {
                    throw new ArgumentException();
                }

                entities.Remove(entity);
                EntitiesByComponents[component.Type] = entities;
            }

            if (!EntitiesByType.TryGetValue(entityType, out entities))
            {
                throw new ArgumentException();
            }

            entities.Remove(entity);
            EntitiesByComponents[entityType] = entities;
        }
    }
}