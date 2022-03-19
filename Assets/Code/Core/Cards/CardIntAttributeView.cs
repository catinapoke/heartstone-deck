using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;

namespace Core.Cards
{
    [RequireComponent(typeof(TMP_Text))]
    public class CardIntAttributeView : MonoBehaviour
    {
        [SerializeField] private Card _card;
        [SerializeField] private AttributeType _attributeType;
            
        private IntAttribute _attribute;
        private TMP_Text _text;
    
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
            _text.text = _attribute.Value.ToString();
        }
    }
}