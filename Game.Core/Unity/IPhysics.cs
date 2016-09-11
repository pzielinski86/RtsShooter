using UnityEngine;

namespace Game.Core.Unity
{
    public interface IPhysics
    {
        RaycastResult Raycast(Vector3 position, Vector3 currentDirection);
        bool Contains(Bounds largerBox, Bounds smallerBox);
    }
}