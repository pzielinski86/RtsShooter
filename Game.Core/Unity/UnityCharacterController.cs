using UnityEngine;

namespace Game.Core.Unity
{
    public class UnityCharacterController : ICharacterController
    {
        public ITransform Transform => _characterTransform.Body;
        public ITransform BarrelTransform => _characterTransform.Barrel;
        public ICharacterAnimationController AnimationController { get; private set; }
        private readonly CharacterController _characterController;
        private readonly CharacterTransform _characterTransform;

        public UnityCharacterController(ICharacterAnimationController animationController, CharacterController characterController, CharacterTransform characterTransform)
        {
            AnimationController = animationController;
            _characterController = characterController;
            _characterTransform = characterTransform;
        }

        public void SimpleMove(Vector3 newPosition)
        {
            _characterController.SimpleMove(newPosition);            
        }

        public Vector3 Center =>_characterController.transform.position+ _characterController.center;

        public Bounds Bounds => _characterController.bounds;
    }
}