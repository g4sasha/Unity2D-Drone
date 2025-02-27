using System;
using Core;
using UnityEngine;

namespace InputSystem
{
    public abstract class InputHandler : MonoInitializable
    {
        public bool Enabled { get; set; } = true;

        private void Update()
        {
            if (Enabled)
            {
                GetMoveDirection();
            }
        }

        public abstract Vector2 GetMoveDirection();
    }
}