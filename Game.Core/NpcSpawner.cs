using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public class NpcSpawner : INpcSpawner
    {
        private readonly IRandom _random;
        public Vector3[] SpawnPlacePositions { get;  }
        public INpcPrototype[] NpcPrototypes { get;  }

        public NpcSpawner(IRandom random,Vector3[] spawnPlacePositions, INpcPrototype[] npcPrototypes)
        {
            _random = random;
            SpawnPlacePositions = spawnPlacePositions;
            NpcPrototypes = npcPrototypes;
        }

        public INpc CreateRandomNpc()
        {
            var spawnPlaceIndex = _random.Range(0, SpawnPlacePositions.Length);
            var npcPrototypeIndex = _random.Range(0, NpcPrototypes.Length);

            var npc = NpcPrototypes[npcPrototypeIndex].Create();
            npc.SetPosition(SpawnPlacePositions[spawnPlaceIndex]);

            return npc;
        }
    }
}