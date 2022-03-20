using System;
using UnityEngine;

namespace Utils
{
    public class CoroutineLoader : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}