using System;
using IComponent = NosSharp.ECS.Components.IComponent;

namespace NosSharp.ECS.Test
{
    public class HealthComponent : IComponent
    {
        private ulong _hp = 10;
        private ulong _mp = 10;

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
            set { _hp = value; }
        }

        public ulong Mp
        {
            get { return _mp; }
            set { _mp = value; }
        }

        public Type Type
        {
            get { return typeof(HealthComponent); }
        }
    }
}