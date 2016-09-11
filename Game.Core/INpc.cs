using System;
using Game.Core.Gun;
using Game.Core.Properties;
using UnityEngine;

namespace Game.Core
{
    public interface INpc
    {
        event EventHandler Killed;
        GunBase CurrentGun { get; set; }
        float Speed { get; }
        float CurrentHealth { get; }
        float MaxHealth { get; }
        CharacterState State { get; }
        void Update(IWorldMap worldMap);
        void Destroy();
        void Hit(BulletBase bullet);
        void SetPosition(Vector3 position);
    }
}