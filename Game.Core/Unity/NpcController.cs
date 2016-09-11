namespace Game.Core.Unity
{
    public class NpcController : INpcController
    {
        public NpcController(INavMeshAgent navMeshAgent,IGameObject gameObject,ICharacterAnimationController animation)
        {
            Astar = navMeshAgent;
            GameObject = gameObject;
            Animation = animation;
        }

        public INavMeshAgent Astar { get; private set; }
        public IGameObject GameObject { get; }
        public ICharacterAnimationController Animation { get; }
    }
}