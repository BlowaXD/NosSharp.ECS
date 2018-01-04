using System;
using NosSharp.ECS.Components;

namespace NosSharp.ECS.Entity
{
    public interface IEntity
    {
        long Id { get; }

        #region Components

        /// <summary>
        /// Check and returns if the <see cref="IEntity"/> has a Component of Type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>true if has compoentn</returns>
        bool HasComponent<T>();

        bool HasComponent(Type type);


        IComponent GetComponent<T>();
        IComponent GetComponent(Type type);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IComponent[] GetComponents();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IComponent[] GetComponents<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IComponent[] GetComponents(Type type);


        /// <summary>
        /// Add a Compoonent of type <see cref="Type"/> in IEntity
        /// </summary>
        /// <param name="type"></param>
        /// <param name="component"></param>
        void AddComponent(Type type, IComponent component);

        void AddComponent<T>(IComponent component);


        bool RemoveComponent<T>();

        bool RemoveComponent(Type type);

        #endregion

        void Destroy();
    }
}