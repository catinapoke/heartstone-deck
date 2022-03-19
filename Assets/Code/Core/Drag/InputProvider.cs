using System;
using UnityEngine;

namespace Core.Drag
{
    public interface IInputProvider
    {
        bool IsHolding(); 
        Vector2 GetPosition();
    }
 
    // Abstraction for mouse and touch pads
    public abstract class MonoInputProvider : MonoBehaviour, IInputProvider
    {
        public Action OnTouchStart;
        public Action OnTouchFinish;
        public abstract Vector2 GetPosition();
        public abstract bool IsHolding(); 
    }
}