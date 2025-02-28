using UnityEngine;

namespace InputSystem
{
    public class DesktopInput : IInputHandler
    {
        public Vector2 GetMoveDirection()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var moveDirection = new Vector2(horizontal, vertical).normalized;

            return moveDirection;
        }
    }
}