using Cards;
using Cards.Factory;
using UnityEngine;
using Utils;
using Utils.SpriteProvider;
using Zenject;

public class DependenciesInstaller : MonoInstaller
{
    [SerializeField] private SpriteContainer _spriteContainer;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_spriteContainer);
        Container.BindInterfacesAndSelfTo<RandomImageProvider>().AsCached();
        Container.BindIFactory<GameObject, Card>().FromFactory<RandomCardFactory>();
    }
}