using System.Collections;
using Core.Cards;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;

namespace Core.Deck
{
    public class DeckChanger : MonoBehaviour
    {
        [SerializeField] private Deck _deck;
        [SerializeField] private int _minValue = -2;
        [SerializeField] private int _maxValue = 9;
    
        private IEnumerator _cardsEnumerator;
    
        private void Start()
        {
            _cardsEnumerator = _deck.GetCardsEnumerator();
        }

        private void OnValidate()
        {
            Assert.IsNotNull(_deck);
        }

        public void ChangeNext()
        {
            if (_cardsEnumerator.MoveNext())
            {
                Card card = (Card)_cardsEnumerator.Current;
                IntAttribute attribute = card.GetRandomAttribute();
                attribute.Value = UnityEngine.Random.Range(_minValue, _maxValue + 1);
            }
        }
    }
}