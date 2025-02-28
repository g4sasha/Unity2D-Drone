using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MonoInitializable[] _initializables;

        private void Awake()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Init();
                Debug.Log($"{initializable.name} initialized");
            }
        }
    }
}