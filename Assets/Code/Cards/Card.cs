using System;
using System.Collections.Generic;
using Extensions;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
    
        private Dictionary<AttributeType, IntAttribute> _attributes;

        private void OnValidate()
        {
            Assert.IsNotNull(_name);
            Assert.IsNotNull(_description);
            Assert.IsNotNull(_icon);
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
    }
}

