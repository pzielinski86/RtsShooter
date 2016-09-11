using Game.Core.Properties;
using Game.Core.Unity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Game.Core.Tests
{
    [TestFixture]
    public class PlayerControllerTests
    {
        private PlayerController _sut;
        private IPlayer _playerMock;
        private IInput _inputMock;

        [SetUp]
        public void Setup()
        {
            _playerMock = Substitute.For<IPlayer>();
            _inputMock = Substitute.For<IInput>();

            _sut = new PlayerController(_playerMock, _inputMock);
        }

        [Test]
        public void When_StateIsNotRun_And_LeftMouseButtonIsClicked_Then_PlayerShouldShoot()
        {
            // arrange
            _playerMock.State.Returns(CharacterState.Aim);
            _inputMock.GetLeftMouseButton().Returns(true);

            // act            
            _sut.Update();

            // assert
            _playerMock.Received(1).Shoot();

        }

        [Test]
        public void When_NoControlKeysArePressed_And_CurrentStateIsRun_Then_Player_GoesTo_Idle_State()
        {
            // arrange
            _playerMock.State.Returns(CharacterState.Run);

            // act            
            _sut.Update();

            // assert
            _playerMock.Received(1).Idle();
        }

        [Test]
        public void When_W_IsPressed_Then_PlayerGoesForward()
        {
            // assert
            _inputMock.GetKey(KeyCode.W).Returns(true);
            _playerMock.CharacterController.Transform.Forward.Returns(Vector3.forward);
            float speed = 5f;

            _playerMock.Speed.Returns(speed);
            // act
            _sut.Update();

            // assert
            Vector3 expectedNewPosition = new Vector3(0, 0, speed);
            _playerMock.CharacterController.Received(1).SimpleMove(expectedNewPosition);
        }

        [Test]
        public void When_S_IsPressed_Then_PlayerGoesBackward()
        {
            // assert
            _inputMock.GetKey(KeyCode.S).Returns(true);
            _playerMock.CharacterController.Transform.Forward.Returns(Vector3.forward);
            float speed = 5f;

            _playerMock.Speed.Returns(speed);
            // act
            _sut.Update();

            // assert
            Vector3 expectedNewPosition = new Vector3(0, 0, -speed);
            _playerMock.CharacterController.Received(1).SimpleMove(expectedNewPosition);
        }

        [Test]
        public void When_A_IsPressed_Then_PlayerGoesLeft()
        {
            // assert
            _inputMock.GetKey(KeyCode.A).Returns(true);
            _playerMock.CharacterController.Transform.Right.Returns(Vector3.right);
            float speed = 5f;

            _playerMock.Speed.Returns(speed);
            // act
            _sut.Update();

            // assert
            Vector3 expectedNewPosition = new Vector3(-speed, 0, 0);
            _playerMock.CharacterController.Received(1).SimpleMove(expectedNewPosition);
        }

        [Test]
        public void When_D_IsPressed_Then_PlayerGoesRight()
        {
            // assert
            _inputMock.GetKey(KeyCode.D).Returns(true);
            _playerMock.CharacterController.Transform.Right.Returns(Vector3.right);
            float speed = 5f;

            _playerMock.Speed.Returns(speed);
            // act
            _sut.Update();

            // assert
            Vector3 expectedNewPosition = new Vector3(speed, 0, 0);
            _playerMock.CharacterController.Received(1).SimpleMove(expectedNewPosition);
        }

        [TestCase(KeyCode.W)]
        [TestCase(KeyCode.S)]
        [TestCase(KeyCode.A)]
        [TestCase(KeyCode.D)]
        public void When_Any_ControlKeyIsPressed_Then_PlayerStartRunning(KeyCode keyCode)
        {
            // assert
            _inputMock.GetKey(keyCode).Returns(true);

            // act
            _sut.Update();

            // assert
            _playerMock.Received(1).Run();
        }
    }
}