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
        private float _cameraOffsetY = 0;

        public ThirdPersonCamera(ITransform cameraTransform, IInput input)
        {
            _cameraTransform = cameraTransform;
            _input = input;
        }

        public float Zoom => 5f;

        public void Update(Player player)
        {
            player.CharacterController.Transform.Rotate(0, _input.GetMouseXDelta(), 0);

            UpdateCameraPosition(player);
            SwitchToAimModeIfRequired(player);

           UpdateCameraLookAt(player);
        }

        private void UpdateCameraLookAt(Player player)
        {
            _cameraOffsetY = Mathf.Clamp(_cameraOffsetY + Input.GetAxis("Mouse Y") / 50f, -1, 1);
            _cameraTransform.LookAt(player.CharacterController.Bounds.max + Vector3.up * _cameraOffsetY);
        }

        private void SwitchToAimModeIfRequired(Player player)
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

        private void UpdateCameraPosition(Player player)
        {
            var playerCenter = player.CharacterController.Center;

            var angle = -MathEx.AngleBetweenVector3(Vector3.forward, player.CharacterController.Transform.Forward);

            var newX = playerCenter.x + Zoom * Mathf.Sin(angle * Mathf.Deg2Rad);
            var newZ = playerCenter.z - Zoom * Mathf.Cos(angle * Mathf.Deg2Rad);

            _cameraTransform.Position = new Vector3(newX, player.CharacterController.Bounds.max.y+1.8f, newZ);            
        }
    }
}
