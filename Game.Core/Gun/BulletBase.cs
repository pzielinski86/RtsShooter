using UnityEngine;

namespace Game.Core.Gun
{
    public abstract class BulletBase
    {
        protected BulletBase(GunBase gun,Vector3 position, Vector3 direction)
        {
            StartPosition = Position = position;
            Gun = gun;
            Damage = 5000;
            Direction = direction;
        }
        public Vector3 StartPosition { get; private set; }
        public Vector3 Position { get; protected set; }
        public GunBase Gun { get; private set; }
        public Vector3 Direction { get; protected set; }
        public float Speed { get; protected set; }
        public uint Damage { get; set; }

        public abstract void Update();

        public void Destroy()
        {
            Gun.BulletsHandler.Bullets.Remove(this);
        }
    }
}