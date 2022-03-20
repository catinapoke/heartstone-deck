using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils.SpriteProvider
{
    public class LoremPicsumPreloadedProvider : ISpriteProvider, IInitializable
    {
        [Inject] private LoremPicsumImageRequester _picsumRequester;
        [Inject] private RandomImageProvider _randomImageProvider;
        [Inject] private CoroutineLoader _coroutineLoader;
        
        private Queue<Sprite> _sprites;
        private int _capacity;

        public bool IsFull => _sprites != null && _sprites.Count == _capacity;

        public LoremPicsumPreloadedProvider(int capacity = 16)
        {
            _capacity = capacity;
        }
        
        public void Initialize()
        {
            _sprites = new Queue<Sprite>(_capacity);
            for (int i = 0; i < _capacity; i++)
            {
                RequestSprite();
            }
        }

        private void RequestSprite()
        {
            _coroutineLoader.StartCoroutine(_picsumRequester.GetTextureRequest(AddSprite));
        }

        private void AddSprite(Sprite sprite)
        {
            _sprites.Enqueue(sprite);
        }
        
        public Sprite GetSprite()
        {
            if (_sprites.Count == 0) 
                return _randomImageProvider.GetSprite();

            RequestSprite();
            return _sprites.Dequeue();
        }
    }
}