using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class InfiniteRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _duration;
        
        private void Awake()
        {
            transform.DOLocalRotate(_rotation, _duration, RotateMode.Fast)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
    }
}