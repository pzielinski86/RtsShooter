namespace Game.Core.Unity
{
    public interface INpcController
    {
        INavMeshAgent Astar { get; }
        IGameObject GameObject { get; }
        ICharacterAnimationController Animation { get; }
    }
}