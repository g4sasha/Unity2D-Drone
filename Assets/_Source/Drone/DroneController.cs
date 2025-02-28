using InputSystem;

namespace Drone
{
    public class DroneController
    {
        private readonly IInputHandler _inputHandler;
        private readonly DroneMovement _droneMovement;

        public DroneController(IInputHandler inputHandler, DroneMovement droneMovement)
        {
            _inputHandler = inputHandler;
            _droneMovement = droneMovement;
        }

        public void Update()
        {
            var direction = _inputHandler.GetMoveDirection();

            _droneMovement.Move(direction);
            _droneMovement.RotateBySpeed();
            _droneMovement.Stabilize();
            _droneMovement.ClipSpeed();
        }
    }
}