using System.Configuration;
using Game.Core.Collections;
using Game.Core.Properties;
using Game.Core.Unity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Game.Core.Tests
{
    [TestFixture]
    public class NpcAttacksManagerTests
    {
        private IWorldMap _worldMapMock;

        [SetUp]
        public void Setup()
        {
            _worldMapMock = Substitute.For<IWorldMap>();
            _worldMapMock.Enemies.Returns(new ObservableCollection<INpc>());
        }

        [Test]
        public void Should_AddNewNpc_AfterSpawnDelay()
        {
            // arrange
            var waveAttack = new WaveAttackInfo();
            waveAttack.SpawnDelayInSecs = 10;
            waveAttack.TotalEnemies = 15;

            var spawner = Substitute.For<INpcSpawner>();
            var sut = new NpcAttacksManager(_worldMapMock, spawner,new[] {waveAttack});

            // act 
            sut.Start(0);
            sut.Update(waveAttack.SpawnDelayInSecs + 1);

            //// act
            spawner.Received(1).CreateRandomNpc();
        }

        [Test]
        public void ShouldNot_AddNewNpc_BeforeSpawnDelay()
        {
            // arrange
            var waveAttack = new WaveAttackInfo();
            waveAttack.SpawnDelayInSecs = 10;
            waveAttack.TotalEnemies = 15;

            var spawner = Substitute.For<INpcSpawner>();
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack });

            // act 
            sut.Start(0);
            sut.Update(waveAttack.SpawnDelayInSecs - 1);

            // act
            spawner.Received(0).CreateRandomNpc();
        }


        [Test]
        public void ShouldStart_NewWave_Whew_AllEnemiesAreKilled()
        {
            // arrange
            var waveAttack1 = new WaveAttackInfo
            {
                SpawnDelayInSecs = 10,
                TotalEnemies = 0
            };

            var waveAttack2 = new WaveAttackInfo
            {
                SpawnDelayInSecs = 10,
                TotalEnemies = 0
            };

            var spawner = Substitute.For<INpcSpawner>();
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack1, waveAttack2 });

            var killedNpc = Substitute.For<INpc>();
            killedNpc.State.Returns(CharacterState.Killed);
            _worldMapMock.Enemies.Add(killedNpc);

            // act 
            sut.Start(0);
            sut.Update(waveAttack1.SpawnDelayInSecs);

            // act
            Assert.That(sut.CurrentWaveAttackInfo,Is.EqualTo(waveAttack2));
        }

        [Test]
        public void ShouldChangeStateToFinished_WhenAllWavesAreCompleted()
        {
            // arrange
            var waveAttack1 = new WaveAttackInfo
            {
                SpawnDelayInSecs = 10,
                TotalEnemies = 0
            };      

            var spawner = Substitute.For<INpcSpawner>();
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack1 });

            var killedNpc = Substitute.For<INpc>();
            killedNpc.State.Returns(CharacterState.Killed);
            _worldMapMock.Enemies.Add(killedNpc);

            // act 
            sut.Start(0);
            sut.Update(waveAttack1.SpawnDelayInSecs);
            sut.Update(waveAttack1.SpawnDelayInSecs);

            // act
            Assert.That(sut.Phase, Is.EqualTo(AttackPhase.Finished));
        }

        [Test]
        public void ShouldChangeStateToPreparing_When_BeforeNewWaveIsStarted()
        {
            // arrange
            var waveAttack1 = new WaveAttackInfo
            {
                SpawnDelayInSecs = 10,
                TotalEnemies = 0,
                WarmupTimeInSecs = 10
            };

            var spawner = Substitute.For<INpcSpawner>();
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack1 });
           
            // act 
            sut.Start(0);
            sut.Update(2);

            // act
            Assert.That(sut.Phase, Is.EqualTo(AttackPhase.Preparing));
        }

        [Test]
        public void ShouldChangeStateTAttacking_When_WaveIsStarted()
        {
            // arrange
            var waveAttack1 = new WaveAttackInfo
            {
                SpawnDelayInSecs = 10,
                TotalEnemies = 1,
                WarmupTimeInSecs = 0
            };

            var spawner = Substitute.For<INpcSpawner>();
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack1 });

            // act 
            sut.Start(0);
            sut.Update(11);

            // act
            Assert.That(sut.Phase, Is.EqualTo(AttackPhase.Attacking));
        }

        [Test]
        public void ShouldNot_AddNewNpc_AfterSpawnDelay_When_AllEnemiesInCurrentTurn_AreAlreadyCreated()
        {
            // arrange
            var waveAttack = new WaveAttackInfo();
            waveAttack.SpawnDelayInSecs = 10;
            waveAttack.TotalEnemies = 0;

            var spawner = Substitute.For<INpcSpawner>();
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack });

            // act 
            sut.Start(0);
            sut.Update(waveAttack.SpawnDelayInSecs + 1);

            //// act
            spawner.Received(0).CreateRandomNpc();
        }

        [Test]
        public void Should_RemoveAllEnemies_Before_FirstNewNpcIsCreated()
        {
            // arrange
            var waveAttack = new WaveAttackInfo();
            waveAttack.SpawnDelayInSecs = 10;
            waveAttack.TotalEnemies = 1;

            var spawner = Substitute.For<INpcSpawner>();
            _worldMapMock.Enemies.Add(null);
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack });

            // act 
            sut.Start(0);
            sut.Update(waveAttack.SpawnDelayInSecs + 1);

            //// act
            Assert.That(_worldMapMock.Enemies.Count,Is.EqualTo(0));
        }

        [Test]
        public void ShouldNot_RemoveAllEnemies_Before_SecondNewNpcIsCreated()
        {
            // arrange
            var waveAttack = new WaveAttackInfo();
            waveAttack.SpawnDelayInSecs = 10;
            waveAttack.TotalEnemies = 5;

            var spawner = Substitute.For<INpcSpawner>();            
            var sut = new NpcAttacksManager(_worldMapMock, spawner, new[] { waveAttack });

            // act 
            sut.Start(0);
            sut.Update(waveAttack.SpawnDelayInSecs + 1);
            _worldMapMock.Enemies.Add(null);
            sut.Update(waveAttack.SpawnDelayInSecs + 1);

            //// act
            Assert.That(_worldMapMock.Enemies.Count, Is.EqualTo(1));
        }

    }
}