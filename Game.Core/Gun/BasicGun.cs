using UnityEngine;

namespace Game.Core.Gun
{
    public class BasicGun:GunBase
    {
        public BasicGun(BulletsHandler bulletsHandler) : base(bulletsHandler)
        {
            BulletRadius = 0.2f;
        }

        protected override void DoShoot(Vector3 startPos, Vector3 direction)
        {
            BulletsHandler.Bullets.Add(new BasicBullet(this,startPos,direction));
        }
    }
}