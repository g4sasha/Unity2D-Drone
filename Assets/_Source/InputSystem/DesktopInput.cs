using Providers;
using UnityEngine;

namespace InputSystem
{
    public class DesktopInput : InputHandler
    {
        private KeyMap _keyMap;

        public override void Initialize(object keyMap)
        {
            _keyMap = (KeyMap)keyMap;
        }

        public override Vector2 GetMoveDirection()
        {
            var moveX = Input.GetKey(_keyMap.MoveLeft) ? -1f : Input.GetKey(_keyMap.MoveRight) ? 1f : 0f;
            var moveY = Input.GetKey(_keyMap.TakeOff) ? 1f : 0f;
            var direction = new Vector2(moveX, moveY).normalized;

            return direction;
        }
    }
}