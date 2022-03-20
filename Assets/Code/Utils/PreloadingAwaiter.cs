using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.SpriteProvider;
using Zenject;

namespace Utils
{
    public class PreloadingAwaiter : MonoBehaviour
    {
        [SerializeField] private string _nextScene;
        [Inject] private LoremPicsumPreloadedProvider _imageProvider;
    
        private async void Start()
        {
            while (!_imageProvider.IsFull)
                await Task.Yield();

            SceneManager.LoadScene(_nextScene);
        }
    }
}
