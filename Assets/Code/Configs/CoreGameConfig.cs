using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CoreGameConfig", menuName = "ScriptableObjects/CoreGameConfig")]
    public class CoreGameConfig : ScriptableObject
    {
        [SerializeField] private LayerMask _cardLayerMask;
        [SerializeField] private int _minCardSpawnCount;
        [SerializeField] private int _maxCardSpawnCount;

        public LayerMask CardLayer => _cardLayerMask;
        public int MinCardSpawnCount => _minCardSpawnCount;
        public int MaxCardSpawnCount => _maxCardSpawnCount;
    }
}