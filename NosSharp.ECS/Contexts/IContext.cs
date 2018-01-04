using System;
using NosSharp.ECS.Components;
using NosSharp.ECS.Entities;

namespace NosSharp.ECS.Contexts
{
    public interface IContext
    {
        IEntity GetEntity(long id);

        IEntity[] GetEntities();
        IEntity[] GetEntities<T>();
        IEntity[] GetEntities(Type type);

        IEntity[] GetEntitiesByComponent<T>();
        IEntity[] GetEntitiesByComponent(Type type);

        void RegisterEntity(IEntity entity);
        void UnregisterEntity(IEntity entity);
    }
}