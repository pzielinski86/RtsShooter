using UnityEngine;

namespace Game.Core.Unity
{
    public interface ICharacterController
    {
        void SimpleMove(Vector3 newPosition);
        Vector3 Center { get; }

        Bounds Bounds { get; }
        ITransform Transform { get; }
        ITransform BarrelTransform { get; }
        ICharacterAnimationController AnimationController { get; }
    }
}