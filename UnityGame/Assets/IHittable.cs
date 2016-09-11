using Game.Core.Gun;

namespace Assets
{
    public interface IHittable
    {
        void Hit(BulletBase bullet);
    }
}