using UnityEngine;

namespace InputSystem
{
    public class DesktopInput : InputHandler
    {
        private void Update()
        {
            HandleMovement();
            HandleCatch();
        }

        private void HandleMovement()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var moveDirection = new Vector2(horizontal, vertical).normalized;

            OnMove?.Invoke(moveDirection);
        }

        private void HandleCatch()
        {
            if (Input.GetButtonDown("Jump"))
            {
                OnCatch?.Invoke();
            }
        }
    }
}