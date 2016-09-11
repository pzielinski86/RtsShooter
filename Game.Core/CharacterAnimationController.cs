using System.Collections.Generic;
using System.Linq;
using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public class CharacterAnimationController : ICharacterAnimationController
    {
        private readonly Animator _animationController;

        public CharacterAnimationController(Animator animationController )
        {
            _animationController = animationController;
        }


        public void Run()
        {
            _animationController.SetBool("run", true);
        }

        public void Idle()
        {
            _animationController.SetBool("run", false);
        }

        public void Hit()
        {
            _animationController.SetBool("hit", true);
        }

        public void Kill()
        {
            _animationController.SetBool("kill", true);
        }

        public void Fire()
        {
            _animationController.SetBool("fire", true);
        }

        public void SetLookAtPosition(Vector3 position)
        {
            _animationController.SetLookAtWeight(1, 1, 0, 0, 1);
            _animationController.SetLookAtPosition(position);
        }
    }
}