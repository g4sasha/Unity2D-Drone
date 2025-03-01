using Core;
using InputSystem;
using UnityEngine;

namespace Drone
{
    public class DroneInstaller : MonoInstaller
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public float FlightSpeed { get; private set; }
        [field: SerializeField] public float RotationIntensity { get; private set; }
        [field: SerializeField] public float StabilizationSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MaxAngle { get; private set; }

        private DroneController _controller;
        private DroneMovement _movement;

        public override void Init()
        {
            var services = ServiceLocator.Instance;
            var input = services.Get<IInputHandler>();

            _movement = new(this);
            _controller = new(input, _movement);
        }

        private void FixedUpdate()
        {
            _controller.Step();
        }
    }

    public class DroneController
    {
        private readonly IInputHandler _inputHandler;
        private readonly DroneMovement _droneMovement;

        public DroneController(IInputHandler inputHandler, DroneMovement droneMovement)
        {
            _inputHandler = inputHandler;
            _droneMovement = droneMovement;
        }

        public void Step()
        {
            var direction = _inputHandler.GetMoveDirection();

            _droneMovement.Move(direction);
            _droneMovement.RotateBySpeed();
            _droneMovement.Stabilize();
            _droneMovement.ClipSpeed();
        }
    }

    public class DroneMovement
    {
        private readonly DroneInstaller _installer;

        public DroneMovement(DroneInstaller installer)
        {
            _installer = installer;
        }

        public void Move(Vector2 direction)
        {
            _installer.Rigidbody.AddForce(direction * _installer.FlightSpeed, ForceMode2D.Force);
        }

        public void RotateBySpeed()
        {
            var angle = Mathf.Clamp(-_installer.Rigidbody.velocity.x * _installer.RotationIntensity, -_installer.MaxAngle, _installer.MaxAngle);
            _installer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void Stabilize()
        {
            var speedX = Mathf.Lerp(_installer.Rigidbody.velocity.x, 0f, _installer.StabilizationSpeed);
            var speedY = _installer.Rigidbody.velocity.y;
            _installer.Rigidbody.velocity = new Vector2(speedX, speedY);
        }

        public void ClipSpeed()
        {
            var speedX = Mathf.Clamp(_installer.Rigidbody.velocity.x, -_installer.MaxSpeed, _installer.MaxSpeed);
            var speedY = Mathf.Clamp(_installer.Rigidbody.velocity.y, -_installer.MaxSpeed, _installer.MaxSpeed);
            _installer.Rigidbody.velocity = new Vector2(speedX, speedY);
        }
    }
}