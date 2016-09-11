using UnityEngine;

namespace Game.Core.Gun
{
    public class BasicBullet:BulletBase
    {
        public BasicBullet(GunBase gun, Vector3 position, Vector3 direction) : base(gun, position, direction)
        {
            Speed = 0.2f;
        }

        public override void Update()
        {
            Position += Direction*Speed;

            if (Vector3.Distance(Position, StartPosition) >= Gun.Range)
                Gun.BulletsHandler.Bullets.Remove(this);

        }
    }
}