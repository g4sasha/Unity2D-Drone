using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class Initializable
    {
        [field: SerializeField] public MonoInitializable Target { get; set; }
        [field: SerializeField] public Object Data { get; set; }

        public Initializable(MonoInitializable target, Object data)
        {
            Target = target;
            Data = data;
        }
    }

    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Initializable[] _initializableObjects;

        private void Awake()
        {
            foreach (var initializable in _initializableObjects)
            {
                initializable.Target.Initialize(initializable.Data);
            }
        }
    }
}