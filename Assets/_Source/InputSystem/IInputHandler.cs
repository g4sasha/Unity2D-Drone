using Core;
using UnityEngine;

namespace InputSystem
{
    public interface IInputHandler : IService
    {
        public Vector2 GetMoveDirection();
        public bool GetGrabActivation();
    }
}