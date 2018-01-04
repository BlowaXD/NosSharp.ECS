using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using NosSharp.ECS.Components;
using NosSharp.ECS.Entity;

namespace NosSharp.ECS.Context
{
    public interface IContext
    {
        IEntity[] GetEntities();
        IEntity[] GetEntities<T>();
        IEntity[] GetEntities(Type type);

        IEntity[] GetEntitiesByComponent<T>();
        IEntity[] GetEntitiesByComponent(Type type);

        IComponent[] GetComponents();
        IComponent[] GetComponents<T>();
        IComponent[] GetComponents(Type type);

        void RegisterEntity(IEntity entity);
        void UnregisterEntity(IEntity entity);
    }
}