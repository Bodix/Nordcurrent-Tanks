using UnityEngine;
using Zenject;

namespace Game.Extensions
{
    public static class DiContainerExtensions
    {
        public static T InstantiateUnityObject<T>(this DiContainer container, T obj) where T : Object
        {
            T objClone = Object.Instantiate(obj);

            container.Inject(objClone);

            return objClone;
        }
    }
}