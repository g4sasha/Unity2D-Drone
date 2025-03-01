using InputSystem;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class EntitiesLifetimeScope : LifetimeScope
    {
        private IInput _input = new DesktopInput();

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_input);
        }
    }
}