using Core;

namespace InputSystem
{
    public class InputInstaller : MonoInitializable
    {
        public override void Init()
        {
            var services = ServiceLocator.Instance;
            var gameInput = new DesktopInput();
            services.Register<IInputHandler>(gameInput);
        }
    }
}