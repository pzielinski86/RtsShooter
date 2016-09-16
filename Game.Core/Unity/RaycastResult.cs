using UnityEngine;

namespace Game.Core.Unity
{
    public class RaycastResult
    {
        private readonly string _tag;
        private readonly float _distance;

        public RaycastResult(string  tag, float distance,Vector3 position)
        {
            Position = position;
            _tag = tag;
            _distance = distance;
        }

        public bool IsNpc
        {
            get { return _tag == "NPC"; }
        }

        public float Distance
        {
            get { return _distance; }
        }

        public Vector3 Position { get; set; }
    }
}