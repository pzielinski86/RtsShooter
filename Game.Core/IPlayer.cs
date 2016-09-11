using Game.Core.Gun;
using Game.Core.Properties;
using Game.Core.Unity;

namespace Game.Core
{
    public interface IPlayer
    {
        ICharacterController CharacterController { get; }
        GunBase CurrentGun { get; }
        float Speed { get; set; }
        CharacterState State { get; }
        uint CurrentHealth { get; }
        uint MaxHealth { get; }
        void Aim();
        void UpdateIK();
        void Shoot();
        void Idle();
        void Run();
        void Hit(BulletBase bullet);
    }
}