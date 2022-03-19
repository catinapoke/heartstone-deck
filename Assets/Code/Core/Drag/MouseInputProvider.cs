
using UnityEngine;

namespace Core.Drag
{
    public class MouseInputProvider : MonoInputProvider
    {
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                OnTouchStart?.Invoke();
            
            if(Input.GetMouseButtonUp(0))
                OnTouchFinish?.Invoke();
        }

        public override Vector2 GetPosition()
        {
            return Input.mousePosition;
        }

        public override bool IsHolding()
        {
            return Input.GetMouseButton(0);
        }
    }
}