using System;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Initializable[] _initializableObjects;

        private void Awake()
        {
            foreach (var initializable in _initializableObjects)
            {
                try
                {
                    initializable.Target.Initialize(initializable.Data);
                    Debug.Log($"{initializable.Target} initialized");
                }
                catch
                {
                    throw new Exception($"Invalid Initialization {initializable.Data.GetType().Name} parameter for {initializable.Target.name}");
                }
            }
        }
    }
}