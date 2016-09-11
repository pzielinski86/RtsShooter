using UnityEngine;

namespace Game.Core
{
    public interface ICharacterAnimationController
    {
        void Run();
        void Idle();
        void Hit();
        void Kill();
        void Fire();
        void SetLookAtPosition(Vector3 position);
    }
}