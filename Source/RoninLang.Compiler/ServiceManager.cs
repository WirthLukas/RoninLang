
using System;
using System.Collections.Generic;

#nullable enable

namespace RoninLang.Compiler
{
    public class ServiceManager
    {
        #region <<Singleton>>
        
        private static ServiceManager? _instance = null;
        public static ServiceManager Instance => _instance ??= new ServiceManager();
        private ServiceManager() { }
        
        #endregion
        
        private readonly Dictionary<string, object> _services = new Dictionary<string, object>();

        public void AddService<T>(T service) where T : class
        {
            var key = typeof(T).Name;

            if (_services.ContainsKey(key))
                throw new Exception($"Already using System with name {key}");
            
            _services.Add(key, service ?? throw new ArgumentNullException(nameof(service)));
        }

        public void Reset<T>(T newService) where T : class
        {
            var key = typeof(T).Name;
            
            if (!_services.ContainsKey(key))
                AddService<T>(newService);

            _services[key] = newService;
        }

        public T GetService<T>() where T : class
        {
            var key = typeof(T).Name;
            return GetService<T>(key);
        }
        
        public T GetService<T>(string subTypeName) where T : class
        {
            if (subTypeName == null) throw new ArgumentNullException(nameof(subTypeName));
            
            if (!_services.ContainsKey(subTypeName))
                throw new Exception("No service registered for that type");

            return _services[subTypeName] as T ?? throw new Exception($"Cannot cast service {subTypeName} to {typeof(T).Name}");
        }

        public T GetService<T>(Type subType) where T : class => GetService<T>(subType.Name);
    }
}