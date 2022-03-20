using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Utils.SpriteProvider
{
    public class LoremPicsumImageRequester
    {
        [Inject] private RandomImageProvider _randomImageProvider;

        private string _urlFormat = "https://picsum.photos/{0}/{1}";
        private float _imageWidth;
        private float _imageHeight;

        public LoremPicsumImageRequester(int width = 338, int height = 443)
        {
            _imageWidth = width;
            _imageHeight = height;
        }

        public void SetImageSize(Vector2 size)
        {
            _imageWidth = size.x;
            _imageHeight = size.y;
        }

        public IEnumerator GetTextureRequest(Action<Sprite> callback)
        {
            string url = String.Format(_urlFormat, _imageWidth, _imageHeight);
            using(var request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();
                
                if (request.result == UnityWebRequest.Result.ProtocolError)
                {
                    callback(_randomImageProvider.GetSprite());
                }
                else
                {
                    if (request.isDone)
                    {
                        var texture = DownloadHandlerTexture.GetContent(request);
                        var rect = new Rect(0, 0, _imageWidth, _imageHeight);
                        var sprite = Sprite.Create(texture,rect,new Vector2(0.5f,0.5f));
                        callback(sprite);
                    }
                }
            }
        }
    }
}