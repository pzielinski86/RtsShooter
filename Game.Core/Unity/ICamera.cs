using UnityEngine;

namespace Game.Core.Unity
{
    public interface ICamera
    {
        Ray GetCrosshairRay();
    }
}