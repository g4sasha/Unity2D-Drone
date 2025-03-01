using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MonoInstaller[] _installers;
        [SerializeField] private bool _initLogs;

        private void Awake()
        {
            foreach (var installer in _installers)
            {
                installer.Init();

                if (_initLogs)
                {
                    Debug.Log($"{installer.name} initialized");
                }
            }
        }
    }
}