using System;
using Cards;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Card))]
public class CardIntAttributeView : MonoBehaviour
{
    [SerializeField] private string _attributeName;
    private IntAttribute _attribute;
    
    private void Awake()
    {
        Card card = GetComponent<Card>();
        
        
    }
}