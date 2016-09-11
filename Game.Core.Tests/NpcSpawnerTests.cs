using Game.Core.Unity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Game.Core.Tests
{
    [TestFixture]
    public class NpcSpawnerTests
    {
        private IRandom _randomMock;

        [SetUp]
        public void Setup()
        {
            _randomMock = Substitute.For<IRandom>();
        }

        [Test]
        public void Should_CreateRandomNpc_At_RandomPosition()
        {
            // arrange
            var positions = new[] {Vector3.zero, Vector3.back, Vector3.right};
            var prototypes = new[]
            {
                Substitute.For<INpcPrototype>(), Substitute.For<INpcPrototype>(), Substitute.For<INpcPrototype>(),
                Substitute.For<INpcPrototype>()
            };

            var sut = new NpcSpawner(_randomMock, positions, prototypes);
            var npcMock = Substitute.For<INpc>();

            const int randomNpcIndex = 3;
            const int randomPosIndex = 1;
            prototypes[randomPosIndex].Create().Returns(npcMock);

            _randomMock.Range(0, positions.Length).Returns(randomPosIndex);
            _randomMock.Range(0, prototypes.Length).Returns(randomNpcIndex);

            // act
            sut.CreateRandomNpc();

            // assert
            prototypes[randomNpcIndex].Create().Received(randomPosIndex);
            npcMock.SetPosition(positions[randomPosIndex]);        
        }
    }
}