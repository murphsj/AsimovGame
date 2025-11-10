using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace Services
{
    /// <summary>
    /// Used to register and access services (singletons basically.)
    /// Roughly based on this tutorial: https://www.youtube.com/watch?v=537cG3KICu0
    /// </summary>
    public static class ServiceLocator
    {
        public static Dictionary<Type, object> services;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            services = new Dictionary<Type, object>();
            foreach (Type service in GetAllAutoRegisterServices())
            {
                if (IsRegistered(service)) continue;

                if (service.IsSubclassOf(typeof(MonoBehaviour)))
                {
                    FindOrCreateMonoBehaviorService(service);
                }
                else
                {
                    RegisterNewInstance(service);
                }
            }
        }

        public static bool IsRegistered(Type serviceType)
        {
            return services.ContainsKey(serviceType);
        }

        public static bool IsRegistered<TService>()
        {
            return IsRegistered(typeof(TService));
        }

        public static TService Get<TService>()
        {
            Type serviceType = typeof(TService);
            if (!IsRegistered<TService>())
            {
                throw new InvalidOperationException(
                    "Services Get: Service " + serviceType.Name + " has not been initialized"
                );
            }

            return (TService)services[serviceType];
        }

        public static void Register<TService>(TService instance)
        {
            Type serviceType = typeof(TService);
            if (IsRegistered<TService>())
            {
                throw new InvalidOperationException(
                    "Services Register: tried to register an already registered service: " + serviceType.Name
                );
            }

            services[serviceType] = instance;
        }

        private static void RegisterForced(Type type, object instance)
        {
            if (IsRegistered(type))
            {
                throw new InvalidOperationException(
                    "Services Register: tried to register an already registered service: " + type.Name
                );
            }

            services[type] = instance;
        }

        private static IEnumerable<Type> GetAllAutoRegisterServices()
        {
            // Get every C# object with the RegisterService attribute
            // (this is weird Reflections stuff I had to look it up don't worry if you're confused)
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes().Where(
                    t => Attribute.GetCustomAttribute(t, typeof(RegisterService)) != null
                ));
        }

        private static void RegisterNewInstance(Type serviceType)
        {
            Register(serviceType.Instantiate());
        }

        private static void FindOrCreateMonoBehaviorService(Type serviceType)
        {
            var inGameService = UnityEngine.Object.FindAnyObjectByType(serviceType);

            if (inGameService == null)
            {
                GameObject newObject = new GameObject();
                inGameService = newObject.AddComponent(serviceType);
                newObject.name = serviceType.Name;
            }

            // inGameService is now a Component or UnityEngine.Object
            // so we need to change its type
            RegisterForced(serviceType, inGameService);
        }
    }
}

