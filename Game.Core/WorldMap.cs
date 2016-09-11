using System;
using System.Collections.Generic;
using Game.Core.Collections;
using Game.Core.Gun;
using Game.Core.Unity;
using UnityEngine;

namespace Game.Core
{
    public class WorldMap
    {
        private readonly IInput _input;
        private readonly IPhysics _physics;

        public WorldMap(IInput input, IPhysics physics)
        {
            _input = input;
            _physics = physics;
            BulletsHandler = new BulletsHandler();
            Enemies = new ObservableCollection<Npc>();
            Enemies.ItemRemoved += Enemies_ItemRemoved;
        }

        private void Enemies_ItemRemoved(object sender, ObservableCollectionEventArgs<Npc> e)
        {
            e.Item.Destroy();
        }

        public void InitPlayer(ICharacterController characterController, ICamera camera)
        {
            Player = new Player(BulletsHandler, camera, _physics, characterController);
        }

        public Player Player { get; private set; }

        public ObservableCollection<Npc> Enemies { get; private set; }
        public BulletsHandler BulletsHandler { get; private set; }
        public NpcAttacksManager NpcAttacksManager { get; set; }

        public Npc InitNpc(CharacterTransform characterTransform, INpcController npcController)
        {
            var npc = new Npc(BulletsHandler, characterTransform, npcController, _physics);
            Enemies.Add(npc);

            return npc;
        }
    }
}