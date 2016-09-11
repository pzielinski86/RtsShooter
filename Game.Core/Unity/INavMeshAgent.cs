using UnityEngine;

namespace Game.Core.Unity
{
    public interface INavMeshAgent
    {
        void SetDestination(Vector3 newDestination);
        float StoppingDistance { get; set; }
        float RemainingDistance { get; }
        bool Enabled { get; set; }
    }

    public class UnityNavMeshAgent : INavMeshAgent
    {
        private readonly NavMeshAgent _navMeshAgent;

        public UnityNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public void SetDestination(Vector3 newDestination)
        {            
            _navMeshAgent.destination = newDestination;
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