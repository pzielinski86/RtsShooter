using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Game.Core.Math;
using Game.Core.Properties;
using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public class ThirdPersonCamera
    {
        private readonly ITransform _cameraTransform;
        private readonly IInput _input;
        private float _cameraLookAtOffsetY = 0;

        public ThirdPersonCamera(ITransform cameraTransform, IInput input)
        {
            _cameraTransform = cameraTransform;
            _input = input;
        }

        public float Zoom => 5f;
        public float CameraPositionYOffset => 1.8f;

        public void Update(IPlayer player)
        {
            player.CharacterController.Transform.Rotate(0, _input.GetMouseXDelta(), 0);

            UpdateCameraPosition(player);
            SwitchToAimModeIfRequired(player);

            UpdateCameraLookAt(player);
        }

        private void UpdateCameraLookAt(IPlayer player)
        {
            _cameraLookAtOffsetY = MathEx.Clamp(_cameraLookAtOffsetY + _input.GetMouseYDelta() / 50f, -1, 3);
            _cameraTransform.LookAt(player.CharacterController.Bounds.max + Vector3.up * _cameraLookAtOffsetY);
        }

        private void SwitchToAimModeIfRequired(IPlayer player)
        {
            if (_input.GetRightMouseButton())
                player.Aim();
            else
            {
                if (player.State == CharacterState.Aim)
                {
                    player.Idle();
                }
            }
        }

        private void UpdateCameraPosition(IPlayer player)
        {
            var playerCenter = player.CharacterController.Center;

            var angle = -MathEx.AngleBetweenVector3(Vector3.forward, player.CharacterController.Transform.Forward);

            float newX = playerCenter.x + Zoom *(float) System.Math.Sin(angle*Mathf.Deg2Rad);
            float newZ = playerCenter.z - Zoom * (float)System.Math.Cos(angle* Mathf.Deg2Rad);

            _cameraTransform.Position = new Vector3(newX, player.CharacterController.Bounds.max.y + CameraPositionYOffset, newZ);
        }
    }
}
