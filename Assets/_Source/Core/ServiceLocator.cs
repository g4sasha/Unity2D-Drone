using System;
using System.Collections.Generic;

namespace Core
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance => _instance;
        private static readonly ServiceLocator _instance = new();
        private readonly Dictionary<Type, IService> _services = new();

        public void Register<T>(T service) where T : IService
        {
            var key = typeof(T);

            if (_services.ContainsKey(key))
            {
                throw new Exception($"Service {nameof(key)} is already registered");
            }

            _services.Add(key, service);
        }

        public void Unregister<T>() where T : IService
        {
            var key = typeof(T);

            if (!_services.ContainsKey(key))
            {
                throw new Exception($"Service {key} is not registered");
            }

            _services.Remove(key);
        }

        public T Get<T>() where T : IService
        {
            var key = typeof(T);

            if (!_services.ContainsKey(key))
            {
                throw new Exception($"Service {key} is not registered");
            }

            return (T)_services[key];
        }
    }
}