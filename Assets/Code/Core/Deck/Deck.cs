using System.Collections;
using System.Collections.Generic;
using Configs;
using Core.Cards;
using Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;
using Zenject;

namespace Core.Deck
{
    [RequireComponent(typeof(CircleCardPositionsProvider))]
    public class Deck : MonoBehaviour
    {
        [Header("Spawn settings")]
        [SerializeField] private Card _cardPrefab;

        [Header("Arrangement settings")]
        [SerializeField] private float _widthPerCard = 10f;
        [SerializeField] private float _heightPerCard = 1.25f;

        [Inject] private IFactory<GameObject, Card> _cardFactory;
        [Inject] private CoreGameConfig _gameConfig;

        private List<Card> _cards;
        private CircleCardPositionsProvider _positionsProvider;

        private void Awake()
        {
            _positionsProvider = GetComponent<CircleCardPositionsProvider>();

            int count = UnityEngine.Random.Range(_gameConfig.MinCardSpawnCount, _gameConfig.MaxCardSpawnCount + 1);
            _cards = new List<Card>(count);
            for (int i = 0; i < count; i++)
            {
                Card card = _cardFactory.Create(_cardPrefab.gameObject);
                card.transform.SetParent(transform);
                card.name = $"Card {i}";
                UpdateZIndex(card.transform, i);
                _cards.Add(card);
            }

            void UpdateZIndex(Transform cardTransform, int index)
            {
                Vector3 pos = cardTransform.localPosition;
                pos.z = index;
                cardTransform.localPosition = pos;
            }
        }

        private void Start()
        {
            float horizontalOffset = _widthPerCard * _cards.Count;
            float verticalOffset = _heightPerCard * _cards.Count;

            _positionsProvider.Init(horizontalOffset, verticalOffset, Vector3.down * (verticalOffset / 2));
            ArrangeCards();
        }

        private void OnValidate()
        {
            Assert.IsNotNull(_cardPrefab);

            int count = 5;
            float horizontalOffset = _widthPerCard * count;
            float verticalOffset = _heightPerCard * count;

            _positionsProvider = GetComponent<CircleCardPositionsProvider>();
            _positionsProvider.Init(horizontalOffset, verticalOffset, Vector3.down * (verticalOffset / 2));
        }

        public IEnumerator GetCardsEnumerator()
        {
            return new CollectionsExtensions.CircularListEnumerator<Card>(_cards);
        }

        private void ArrangeCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                CardPosition data = _positionsProvider.GetPosition(i, _cards.Count);
                _cards[i].transform.position = data.Position;
                _cards[i].transform.rotation = Quaternion.Euler(data.Rotation);
            }
        }
    }
}