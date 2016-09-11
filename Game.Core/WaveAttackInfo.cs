using System;

namespace Game.Core
{
    [Serializable]
    public class WaveAttackInfo
    {
        public uint TotalEnemies;
        public float SpawnDelayInSecs;
        public uint WarmupTimeInSecs;
    }
}