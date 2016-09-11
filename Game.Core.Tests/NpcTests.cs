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
            _characterTransform=new CharacterTransform(null,null);

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
            var bullet = Substitute.For<BulletBase>();
            float currentHealth = _sut.CurrentHealth;
            float expectedHealth = currentHealth - bullet.Damage;

            // act
            _sut.Hit(bullet);

            // assert       
            Assert.That(_sut.CurrentHealth, Is.EqualTo(expectedHealth));
        }
    }
}