using UnityEngine;

namespace Game.Core.Unity
{
    public class UnityCamera : ICamera
    {
        public Ray GetCrosshairRay()
        {
            var screenPointToRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

            return screenPointToRay;
        }
    }
}