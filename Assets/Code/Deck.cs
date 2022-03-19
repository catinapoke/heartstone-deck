using System;
using Cards;
using ModestTree;
using UnityEngine;
using Utils;
using Zenject;

[RequireComponent(typeof(CircleCardPositionsProvider))]
public class Deck : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] private int _minCardCount;
    [SerializeField] private int _maxCardCount;
    [SerializeField] private Card _cardPrefab;

    [Header("Arrangement settings")]
    [SerializeField] private float _verticalOffset = 1;
    [SerializeField] private float _horizontalOffset = 1;
    
    [Inject] private IFactory<GameObject, Card> _cardFactory;
    private Card[] _cards;
    private CircleCardPositionsProvider _positionsProvider;
    
    private void Awake()
    {
        _positionsProvider = GetComponent<CircleCardPositionsProvider>();
        _positionsProvider.Init(_horizontalOffset, _verticalOffset);
        
        _cards = new Card[UnityEngine.Random.Range(_minCardCount, _maxCardCount + 1)];
        for (int i = 0; i < _cards.Length; i++)
        {
            _cards[i] = _cardFactory.Create(_cardPrefab.gameObject);
            _cards[i].transform.SetParent(transform);
        }

        ArrangeCards();
    }

    public void OnValidate()
    {
        Assert.IsNotNull(_cardPrefab);
        
        _positionsProvider = GetComponent<CircleCardPositionsProvider>();
        _positionsProvider.Init(_horizontalOffset, _verticalOffset);
    }

    private void ArrangeCards()
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            CardPosition data = _positionsProvider.GetPosition(i, _cards.Length);
            _cards[i].transform.position = data.Position;
            _cards[i].transform.rotation = Quaternion.Euler(data.Rotation);
        }
    }
}