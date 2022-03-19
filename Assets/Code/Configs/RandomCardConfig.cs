using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "RandomCardConfig", menuName = "ScriptableObjects/RandomCardConfig")]
    public class RandomCardConfig : ScriptableObject
    {
        [SerializeField] private int _lowBorder = 1;
        [SerializeField] private int _highBorder = 9;

        public int LowBorder => _lowBorder;
        public int HighBorder => _highBorder;
    }
}