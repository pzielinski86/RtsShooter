namespace Game.Core.Unity
{
    public interface IAnimationController
    {
        void SetAnimation(string clipName);
        bool IsPlaying { get; }
    }
}