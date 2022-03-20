using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.Cards
{
    public abstract class CardsContainerView : MonoBehaviour
    {
        [SerializeField] protected int _initialCapacity;

        protected List<Card> _cards;

        protected virtual void Awake()
        {
            _cards = new List<Card>(_initialCapacity);
        }

        protected virtual void OnEnable()
        {
            foreach (var card in _cards)
            {
                card.OnDeath += RemoveCard;
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var card in _cards)
            {
                card.OnDeath -= RemoveCard;
            }
        }

        public bool Contains(Card card)
        {
            return _cards.Contains(card);
        }

        public void Add(Card card)
        {
            card.transform.SetParent(transform);
            card.OnDeath += RemoveCard;
            
            _cards.Add(card);
            ArrangeCards();
        }

        public void Remove(Card card)
        {
            _cards.Remove(card);
            ArrangeCards();
        }

        protected abstract void ArrangeCards();
        
        private async void RemoveCard(Card card)
        {
            await Task.Yield();
            await card.Animator.GetAttributeAnimation(AttributeType.Health).AsyncWaitForCompletion();
            await card.Animator.Disappear().AsyncWaitForCompletion();
            Remove(card);
            Destroy(card.gameObject);
        }
    }
}