using Game.Core.Gun;
using Game.Core.Properties;
using Game.Core.Unity;

namespace Game.Core
{
    public class Player : IPlayer
    {
        private readonly ICamera _camera;
        private readonly IPhysics _physics;
        public ICharacterController CharacterController { get; }

        public Player(BulletsHandler bulletsHandler,
            ICamera camera,
            IPhysics physics,
            ICharacterController characterCharacterController)
        {
            Speed = 5f;
            _camera = camera;
            _physics = physics;
            CharacterController = characterCharacterController;
            CurrentGun = new BasicGun(bulletsHandler);

            CurrentHealth = MaxHealth = 1000;
        }

        public GunBase CurrentGun { get; private set; }
        public float Speed { get; set; }
        public CharacterState State { get; private set; }
        public uint CurrentHealth { get; private set; }
        public uint MaxHealth { get; private set; }

        public void Aim()
        {
            State = CharacterState.Aim;
        }

        public void UpdateIK()
        {
            var crosshairRay = _camera.GetCrosshairRay();
            var hitInfo = _physics.Raycast(crosshairRay.origin, crosshairRay.direction);

            if (hitInfo.ObjectName != null)
            {
                CharacterController.AnimationController.SetLookAtPosition(hitInfo.Position);
            }
        }

        public void Shoot()
        {
            State = CharacterState.Aim;

            var crosshairRay = _camera.GetCrosshairRay();

            var hitInfo = _physics.Raycast(crosshairRay.origin, crosshairRay.direction);

            if (hitInfo.ObjectName != null)
            {
                CharacterController.AnimationController.Fire();
                CurrentGun.Shoot(CharacterController.BarrelTransform.Position, (hitInfo.Position - CharacterController.BarrelTransform.Position).normalized);
            }
        }

        public void Idle()
        {
            CharacterController.AnimationController.Idle();
            State = CharacterState.Idle;
        }

        public void Run()
        {
            CharacterController.AnimationController.Run();
            State = CharacterState.Run;
        }

        public void Hit(BulletBase bullet)
        {
            CurrentHealth -= bullet.Damage;
            CharacterController.AnimationController.Hit();
        }
    }
}