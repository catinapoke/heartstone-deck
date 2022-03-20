using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CoreGameConfig", menuName = "ScriptableObjects/CoreGameConfig")]
    public class CoreGameConfig : ScriptableObject
    {
        [Header("Card spawn")]
        [SerializeField] private int _minCardSpawnCount;
        [SerializeField] private int _maxCardSpawnCount;

        [Header("Visual")] 
        [SerializeField] private float _attributeChangeDuration;
        [SerializeField] private float _animatorDuration;
        [SerializeField] private float _cardMoveSpeed;
        
        public int MinCardSpawnCount => _minCardSpawnCount;
        public int MaxCardSpawnCount => _maxCardSpawnCount;
        
        public float AttributeChangeDuration => _attributeChangeDuration;
        public float AnimatorDuration => _animatorDuration;
        public float CardMoveSpeed => _cardMoveSpeed;
    }
}