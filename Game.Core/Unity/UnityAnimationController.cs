using UnityEngine;

namespace Game.Core.Unity
{
    public class UnityAnimationController:IAnimationController
    {
        private readonly Animation _animation;

        public UnityAnimationController(Animation animation)
        {
            _animation = animation;
        }

        public void SetAnimation(string clipName)
        {
            _animation.CrossFade(clipName);
        }

        public bool IsPlaying => _animation.isPlaying;
    }
}