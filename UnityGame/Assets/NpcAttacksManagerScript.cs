using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Adapters;
using Game.Core;
using Game.Core.Unity;

public class NpcAttacksManagerScript : MonoBehaviour
{
    public UnityNpcPrototype[] NpcPrototypes;
    public WaveAttackInfo[] Waves;

    private WorldMap _worldMap;

    // Use this for initialization
	void Start ()
	{
        _worldMap = GameObject.Find("Terrain").GetComponent<WorldMapScript>().WorldMap;

        Vector3[] spawnPlacePositions = GetSpawnPositions().ToArray();

	    _worldMap.NpcAttacksManager = new NpcAttacksManager(_worldMap,Waves, spawnPlacePositions, NpcPrototypes.OfType<INpcPrototype>().ToArray());

        _worldMap.NpcAttacksManager.Start(Time.time);
    }

    // Update is called once per frame
    void Update () {

        _worldMap.NpcAttacksManager.Update(Time.time);
	}

    private IEnumerable<Vector3> GetSpawnPositions()
    {
        return from Transform spawnPlace in transform select spawnPlace.position;
    }
}
