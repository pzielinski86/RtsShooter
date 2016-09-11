using Game.Core.Gun;
using Game.Core.Properties;
using Game.Core.Unity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Game.Core.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private Player _sut;
        private ICamera _cameraMock;
        private IPhysics _physicsMock;
        private ICharacterController _characterControllerMock;
        private BulletsHandler _bulletsHandler;

        [SetUp]
        public void Setup()
        {
            _cameraMock = Substitute.For<ICamera>();
            _characterControllerMock = Substitute.For<ICharacterController>();
            _physicsMock = Substitute.For<IPhysics>();

            _bulletsHandler = new BulletsHandler();
            _sut = new Player(_bulletsHandler, _cameraMock, _physicsMock, _characterControllerMock);
        }

        [Test]
        public void UpdateIK_Should_SetLookAtPosition_To_RayCastedObject_When_ThereIsRayCastedObject_In_Scene()
        {
            // arrange
            var raycastResult = new RaycastResult("test object", 1, new Vector3(1, 5, 1));
            _physicsMock.Raycast(Arg.Any<Vector3>(), Arg.Any<Vector3>()).Returns(raycastResult);

            // act
            _sut.UpdateIK();

            // assert
            _characterControllerMock.AnimationController.Received(1).SetLookAtPosition(raycastResult.Position);
        }

        [Test]
        public void Shoot_Should_ShootFromCurrentGun_To_RayCastedObject_When_ThereIsRayCastedObject_In_Scene()
        {
            // arrange
            var raycastResult = new RaycastResult("test object", 1, new Vector3(1, 5, 1));
            _physicsMock.Raycast(Arg.Any<Vector3>(), Arg.Any<Vector3>()).Returns(raycastResult);

            // act
            _sut.Shoot();

            // assert
            Assert.That(_bulletsHandler.Bullets.Count,Is.EqualTo(1));
        }

        [Test]
        public void Hit_ShouldSubtractHealth()
        {
            // arrange
            var bullet=new BasicBullet(null,Vector3.zero, Vector3.zero);
            float currentHealth = _sut.CurrentHealth;
            float expectedHealth = currentHealth - bullet.Damage;
            
            // act
            _sut.Hit(bullet);

            // assert       
            Assert.That(_sut.CurrentHealth,Is.EqualTo(expectedHealth));
        }

        [Test]
        public void Aim_ShouldChangeStateToAim()
        {
            // act
            _sut.Aim();

            Assert.That(_sut.State, Is.EqualTo(CharacterState.Aim));
        }

        [Test]
        public void Run_ShouldChangeStateToRun()
        {
            // act
            _sut.Run();

            Assert.That(_sut.State,Is.EqualTo(CharacterState.Run));
        }

        [Test]
        public void Run_ShouldStart_Run_Animation()
        {
            // act
            _sut.Run();

            _characterControllerMock.AnimationController.Received(1).Run();
        }

        [Test]
        public void Idle_ShouldChangeStateToIdle()
        {
            // act
            _sut.Idle();

            Assert.That(_sut.State, Is.EqualTo(CharacterState.Idle));
        }

        [Test]
        public void Idle_ShouldStart_Idle_Animation()
        {
            // act
            _sut.Idle();

            _characterControllerMock.AnimationController.Received(1).Idle();
        }
    }
}