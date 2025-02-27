using System;
using Core;
using UnityEngine;

namespace InputSystem
{
    public abstract class InputHandler : MonoInitializable, IInitializableData
    {
        public Action<Vector2> OnMove;
        public bool Enabled { get; set; } = true;

        private void Update()
        {
            if (Enabled)
            {
                HandleMovement();
            }
        }

        public abstract void HandleMovement();
    }
}