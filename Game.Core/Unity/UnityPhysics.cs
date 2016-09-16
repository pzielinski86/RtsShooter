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
                var hitObject = unityRaycastHit.transform;

                var rayCastResult= new RaycastResult(hitObject.tag, unityRaycastHit.distance,unityRaycastHit.point);
                
                return rayCastResult;
            }

            return null;
        }

        public RaycastResult SphereRaycast(Vector3 position, Vector3 direction,float radius)
        {
            RaycastHit unityRaycastHit;
            if (Physics.SphereCast(position, radius, direction, out unityRaycastHit))
            {
                var hitObject = unityRaycastHit.transform;

                var rayCastResult = new RaycastResult(hitObject.tag, unityRaycastHit.distance, unityRaycastHit.point);

                return rayCastResult;
            }

            return null;
        }

        public bool Contains(Bounds largerBox, Bounds smallerBox)
        {
            return largerBox.Contains(smallerBox.min) && largerBox.Contains(smallerBox.max);
        }
    }
}