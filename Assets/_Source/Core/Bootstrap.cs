using InputSystem;
using UnityEngine;
using VContainer;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        private IInput _input;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }

        private void Start()
        {
            _input.Initialize();
        }
    }
}