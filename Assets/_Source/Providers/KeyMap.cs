using Core;
using UnityEngine;

namespace Providers
{
    [CreateAssetMenu(fileName = "New key map", menuName = "Key maps/New key map")]
    public class KeyMap : ScriptableObject
    {
        [field: SerializeField] public KeyCode TakeOff;
        [field: SerializeField] public KeyCode MoveLeft;
        [field: SerializeField] public KeyCode MoveRight;
    }
}