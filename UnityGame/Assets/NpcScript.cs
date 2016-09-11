using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Game.Core;
using Game.Core.Gun;
using Game.Core.Unity;

public class NpcScript : MonoBehaviour,IHittable
{
    public Transform GunBarrel;
    public INpc Npc { get; private set; }
    private WorldMap _worldMap;
    // Use this for initialization

   
	void Awake ()
	{        
	    _worldMap = GameObject.Find("Terrain").GetComponent<WorldMapScript>().WorldMap;
        var animator = GetComponent<Animator>();

        var characterAnimationController = new CharacterAnimationController(animator);

        var characterTransform = new CharacterTransform(new UnityTransform(transform), new UnityTransform(GunBarrel));

        var npcController = new NpcController(new UnityNavMeshAgent(GetComponent<NavMeshAgent>()),
	        new UnityGameObject(this.gameObject), characterAnimationController);

	    Npc = _worldMap.InitNpc(characterTransform, npcController);
	}

    // Update is called once per frame
	void Update ()
	{
	    Npc.Update(_worldMap);
	}

    public void Hit(BulletBase bullet)
    {
        Npc.Hit(bullet);
    }
}
