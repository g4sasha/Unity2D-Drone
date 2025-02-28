using Core;
using InputSystem;
using UnityEngine;

namespace Drone
{
    public class DroneInstaller : MonoInitializable
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
            _controller.Update();
        }
    }
}