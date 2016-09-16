using System;
using Game.Core.Gun;
using Game.Core.Math;
using Game.Core.Properties;
using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public class Npc : INpc
    {
        public event EventHandler Killed;
        private readonly CharacterTransform _transform;
        private readonly INpcController _npcController;
        private readonly IPhysics _physics;
        private GunBase _currentGun;
        private const float MinDistanceToPlayer = 10;

        public Npc(BulletsHandler bulletsHandler,
        CharacterTransform transform,
            INpcController npcController,
            IPhysics physics)
        {
            _transform = transform;
            _npcController = npcController;
            _physics = physics;
            CurrentGun = new BasicGun(bulletsHandler);
            Speed = 8f;
            CurrentHealth = MaxHealth = 500;
        }

        public GunBase CurrentGun
        {
            get { return _currentGun; }
            set
            {
                _currentGun = value;
                SetDefaultStoppingDistance();
            }
        }

        private void SetDefaultStoppingDistance()
        {
            _npcController.Astar.StoppingDistance = _currentGun.Range * 0.75f;
        }

        public float Speed { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public CharacterState State { get; private set; }

        public void Update(IWorldMap worldMap)
        {
            if (State == CharacterState.Killed)
                return;

            var remainingDist = Vector3.Distance(worldMap.Player.CharacterController.Transform.Position,
                _transform.Body.Position);

            if (remainingDist <= _npcController.Astar.StoppingDistance)
            {
                LookAtPlayer(worldMap);

                var result = _physics.Raycast(_transform.Body.Position, _transform.Body.Forward);
                var playerDist = Vector3.Distance(_transform.Body.Position,
                    worldMap.Player.CharacterController.Transform.Position);

                if (result != null && result.Distance < playerDist)
                {
                    TryToComeCloserToPlayer();
                    return;
                }
                else
                {
                    SetDefaultStoppingDistance();
                    _npcController.Astar.Stop();
                }

                Idle();
                Shoot(worldMap, playerDist);
            }
            else
            {
                _npcController.Astar.SetDestination(worldMap.Player.CharacterController.Transform.Position);
                Run();
            }
        }

        private void Shoot(IWorldMap worldMap, float playerDist)
        {
            AimAtPlayer(worldMap.Player);

            var result = _physics.SphereRaycast(_transform.Barrel.Position, _transform.Barrel.Forward,CurrentGun.BulletRadius);

            if (result != null && result.Distance <= playerDist && result.IsNpc)
            {
                return;
            }

            _npcController.Animation.Fire();
            CurrentGun.Shoot(_transform.Barrel.Position, _transform.Barrel.Forward);
        }

        public void Destroy()
        {
            _npcController.GameObject.Destroy();
        }

        public void Hit(BulletBase bullet)
        {
            CurrentHealth -= bullet.Damage;
            if (CurrentHealth <= 0)
                Kill();
            else
                _npcController.Animation.Hit();
        }

        public void SetPosition(Vector3 position)
        {
            _npcController.Astar.Enabled = false;
            _transform.Body.Position = position;
            _npcController.Astar.Enabled = true;
        }

        private void TryToComeCloserToPlayer()
        {
            var newStoppingDistance = _npcController.Astar.StoppingDistance - _npcController.Astar.StoppingDistance / 10f;
            _npcController.Astar.StoppingDistance = Mathf.Max(newStoppingDistance, MinDistanceToPlayer);
        }

        private void Run()
        {
            _npcController.Animation.Run();
            State = CharacterState.Run;
        }

        private void Idle()
        {
            _npcController.Animation.Idle();
            State = CharacterState.Idle;
        }

        private void Kill()
        {
            Killed?.Invoke(this, EventArgs.Empty);
            State = CharacterState.Killed;
            _npcController.Animation.Kill();
        }

        private void AimAtPlayer(IPlayer player)
        {
            var targetPos = player.CharacterController.Center - _transform.Barrel.Position;
            targetPos.y = _transform.Barrel.Forward.y;
            var npcPlayerAngleDiff = MathEx.AngleBetweenVector3(_transform.Barrel.Forward, targetPos);

            _transform.Body.Rotate(0, npcPlayerAngleDiff, 0);
        }

        private void LookAtPlayer(IWorldMap worldMap)
        {
            var targetPos = worldMap.Player.CharacterController.Center - _transform.Body.Position;
            targetPos.y = _transform.Body.Forward.y;
            var npcPlayerAngleDiff = MathEx.AngleBetweenVector3(_transform.Body.Forward, targetPos);

            _transform.Body.Rotate(0, npcPlayerAngleDiff, 0);
        }
    }
}