using Game.Core.Properties;
using Game.Core.Unity;
using UnityEngine;


namespace Game.Core
{
    public class PlayerController
    {
        private readonly IPlayer _player;
        private readonly IInput _input;

        public PlayerController(IPlayer player, IInput input)
        {
            _player = player;
            _input = input;
        }

        public void Update()
        {
            if (_player.State != CharacterState.Run && _input.GetLeftMouseButton())
            {                
                _player.Shoot();
            }
            else if (_input.GetKey(KeyCode.W) || _input.GetKey(KeyCode.S) || _input.GetKey(KeyCode.A) || _input.GetKey(KeyCode.D))
            {
                if (_input.GetKey(KeyCode.W))
                {
                    _player.CharacterController.SimpleMove(_player.Speed * _player.CharacterController.Transform.Forward);
                }
                else if (_input.GetKey(KeyCode.S))
                {
                    _player.CharacterController.SimpleMove(-_player.Speed * _player.CharacterController.Transform.Forward);
                }
                else if (_input.GetKey(KeyCode.A))
                {
                    _player.CharacterController.SimpleMove(-_player.Speed * _player.CharacterController.Transform.Right);
                }
                else if (_input.GetKey(KeyCode.D))
                {
                    _player.CharacterController.SimpleMove(_player.Speed * _player.CharacterController.Transform.Right);
                }

                _player.Run();
            }
            else if (_player.State == CharacterState.Run)
            {
                _player.Idle();
            }
        }
    }
}