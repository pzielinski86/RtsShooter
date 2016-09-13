using UnityEngine;

namespace Game.Core.Unity
{
    public interface IGameObject
    {
        void Destroy();
        IGameObject Create();
        ITransform Transform { get; }
        Bounds Bounds { get; }
    }
}