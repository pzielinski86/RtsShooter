using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Game.Core;
using Game.Core.Gun;
using Game.Core.Unity;
using UnityEditor;
using UnityEngine.Networking;
using PlayerController = Game.Core.PlayerController;

public class PlayerScript : MonoBehaviour, IHittable
{
    public IPlayer Player { get; private set; }
    public PlayerController PlayerController { get; private set; }
    public Transform GunBarrel;
    public GameObject WorldMapComponent;


    void Start()
    {
        var worldMapScript = WorldMapComponent.GetComponent<WorldMapScript>();
        InitPlayer(worldMapScript.WorldMap);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Update();
    }

    void OnAnimatorIK(int layerIndex)
    {
        Player.UpdateIK();
    }

    private void InitPlayer(WorldMap worldMap)
    {
        var animator = GetComponent<Animator>();
        var characterAnimationController = new CharacterAnimationController(animator);
        var characterTransform = new CharacterTransform(new UnityTransform(transform), new UnityTransform(GunBarrel));

        var characterController = new UnityCharacterController(characterAnimationController,
            GetComponent<CharacterController>(),
            characterTransform);
        
        worldMap.InitPlayer(characterController,new UnityCamera());

        Player = worldMap.Player;
        PlayerController = new PlayerController(Player, new UnityInput());
    }

    public void Hit(BulletBase bullet)
    {
        Player.Hit(bullet);
    }
}
