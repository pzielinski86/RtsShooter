using UnityEngine;

namespace Game.Core.Unity
{
    public class UnityTransform:ITransform
    {
        private readonly Transform _transform;

        public UnityTransform(Transform transform)
        {
            _transform = transform;
        }

        public Vector3 Position
        {
            get { return _transform.position; }
            set { _transform.position = value; }
        }

        public Vector3 Forward => _transform.forward;

        public void LookAt(Vector3 vector3)
        {
            _transform.LookAt(vector3);
        }

        public void Rotate(float xAngle, float yAngle, float zAngle)
        {
            _transform.Rotate(xAngle,yAngle,zAngle);
        }

        public Vector3 EurelAngles
        {
            get { return _transform.eulerAngles; }
            set { _transform.eulerAngles = value; }
        }

        public void RotateAround(Vector3 point, Vector3 axis, float angle)
        {
            _transform.RotateAround(point,axis,angle);
        }

        public void Rotate(Quaternion quaternion)
        {
            _transform.rotation = quaternion;
        }

        public Vector3 Right => _transform.right;
    }
}