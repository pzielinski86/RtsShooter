using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Unity;

public class WorldMapScript : MonoBehaviour
{

    private WorldMap _worldMap;
    public WorldMap WorldMap
    {
        get { return _worldMap ?? (_worldMap = new WorldMap(new UnityInput(),new UnityPhysics())); }
    }
	void Start () {
	
     }

    // Update is called once per frame
    void Update () {
	
	}

}
