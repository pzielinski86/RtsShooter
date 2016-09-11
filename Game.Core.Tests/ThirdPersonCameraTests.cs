using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.Properties;
using Game.Core.Unity;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

namespace Game.Core.Tests
{
    [TestFixture]
    public class ThirdPersonCameraTests
    {
        private ThirdPersonCamera _sut;
        private ITransform _cameraTransformMock;
        private IInput _inputMock;
        private IPlayer _playerMock;

        [SetUp]
        public void SetUp()
        {
            _cameraTransformMock = Substitute.For<ITransform>();
            _inputMock = Substitute.For<IInput>();
            _playerMock = Substitute.For<IPlayer>();

            _sut = new ThirdPersonCamera(_cameraTransformMock, _inputMock);
        }

        [Test]
        public void ShouldRotatePlayer_When_MouseIsMovedHorizontally()
        {
            // arrange
            const float mouseX = 5f;
            _inputMock.GetMouseXDelta().Returns(mouseX);

            // act
            _sut.Update(_playerMock);

            // assert
            _playerMock.CharacterController.Transform.Rotate(0, mouseX, 0);
        }

        [Test]
        public void ShouldPositionCamera_BehindPlayer()
        {
            // arrange
            var playerBounds = new Bounds(Vector3.zero, Vector3.one * 19f);
            _playerMock.CharacterController.Center.Returns(new Vector3(3, 3, 3));
            _playerMock.CharacterController.Bounds.Returns(playerBounds);
            _playerMock.CharacterController.Transform.Forward.Returns(new Vector3(0, 0, 1));
            var zoom = _sut.Zoom;

            // act
            _sut.Update(_playerMock);

            // assert
            var expectedCameraPos = new Vector3(3, playerBounds.max.y + _sut.CameraPositionYOffset, 3 - zoom);
            Assert.That(_cameraTransformMock.Position, Is.EqualTo(expectedCameraPos));
        }

        [Test]
        public void CameraShouldLook_AbovePlayer()
        {
            // arrange
            var playerBounds = new Bounds(Vector3.zero, Vector3.one * 10f);
            _playerMock.CharacterController.Bounds.Returns(playerBounds);

            // act
            _sut.Update(_playerMock);

            // assert
            var lookAt = new Vector3(5, 5, 5);
            _cameraTransformMock.Received(1).LookAt(lookAt);
        }

        [Test]
        public void When_MouseIsMovedVerticallyUp_Then_CameraShouldUpAbovePlayer()
        {
            // arrange
            var playerBounds = new Bounds(Vector3.zero, Vector3.one * 20f);
            _playerMock.CharacterController.Bounds.Returns(playerBounds);
            _inputMock.GetMouseYDelta().Returns(500);

            // act
            _sut.Update(_playerMock);

            // assert
            var lookAt = new Vector3(10, 11, 10);
            _cameraTransformMock.Received(1).LookAt(lookAt);
        }

        [Test]
        public void RightMouseClick_Should_SwitchPlayerToAimMode()
        {
            // arrange
            _inputMock.GetRightMouseButton().Returns(true);

            // act
            _sut.Update(_playerMock);

            // assert
            _playerMock.Received(1).Aim();
        }

        [Test]
        public void When_PlayerIsInAimMode_Then_ReleasingRightMouseButton_ShouldSwitchPlayerToIdleMode()
        {
            // arrange
            _playerMock.State.Returns(CharacterState.Aim);
            _inputMock.GetRightMouseButton().Returns(false);

            // act
            _sut.Update(_playerMock);

            // assert
            _playerMock.Received(1).Idle();
        }
    }
}
