using System;

namespace NosSharp.ECS.Components
{
    public interface IComponent
    {
        Type Type { get; }
    }
}