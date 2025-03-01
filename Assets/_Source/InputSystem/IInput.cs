using System;
using UnityEngine;
using VContainer.Unity;

namespace InputSystem
{
    public interface IInput : IInitializable
    {
        public event Action<Vector2> OnMove;
        public event Action OnCatch;
    }
}