using System;
using Game.Core;
using Game.Core.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Adapters
{
    [Serializable]
    public class UnityNpcPrototype : INpcPrototype
    {
        public GameObject Prefab;
          
        public Npc Create()
        {
            var gameObject = Object.Instantiate(Prefab);

            var npc = gameObject.GetComponent<NpcScript>().Npc;

            return npc;
        }
    }
}