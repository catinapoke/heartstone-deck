using Configs;
using Core.Cards;
using UnityEngine;
using Utils.SpriteProvider;
using Zenject;

namespace Cards.Factory
{
    public class RandomCardFactory : IFactory<GameObject, Card>
    {
        [Inject] private ISpriteProvider _spriteProvider;
        [Inject] private RandomCardConfig _config;

        private string[] names = new[]
        {
            "Hunter",
            "Druid",
            "Warlock",
            "Teapot",
            "Red rift",
            "Onyx",
            "Sample"
        };
        
        private string[] descriptions = new[]
        {
            "<b>Discover</b> a card from your deck.",
            "Summon three 2/2 Treants",
            "<b>Battlecry:</b> Equip a 3/8 Blood Fury.",
            "Draw 2 cards",
            "Hires me, lol",
            "<b>Combo:</b> Gain +3 Attack",
            "Use your imagination"
        };
        
        public Card Create(GameObject prefab)
        {
            GameObject instance = Object.Instantiate(prefab);
            Card card = instance.GetComponent<Card>();
            
            if(card == null)
                card = instance.AddComponent<Card>();
            
            card.Init(GenerateData());

            return card;
        }

        private CardData GenerateData()
        {
            int index = UnityEngine.Random.Range(0, names.Length);
            return new CardData(
                names[index],
                descriptions[index],
                _spriteProvider.GetSprite(),
                RandomAttributeValue(),
                RandomAttributeValue(),
                RandomAttributeValue()
            );

            int RandomAttributeValue() => UnityEngine.Random.Range(_config.LowBorder, _config.HighBorder + 1);
        }
    }
}