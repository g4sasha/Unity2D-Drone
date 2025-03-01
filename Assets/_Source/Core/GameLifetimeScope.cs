using Gameplay;
using InputSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputHandler _input;
        [SerializeField] private Catcher _catcher;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_input);
            builder.RegisterComponent(_catcher);
        }
    }
}