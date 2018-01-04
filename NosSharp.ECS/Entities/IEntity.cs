using System;
using NosSharp.ECS.Components;

namespace NosSharp.ECS.Entities
{
    public interface IEntity
    {
        long Id { get; }
        Type EntityType { get; }

        #region Components

        bool HasComponent<T>();
        bool HasComponent(Type type);


        IComponent GetComponent<T>();
        IComponent GetComponent(Type type);


        IComponent[] GetComponents();
        IComponent[] GetComponents<T>();
        IComponent[] GetComponents(Type type);

        void AddComponent(IComponent component, Type type);
        void AddComponent<T>(IComponent component) where T : class;

        bool RemoveComponent<T>();
        bool RemoveComponent(Type type);

        #endregion

        void Destroy();
    }
}