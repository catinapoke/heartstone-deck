using System;
using System.Collections.Generic;
using Configs;
using Core.Cards;
using Extensions;
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
        [Inject] private CoreGameConfig _coreGameConfig;
        
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

        private void Update()
        {
            if(currentCard == null) return;
            
            // move card
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
                Card card = GetClosestCard();
                if(card == null) return;
                
                // Check state
                currentCard = card;
                Debug.Log($"Picked card: {card.name}");
                // Change state and start movement
            }
            
            Card GetClosestCard()
            {
                int CardDisanceComparison(GameObject x, GameObject y) => x.transform.position.z.CompareTo(y.transform.position.z);
                
                Card result = null;
                foreach (var hit in hits)
                {
                    Card temp = hit.gameObject.GetComponent<Card>();
                    if (temp != null)
                    {
                        if (result == null)
                        {
                            result = temp;
                            Debug.Log($"init depth = {hit.depth}");
                        }


                        if (CardDisanceComparison(result.gameObject, temp.gameObject) == -1)
                        {
                            result = temp;
                            Debug.Log($"new depth = {hit.depth}");
                        }
                            
                    }
                }

                return result;
            }
        }

        private void OnTouchFinish()
        {
            if (currentCard == null) return;
            // check drop panel

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
        }

        private void PlaceAtPanel(Card card, DropPanel panel)
        {
            Debug.Log($"Hit drop panel! - {panel.name}");
        }
    }
}