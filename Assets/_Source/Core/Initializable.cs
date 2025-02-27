using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class Initializable
    {
        [field: SerializeField] public MonoInitializable Target { get; set; }
        [field: SerializeField] public Object Data { get; set; }

        public Initializable(MonoInitializable target, Object data)
        {
            Target = target;
            Data = data;
        }
    }
}