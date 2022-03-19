using Cards;
using Cards.Factory;
using Configs;
using Core.Cards;
using Core.Drag;
using UnityEngine;
using Utils;
using Utils.SpriteProvider;
using Zenject;

public class DependenciesInstaller : MonoInstaller
{
    [Header("Configs")]
    [SerializeField] private RandomCardConfig _randomCardConfig;
    [SerializeField] private CoreGameConfig _coreGameConfig;

    [Header("Core")] 
    [SerializeField] private CardDragger _cardDragger;
    [SerializeField] private MouseInputProvider _mouseInput;
    
    [Header("Containers")]
    [SerializeField] private SpriteContainer _spriteContainer;
    
    
    public override void InstallBindings()
    {
        BindImageProvider();
        BindConfigs();
        BindCoreDependencies();
    }

    private void BindCoreDependencies()
    {
        Container.BindIFactory<GameObject, Card>().FromFactory<RandomCardFactory>();
        Container.Bind(typeof(IInputProvider), typeof(MonoInputProvider)).FromInstance(_mouseInput);
        Container.Bind<CardDragger>().FromInstance(_cardDragger);
    }

    private void BindImageProvider()
    {
        Container.BindInstance(_spriteContainer);
        Container.BindInterfacesAndSelfTo<RandomImageProvider>().AsCached();
    }

    private void BindConfigs()
    {
        Container.BindInstance(_randomCardConfig);
        Container.BindInstance(_coreGameConfig);
    }
}