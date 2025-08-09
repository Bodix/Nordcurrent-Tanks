using UnityEngine;
using Zenject;

namespace Game
{
    public static class DiContainerExtensions
    {
        public static T InstantiateUnityObject<T>(this DiContainer _container, T obj) where T : Object
        {
            T objClone = Object.Instantiate(obj);

            _container.Inject(objClone);

            return objClone;
        }
    }
}