using UnityEngine;
using Zenject;

namespace Utils.SpriteProvider
{
    public class RandomImageProvider : ISpriteProvider
    {
        [Inject] private SpriteContainer _spriteContainer;
        
        public Sprite GetSprite()
        {
            return _spriteContainer.GetRandom();
        }
    }
}