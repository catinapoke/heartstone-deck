using Extensions;
using UnityEngine;

namespace Utils
{
    
    [CreateAssetMenu(fileName = "SpriteContainer", menuName = "ScriptableObjects/SpriteContainer")]
    public class SpriteContainer : ScriptableObject
    {
        [SerializeField] private Sprite[] _sprites;

        public Sprite Get(int index)
        {
            if (index < 0 || index > _sprites.Length - 1) 
                return null;

            return _sprites[index];
        }
        
        public Sprite GetRandom()
        {
            return _sprites.GetRandom();
        }
    }
}