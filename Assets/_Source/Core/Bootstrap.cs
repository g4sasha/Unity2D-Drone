using InputSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class Bootstrap : LifetimeScope
    {
        [SerializeField] private DesktopInput _input;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent<InputHandler>(_input);
        }
    }
}