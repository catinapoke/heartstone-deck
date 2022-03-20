using UnityEngine;
using Utils.SpriteProvider;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "PreloadInstaller", menuName = "Zenject/PreloadInstaller")]
    public class PreloadInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private SpriteContainer _spriteContainer;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_spriteContainer);
            Container.Bind<RandomImageProvider>().AsCached();
            Container.Bind<LoremPicsumImageRequester>().AsCached();
            Container.BindInterfacesAndSelfTo<LoremPicsumPreloadedProvider>().AsCached();
        }
    }
}