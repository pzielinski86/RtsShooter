using System.Collections.Generic;
using System.Linq;
using Game.Core.Properties;
using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{

    public enum AttackPhase { Preparing, Attacking, Finished }
    public class NpcAttacksManager
    {
        private readonly WorldMap _worldMap;
        private readonly Vector3[] _spawnPlacePositions;
        private readonly INpcPrototype[] _npcPrototypes;
        private float _lastSpawnTime;
        private float _attackStartTimeInSecs = float.MinValue;
        private uint _numberOfNpcCreatedInCurrentTurn = 0;
        private int _currentWaveIndex;
        public WaveAttackInfo[] Waves { get; }
        public WaveAttackInfo CurrentWaveAttackInfo => Waves[_currentWaveIndex];
        public AttackPhase Phase { get; private set; }
        public NpcAttacksManager(WorldMap worldMap, WaveAttackInfo[] waves, Vector3[] spawnPlacePositions, INpcPrototype[] npcPrototypes)
        {
            _worldMap = worldMap;
            _spawnPlacePositions = spawnPlacePositions;
            _npcPrototypes = npcPrototypes;
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
            var spawnPlaceIndex = Random.Range(0, _spawnPlacePositions.Length);
            var npcPrototypeIndex = Random.Range(0, _npcPrototypes.Length);

            Npc npc = _npcPrototypes[npcPrototypeIndex].Create();
            npc.SetPosition(_spawnPlacePositions[spawnPlaceIndex]);
            _numberOfNpcCreatedInCurrentTurn++;
        }
    }
}