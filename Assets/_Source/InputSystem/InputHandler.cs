using System;
using UnityEngine;

namespace InputSystem
{
    public abstract class InputHandler : MonoBehaviour
    {
        public Action<Vector2> OnMove;
        public Action OnCatch;
    }
}