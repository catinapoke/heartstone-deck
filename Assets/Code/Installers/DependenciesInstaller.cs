using Cards.Factory;
using Configs;
using Core.Cards;
using Core.Decks;
using Core.Drag;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DependenciesInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private RandomCardConfig _randomCardConfig;
        [SerializeField] private CoreGameConfig _coreGameConfig;

        [Header("Core")] 
        [SerializeField] private CardDragger _cardDragger;
        [SerializeField] private MouseInputProvider _mouseInput;
        [SerializeField] private Deck _deck;
        [SerializeField] private DropPanel _dropPanel;

        [Header("Visual")] 
        [SerializeField] private Material _defaultCardMaterial;
        [SerializeField] private Material _shinyCardMaterial;


        public override void InstallBindings()
        {
            BindConfigs();
            BindCoreDependencies();
            BindVisual();
        }

        private void BindVisual()
        {
            Container.BindInstance(_defaultCardMaterial).WithId("DefaultCard");
            Container.BindInstance(_shinyCardMaterial).WithId("ShinyCard");
        }

        private void BindCoreDependencies()
        {
            Container.BindIFactory<GameObject, Card>().FromFactory<RandomCardFactory>();
            Container.Bind(typeof(IInputProvider), typeof(MonoInputProvider)).FromInstance(_mouseInput);
            Container.BindInstance(_cardDragger);
            Container.BindInstance(_deck);
            Container.BindInstance(_dropPanel);
        }

        private void BindConfigs()
        {
            Container.BindInstance(_randomCardConfig);
            Container.BindInstance(_coreGameConfig);
        }
    }
}