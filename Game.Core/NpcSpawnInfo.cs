using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public class NpcSpawnInfo
    {
        public ITransform Transform { get; set; } 
        public ITransform BarrelTransform { get; set; }
    }
}