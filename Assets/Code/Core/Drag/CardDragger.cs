using System.Collections.Generic;
using Configs;
using Core.Cards;
using Core.Decks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Core.Drag
{
    public class CardDragger : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster _raycaster;
        
        [Inject] private MonoInputProvider _inputProvider;
        [Inject] private Deck _deck;

        private Card currentCard;
        private List<RaycastResult> hits;

        private void Awake()
        {
            hits = new List<RaycastResult>(16);
        }

        private void OnEnable()
        {
            _inputProvider.OnTouchStart += OnTouchStart;
            _inputProvider.OnTouchFinish += OnTouchFinish;
        }

        private void OnDisable()
        {
            _inputProvider.OnTouchStart -= OnTouchStart;
            _inputProvider.OnTouchFinish -= OnTouchFinish;
        }

        private void OnDrawGizmos()
        {
            if(_inputProvider == null) return;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_inputProvider.GetPosition(), Vector3.one * 3); 
        }
        
        private void OnTouchStart()
        {
            if (Raycast(_inputProvider.GetPosition(), hits))
            {
                Card card = GetCard();
                if(card == null || !card.Animator.IsComplete || !_deck.Contains(card)) return;
                
                // Check state
                currentCard = card;
                Debug.Log($"Picked card: {card.name}");
                currentCard.Animator.FollowPointer(_inputProvider);
                currentCard.Animator.Glow(true);
            }
            
            Card GetCard()
            {
                foreach (var hit in hits)
                {
                    Card result = hit.gameObject.GetComponent<Card>();
                    if (result != null)
                    {
                        return result;
                    }
                }

                return null;
            }
        }

        private void OnTouchFinish()
        {
            if (currentCard == null) return;
            // check drop panel
            Debug.Log($"Finish touch with: {currentCard.name}");
            currentCard.Animator.Glow(false);
            if (Raycast(_inputProvider.GetPosition(), hits) && HitDropPanel(out DropPanel panel))
            {
                PlaceAtPanel(currentCard, panel);
            }
            else
            {
                ReturnToDeck(currentCard);
            }

            currentCard = null;
            
            bool HitDropPanel(out DropPanel panel)
            {
                panel = null;
                foreach (var hit in hits)
                {
                    panel = hit.gameObject.GetComponent<DropPanel>();
                    if (panel != null) return true;
                }

                return false;
            }
        }

        private bool Raycast(Vector2 position, List<RaycastResult> results)
        {
            results.Clear();
            
            EventSystem system = EventSystem.current;
            PointerEventData data = new PointerEventData(system);
            data.position = _inputProvider.GetPosition();

            _raycaster.Raycast(data, results);
            return results.Count > 0;
        }
        
        private void ReturnToDeck(Card card)
        {
            Debug.Log("Haven't hit panel");
            _deck.ReturnCard(card);
        }

        private void PlaceAtPanel(Card card, DropPanel panel)
        {
            Debug.Log($"Place at panel {panel.name}");
            panel.Add(card);
            _deck.Remove(card);
        }
    }
}