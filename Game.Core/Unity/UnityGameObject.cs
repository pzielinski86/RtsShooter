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

        public IGameObject Create()
        {
            var newObject = Object.Instantiate(_gameObject);

            return new UnityGameObject(newObject);
        }

        private ITransform _transform;
        public ITransform Transform
        {
            get
            {
                if (_transform == null)
                {
                    _transform=new UnityTransform(_gameObject.transform);

                    return _transform;
                }

                return _transform;
            }
        }

        public Bounds Bounds => _gameObject.GetComponent<Renderer>().bounds;
    }
}