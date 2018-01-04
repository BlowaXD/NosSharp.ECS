using System;
using System.ComponentModel;
using IComponent = NosSharp.ECS.Components.IComponent;

namespace NosSharp.ECS.Test
{
    public class HealthComponent : IComponent
    {
        private readonly ulong _hp = 10;
        private readonly ulong _mp = 10;

        public HealthComponent()
        {
            HpMax = 100;
            MpMax = 100;
        }

        public ulong HpMax { get; }
        public ulong MpMax { get; }

        public ulong Hp
        {
            get { return _hp; }
        }

        public ulong Mp
        {
            get { return _mp; }
        }

        public Type Type
        {
            get { return typeof(HealthComponent); }
        }
    }
}