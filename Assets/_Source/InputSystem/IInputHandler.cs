using Core;
using UnityEngine;

namespace InputSystem
{
    public interface IInputHandler : IService
    {
        Vector2 GetMoveDirection();
    }
}