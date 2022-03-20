using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Core.Cards;
using Extensions;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;
using Zenject;

namespace Core.Decks
{
    [RequireComponent(typeof(CircleCardPositionsProvider))]
    public class Deck : CardsContainerView
    {
        [Header("Spawn settings")]
        [SerializeField] private Card _cardPrefab;

        [Header("Arrangement settings")]
        [SerializeField] private float _widthUnits = 1.25f;
        [SerializeField] private float _heightUnits = 0.5f;
        [SerializeField] private float _unitsPerRect = 18;

        [Inject] private IFactory<GameObject, Card> _cardFactory;
        [Inject] private CoreGameConfig _gameConfig;
        
        private CircleCardPositionsProvider _positionsProvider;

        protected override void Awake()
        {
            base.Awake();
            _positionsProvider = GetComponent<CircleCardPositionsProvider>();
            
            int count = UnityEngine.Random.Range(_gameConfig.MinCardSpawnCount, _gameConfig.MaxCardSpawnCount + 1);
            for (int i = 0; i < count; i++)
            {
                Card card = _cardFactory.Create(_cardPrefab.gameObject);
                card.transform.SetParent(transform);
                card.name = $"Card {i}";
                _cards.Add(card);
            }
        }

        private void Start()
        {
            foreach (Card card in _cards)
            {
                card.transform.localPosition = Vector3.zero;
            }
            
            ArrangeCards();
        }

        private void OnValidate()
        {
            Assert.IsNotNull(_cardPrefab);

            int count = 5;
            GetOffsets(count, out var horizontalOffset, out var verticalOffset);

            _positionsProvider = GetComponent<CircleCardPositionsProvider>();
            _positionsProvider.Init(horizontalOffset, verticalOffset, Vector3.down * (verticalOffset / 2));
        }

        public IEnumerator GetCardsEnumerator()
        {
            return new CollectionsExtensions.CircularListEnumerator<Card>(_cards);
        }

        public void ReturnCard(Card card)
        {
            int index  = _cards.FindIndex((x) => x == card);
            MoveCardToPosition(index);
        }

        protected override void ArrangeCards()
        {
            UpdateOffsets();
            for (int i = 0; i < _cards.Count; i++)
            {
                MoveCardToPosition(i);
            }
        }

        private void MoveCardToPosition(int i)
        {
            CardPosition data = _positionsProvider.GetPosition(i, _cards.Count);
            _cards[i].Animator.LocalMoveTo(data);
        }
        
        private void GetOffsets(int count, out float horizontalOffset, out float verticalOffset)
        {
            Rect rect = GetComponent<RectTransform>().rect;
            float unitWidth = rect.width / _unitsPerRect;
            float unitHeight = rect.height / _unitsPerRect;
            
            Debug.Log($"{rect.width} / {_unitsPerRect} = {unitWidth}");
            Debug.Log($"{rect.height} / {_unitsPerRect} = {unitHeight}");
            Debug.Log($"W: {Screen.width}; H: {Screen.height}");

            horizontalOffset = unitWidth * _widthUnits * count;
            verticalOffset = unitHeight * _heightUnits * count;
        }
        
        private void UpdateOffsets()
        {
            GetOffsets(_cards.Count, out var horizontalOffset, out var verticalOffset);
            _positionsProvider.Init(horizontalOffset, verticalOffset, Vector3.down * (verticalOffset / 2));
        }
    }
}