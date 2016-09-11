using UnityEngine;

namespace Game.Core.Unity
{
    public class UnityPhysics:IPhysics
    {
        public RaycastResult Raycast(Vector3 position, Vector3 currentDirection)
        {
            RaycastHit unityRaycastHit;
            if (Physics.Raycast(position, currentDirection, out unityRaycastHit))
            {
                string objectName = null;

                if (unityRaycastHit.transform != null)
                    objectName = unityRaycastHit.transform.name;

                return new RaycastResult(objectName, unityRaycastHit.distance,unityRaycastHit.point);
            }

            return new RaycastResult(null, float.MaxValue,Vector3.zero);
        }

        public bool Contains(Bounds largerBox, Bounds smallerBox)
        {
            return largerBox.Contains(smallerBox.min) && largerBox.Contains(smallerBox.max);
        }
    }
}