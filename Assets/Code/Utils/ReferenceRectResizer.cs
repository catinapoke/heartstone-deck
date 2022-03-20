using System;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(RectTransform))]
    public class ReferenceRectResizer : MonoBehaviour
    {
        [SerializeField] private Vector2 referenceRect;
        [SerializeField] private Vector2 referenceScreenSize;

        private void Awake()
        {
            ResizeUsingReference(transform as RectTransform);
        }
        
        private void ResizeUsingReference(RectTransform rectTransform)
        {
            float width = Screen.width;
            float height = Screen.height;

            float ratio = Math.Max(referenceScreenSize.x / width, referenceScreenSize.y / height);

            Rect rect = rectTransform.rect;
            Vector2 delta = new Vector2(
                referenceRect.x / ratio - rect.width,
                referenceRect.y / ratio - rect.height);

            delta += rectTransform.sizeDelta;
            
            rectTransform.sizeDelta = delta;
        }
    }
}