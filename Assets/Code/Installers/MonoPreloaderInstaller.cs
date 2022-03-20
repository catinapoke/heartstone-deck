using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    public class MonoPreloaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            CoroutineLoader coroutineLoader = CreateComponentObject<CoroutineLoader>();
            Container.BindInstance(coroutineLoader);
        }

        private T CreateComponentObject<T>() where T : Component
        {
            GameObject holder = new GameObject();
            T component = holder.AddComponent<T>();
            return component;
        }
    }
}