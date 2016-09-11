using UnityEngine;

namespace Game.Core.Unity
{
    public interface ITransform
    {
        Vector3 Position { get; set; }
        Vector3 Forward { get;  }
        void LookAt(Vector3 vector3);
        void Rotate(float xAngle, float yAngle, float zAngle);
        Vector3 EurelAngles { get; set; }
        Vector3 Right { get; }
        void RotateAround(Vector3 point, Vector3 axis, float angle);
        void Rotate(Quaternion quaternion);
    }
}