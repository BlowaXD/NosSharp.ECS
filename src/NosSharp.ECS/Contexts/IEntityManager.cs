using System;
using System.Collections.Generic;
using NosSharp.ECS.Components;
using NosSharp.ECS.Entities;

namespace NosSharp.ECS.Contexts
{
    public interface IEntityManager
    {
        /// <summary>
        /// Gets the IEntity with Id id
        /// </summary>
        /// <param name="id">id of <see cref="IEntity"/> to get</param>
        /// <returns></returns>
        IEntity GetEntity(long id);

        /// <summary>
        /// Gets all entities in the context
        /// </summary>
        /// <returns></returns>
        IEntity[] GetEntities();


        /// <summary>
        /// Gets all entities with the <see cref="IComponent"/> of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEntity or null of none</returns>
        IEntity[] GetEntities<T>();

        /// <summary>
        /// Gets all entities with the <see cref="IComponent"/> of type given in parameter
        /// </summary>
        /// <param name="type">Type of <see cref="IComponent"/> that the entity has to contain</param>
        /// <returns></returns>
        IEntity[] GetEntities(Type type);

        /// <summary>
        /// Register the Entity in the actual <see cref="IEntityManager"/>
        /// </summary>
        /// <param name="entity"></param>
        void RegisterEntity(IEntity entity);

        void RegisterEntity(IEntity[] entities);

        /// <summary>
        /// Unregister the Entity in the actual <see cref="IEntityManager"/>
        /// </summary>
        /// <param name="entity"></param>
        void UnregisterEntity(IEntity entity);

        void UnregisterEntity(IEntity[] entities);
    }
}