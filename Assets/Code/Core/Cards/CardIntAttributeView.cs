using System;
using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;
using Zenject;

namespace Core.Cards
{
    [RequireComponent(typeof(TMP_Text))]
    public class CardIntAttributeView : MonoBehaviour
    {
        [SerializeField] private Card _card;
        [SerializeField] private AttributeType _attributeType;

        [Inject] private CoreGameConfig _config;
            
        private IntAttribute _attribute;
        private TMP_Text _text;
        private Tweener tweener;

        public Action OnAnimationFinished;
        public AttributeType Type => _attributeType;
        public Tween CurrentAnimation => tweener;
        public bool IsAnimationComplete => tweener == null || !tweener.active || tweener.IsComplete();
    
        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            
            _attribute = _card.GetAttribute(_attributeType);
            _text.text = _attribute.Value.ToString();
            
            _attribute.OnChange += UpdateText;
        }

        private void OnValidate()
        {
            Assert.IsNotNull(_card);
        }

        private void OnEnable()
        {
            if(_attribute != null)
                _attribute.OnChange += UpdateText;
        }
        
        private void OnDisable()
        {
            _attribute.OnChange -= UpdateText;
        }

        private void UpdateText(int from, int to)
        {
            tweener?.Complete();
            tweener = DOTween.To(x => _text.text = ((int)x).ToString(), from, to, _config.AttributeChangeDuration)
                .OnComplete(()=>
                {
                    _text.text = to.ToString();
                    OnAnimationFinished?.Invoke();
                });
        }
    }
}