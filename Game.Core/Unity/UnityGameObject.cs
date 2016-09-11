using UnityEngine;

namespace Game.Core.Unity
{
    public class UnityGameObject : IGameObject
    {
        private readonly GameObject _gameObject;

        public UnityGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public void Destroy()
        {
            Object.Destroy(_gameObject);
        }
    }
}