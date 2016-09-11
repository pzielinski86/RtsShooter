using System.Collections.Generic;
using System.Linq;
using Game.Core.Properties;
using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public class NpcAttacksManager
    {
        private readonly IWorldMap _worldMap;
        private readonly INpcSpawner _npcSpawner;
        private float _lastSpawnTime;
        private float _attackStartTimeInSecs = float.MinValue;
        private uint _numberOfNpcCreatedInCurrentTurn = 0;
        private int _currentWaveIndex;
        public WaveAttackInfo[] Waves { get; }
        public WaveAttackInfo CurrentWaveAttackInfo => Waves[_currentWaveIndex];
        public AttackPhase Phase { get; private set; }
        public NpcAttacksManager(IWorldMap worldMap, INpcSpawner npcSpawner,WaveAttackInfo[] waves) 
        {
            _worldMap = worldMap;
            _npcSpawner = npcSpawner;
            Waves = waves;
        }

        public void Start(float currentTimeInSecs)
        {
            _attackStartTimeInSecs = currentTimeInSecs;
        }

        public void Update(float currentTimeInSecs)
        {
            if (Phase == AttackPhase.Finished)
                return;

            if (currentTimeInSecs - _attackStartTimeInSecs < CurrentWaveAttackInfo.WarmupTimeInSecs)
                Phase = AttackPhase.Preparing;
            else
            {
                Phase = AttackPhase.Attacking;

                if (_numberOfNpcCreatedInCurrentTurn < CurrentWaveAttackInfo.TotalEnemies)
                {
                    if(_numberOfNpcCreatedInCurrentTurn==0)
                        _worldMap.Enemies.Clear();

                    TryToCreateNewNpc(currentTimeInSecs);
                }
                else
                    StartNewWave(currentTimeInSecs);
            }
        }

        private void TryToCreateNewNpc(float currentTimeInSecs)
        {
            var timeSinceLastSpawn = currentTimeInSecs - _lastSpawnTime;

            if (timeSinceLastSpawn >= CurrentWaveAttackInfo.SpawnDelayInSecs)
            {
                CreateRandomNpc();
                _lastSpawnTime = currentTimeInSecs;
            }
        }

        private void StartNewWave(float currentTimeInSecs)
        {
            // TODO: Optimize
            if (_worldMap.Enemies.All(x => x.State == CharacterState.Killed))
            {
                _currentWaveIndex++;
                if (_currentWaveIndex == Waves.Length)
                    Phase = AttackPhase.Finished;
                else
                {                    
                    _numberOfNpcCreatedInCurrentTurn = 0;
                    _attackStartTimeInSecs = currentTimeInSecs;
                }
            }
        }

        public void CreateRandomNpc()
        {            
            _npcSpawner.CreateRandomNpc();
            _numberOfNpcCreatedInCurrentTurn++;
        }
    }
}