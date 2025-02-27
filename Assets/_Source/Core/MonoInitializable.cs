using UnityEngine;

namespace Core
{
    public abstract class MonoInitializable : MonoBehaviour
    {
        public abstract void Initialize(object data);
    }
}