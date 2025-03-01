using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace InputSystem
{
    public class DesktopInput : IInput
    {
        public event Action<Vector2> OnMove;
        public event Action OnCatch;

        public void Initialize() => StartHandling().Forget();

        private async UniTaskVoid StartHandling()
        {
            while (Application.isPlaying)
            {
                HandleMovement();
                HandleCatch();

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
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