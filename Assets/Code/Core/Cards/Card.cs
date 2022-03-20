using System;
using System.Collections.Generic;
using Extensions;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Core.Cards
{
    [RequireComponent(typeof(CardAnimator))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
    
        private Dictionary<AttributeType, IntAttribute> _attributes;
        private CardAnimator _animator;

        public event Action<Card> OnDeath; 

        public CardAnimator Animator => _animator;

        private void Awake()
        {
            _animator = GetComponent<CardAnimator>();
        }

        private void OnValidate()
        {
            Assert.IsNotNull(_name);
            Assert.IsNotNull(_description);
            Assert.IsNotNull(_icon);
        }

        private void Start()
        {
            GetAttribute(AttributeType.Health).OnChange += OnHealthUpdate;
        }

        private void OnDisable()
        {
            GetAttribute(AttributeType.Health).OnChange -= OnHealthUpdate;
            foreach (IntAttribute attribute in GetAttributesEnumerable())
            {
                attribute.Dispose();
            }
        }

        public void Init(CardData data)
        {
            _attributes = new Dictionary<AttributeType, IntAttribute>()
            {
                {AttributeType.Health, data.Health},
                {AttributeType.Attack, data.Attack},
                {AttributeType.ManaCost, data.ManaCost},
            };

            _name.text = data.Name;
            _description.text = data.Description;
            _icon.sprite = data.Icon;
        }
    
        public IntAttribute GetAttribute(AttributeType type)
        {
            if (_attributes.ContainsKey(type))
                return _attributes[type];

            Debug.LogWarning("Can't find attribute!");
            return null;
        }

        public IntAttribute GetRandomAttribute()
        {
            return _attributes.Values.GetRandom();
        }

        public IEnumerable<IntAttribute> GetAttributesEnumerable()
        {
            return _attributes.Values;
        }

        private void OnHealthUpdate(int from, int to)
        {
            if (to < 1)
            {
                GetAttribute(AttributeType.Health).OnChange -= OnHealthUpdate;
                OnDeath?.Invoke(this);
            }
        }
    }
}

