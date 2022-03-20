
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
            Vector2 pos = Input.mousePosition;

            pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
            return Input.mousePosition;//pos;// Input.mousePosition;
        }

        public override bool IsHolding()
        {
            return Input.GetMouseButton(0);
        }
    }
}