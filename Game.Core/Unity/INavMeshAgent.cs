using UnityEngine;

namespace Game.Core.Unity
{
    public interface INavMeshAgent
    {
        void    SetDestination(Vector3 newDestination);
        float StoppingDistance { get; set; }
        float RemainingDistance { get; }
        bool Enabled { get; set; }
        void Stop();
    }

    public class UnityNavMeshAgent : INavMeshAgent
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly NavMeshObstacle _navMeshObstacle;

        public UnityNavMeshAgent(NavMeshAgent navMeshAgent,NavMeshObstacle navMeshObstacle)
        {
            _navMeshAgent = navMeshAgent;
            _navMeshObstacle = navMeshObstacle;
        }

        public void SetDestination(Vector3 newDestination)
        {
            _navMeshObstacle.enabled = false;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(newDestination);
        }

        public void Stop()
        {
            _navMeshAgent.enabled = false;
            _navMeshObstacle.enabled = true;            
        }

        public float StoppingDistance
        {
            get { return _navMeshAgent.stoppingDistance; }
            set { _navMeshAgent.stoppingDistance = value; }
        }

        public float RemainingDistance => _navMeshAgent.remainingDistance;

        public bool Enabled
        {
            get { return _navMeshAgent.enabled; }
            set { _navMeshAgent.enabled = value; }
        }
    }
}