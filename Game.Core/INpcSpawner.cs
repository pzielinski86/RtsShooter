using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public interface INpcSpawner
    {
        Vector3[] SpawnPlacePositions { get; }
        INpcPrototype[] NpcPrototypes { get; }
        INpc CreateRandomNpc();
    }
}