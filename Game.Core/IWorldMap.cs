using Game.Core.Collections;
using Game.Core.Gun;
using Game.Core.Unity;

namespace Game.Core
{
    public interface IWorldMap
    {
        void InitPlayer(ICharacterController characterController, ICamera camera);
        IPlayer Player { get; }
        ObservableCollection<INpc> Enemies { get; }
        BulletsHandler BulletsHandler { get; }
        NpcAttacksManager NpcAttacksManager { get; set; }
        INpc InitNpc(CharacterTransform characterTransform, INpcController npcController);
    }
}