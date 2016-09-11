using UnityEngine;
using System.Collections;
using System.Linq;
using Game.Core;
using Game.Core.Properties;

public class WaveAttackInfoScript : MonoBehaviour {
    private WorldMap _worldMap;
    private Rect _rect;

    // Use this for initialization
	void Start () {

        _worldMap = GameObject.Find("Terrain").GetComponent<WorldMapScript>().WorldMap;
        _rect=new Rect(Screen.width/2,10,300,200);

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnGUI()
    {
        if (_worldMap.NpcAttacksManager.Phase == AttackPhase.Attacking)
        {
            // TODO: Optimize
            int killedEnemies = _worldMap.Enemies.Count(x => x.State == CharacterState.Killed);
            uint allEnemies = _worldMap.NpcAttacksManager.CurrentWaveAttackInfo.TotalEnemies;

            GUI.Label(_rect, string.Format("{0}/{1}", killedEnemies, allEnemies));
        }
        else if (_worldMap.NpcAttacksManager.Phase == AttackPhase.Preparing)
            GUI.Label(_rect, "Preparing to the attack...");
        else
            GUI.Label(_rect, "Game over");

    }
}
