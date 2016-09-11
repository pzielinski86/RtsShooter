using System;
using Game.Core.Gun;
using Game.Core.Properties;
using Game.Core.Unity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Game.Core.Tests
{
    [TestFixture]
    public class NpcTests
    {
        private Npc _sut;
        private CharacterTransform _characterTransform;
        private IPhysics _physicsMock;
        private INpcController _npcControllerMock;
        private BulletsHandler _bulletsHandler;

        [SetUp]
        public void Setup()
        {
            _npcControllerMock = Substitute.For<INpcController>();
            _physicsMock = Substitute.For<IPhysics>();
            _characterTransform = new CharacterTransform(Substitute.For<ITransform>(), Substitute.For<ITransform>());

            _bulletsHandler = new BulletsHandler();

            _sut = new Npc(_bulletsHandler, _characterTransform, _npcControllerMock, _physicsMock);
        }

        [Test]
        public void Destroy_ShouldRemoveGameObject()
        {
            // act
            _sut.Destroy();

            // assert
            _npcControllerMock.GameObject.Received(1).Destroy();
        }

        [Test]
        public void Hit_ShouldSubtractHealth()
        {
            // arrange
            const uint bulletDamage = 2;
            var bullet = new StubnBulletWithDamage(bulletDamage);

            float expectedHealth = _sut.CurrentHealth - bulletDamage;

            // act
            _sut.Hit(bullet);

            // assert       
            Assert.That(_sut.CurrentHealth, Is.EqualTo(expectedHealth));
        }

        [Test]
        public void Hit_ShouldKillNpc_When_Damage_ExceedsCurrentHealth()
        {
            // arrange
            float bulletDamage = _sut.CurrentHealth + 1f;
            var bullet = new StubnBulletWithDamage(bulletDamage);
            var killedEvent = Substitute.For<EventHandler>();
            _sut.Killed += killedEvent;

            // act
            _sut.Hit(bullet);

            // assert       
            Assert.That(_sut.State, Is.EqualTo(CharacterState.Killed));
            _npcControllerMock.Animation.Received(1).Kill();
            killedEvent.Received(1).Invoke(_sut, EventArgs.Empty);
        }

        [Test]
        public void When_Player_Is_InGunRange_Then_NpcShouldLookAtHim()
        {
            // arrange
            var gunRange = _sut.CurrentGun.Range;
            _npcControllerMock.Astar.RemainingDistance.Returns(gunRange * 0.5f);
            var worldMapMock = Substitute.For<IWorldMap>();

            _characterTransform.Body.Position.Returns(Vector3.zero);
            _characterTransform.Body.Forward.Returns(-Vector3.forward);

            worldMapMock.Player.CharacterController.Center.Returns(Vector3.right);
            // act
            _sut.Update(worldMapMock);

            // assert
            _characterTransform.Body.Received(1).Rotate(0,90,0);
        }

        [Test]
        public void When_Player_Is_InGunRange_Then_NpcShouldStop()
        {
            // arrange
            var gunRange = _sut.CurrentGun.Range;
            _npcControllerMock.Astar.RemainingDistance.Returns(gunRange * 0.5f);
            var worldMapMock = Substitute.For<IWorldMap>();

            // act
            _sut.Update(worldMapMock);

            // assert
            Assert.That(_sut.State,Is.EqualTo(CharacterState.Idle));
        }

        [Test]
        public void When_Player_IsNot_InGunRange_Then_NpcShouldRun()
        {
            // arrange            
            _npcControllerMock.Astar.RemainingDistance.Returns(150);
            var worldMapMock = Substitute.For<IWorldMap>();

            // act
            _sut.Update(worldMapMock);

            // assert
            Assert.That(_sut.State, Is.EqualTo(CharacterState.Run));
            _npcControllerMock.Animation.Received(1).Run();
        }

        [Test]
        public void When_Player_Is_InGunRange_Then_NpcShouldAim()
        {
            // arrange
            var gunRange = _sut.CurrentGun.Range;
            _npcControllerMock.Astar.RemainingDistance.Returns(gunRange * 0.5f);
            var worldMapMock = Substitute.For<IWorldMap>();

            _characterTransform.Barrel.Position.Returns(Vector3.zero);
            _characterTransform.Barrel.Forward.Returns(-Vector3.forward);

            worldMapMock.Player.CharacterController.Center.Returns(Vector3.right);

            // act
            _sut.Update(worldMapMock);

            // assert
            _characterTransform.Body.Received(1).Rotate(0, 90, 0);
        }

        [Test]
        public void When_Player_Is_InGunRange_Then_NpcShouldShoot()
        {
            // arrange
            var gunRange = _sut.CurrentGun.Range;
            _npcControllerMock.Astar.RemainingDistance.Returns(gunRange * 0.5f);
            var worldMapMock = Substitute.For<IWorldMap>();

            // act
            _sut.Update(worldMapMock);

            // assert
            _npcControllerMock.Animation.Received(1).Fire();
            Assert.That(_bulletsHandler.Bullets.Count,Is.EqualTo(1));
        }

        [Test]
        public void When_Player_Is_InGunRange_But_There_IsObstacle_BetweenThem_Then_NpcShouldComeCloserBy10Percent()
        {
            // arrange
            
            // obstacle
            _physicsMock.Raycast(Arg.Any<Vector3>(), Arg.Any<Vector3>())
                .Returns(new RaycastResult("obstacle", 5, Vector3.zero));
            var worldMapMock = Substitute.For<IWorldMap>();

            _npcControllerMock.Astar.RemainingDistance.Returns(100);
            _npcControllerMock.Astar.StoppingDistance = 100;
            _characterTransform.Body.Position.Returns(Vector3.zero);
            worldMapMock.Player.CharacterController.Transform.Position.Returns(new Vector3(0, 0, 50));

            // act
            _sut.Update(worldMapMock);

            // assert
            Assert.That(_npcControllerMock.Astar.StoppingDistance, Is.EqualTo(90));
        }

        [Test]
        public void When_Player_Is_InGunRange_And_There_IsObstacle_BetweenThem_And_Then_NpcShouldNot_ComeCloser_Than10Units()
        {
            // arrange

            // obstacle
            _physicsMock.Raycast(Arg.Any<Vector3>(), Arg.Any<Vector3>())
                .Returns(new RaycastResult("obstacle", 5, Vector3.zero));
            var worldMapMock = Substitute.For<IWorldMap>();

            _npcControllerMock.Astar.RemainingDistance.Returns(10.1f);
            _npcControllerMock.Astar.StoppingDistance = 10.1f;
            _characterTransform.Body.Position.Returns(Vector3.zero);
            worldMapMock.Player.CharacterController.Transform.Position.Returns(new Vector3(0, 0, 50));

            // act
            _sut.Update(worldMapMock);

            // assert
            Assert.That(_npcControllerMock.Astar.StoppingDistance, Is.EqualTo(10));
        }

        [Test]
        public void SetPosition_Should_DisableAstar_SetPosition_And_ThenReenableAstar()
        {
            // arrange
            Vector3 newPosition=new Vector3(5,1,6);
            
            // act
            _sut.SetPosition(newPosition);

            // assert
            Received.InOrder(() =>
            {
                _npcControllerMock.Astar.Enabled = false;
                _characterTransform.Body.Position = newPosition;
                _npcControllerMock.Astar.Enabled = true;
            });
        }

        class StubnBulletWithDamage : BulletBase
        {
            public StubnBulletWithDamage(float damage) : base(null, Vector3.zero, Vector3.zero)
            {
                Damage = damage;
            }

            public override void Update()
            {
            }
        }
    }
}