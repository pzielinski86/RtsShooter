using UnityEngine;

namespace Game.Core.Unity
{
    public interface IPhysics
    {
        RaycastResult Raycast(Vector3 position, Vector3 currentDirection);
        RaycastResult SphereRaycast(Vector3 position, Vector3 direction, float radius);
        bool Contains(Bounds largerBox, Bounds smallerBox);
    }
}