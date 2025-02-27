using Core;
using Gameplay;
using InputSystem;
using UnityEngine;

namespace Providers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DroneProvider : MonoInitializable
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MaxAngle { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
        [field: SerializeField] public float RecoveryRotateSpeed { get; private set; }

        private InputHandler _input;
        private Drone _drone;

        public override void Initialize(object data)
        {
            _input = data as InputHandler;
            _drone = new Drone(this);
        }

        private void OnValidate()
        {
            if (Rigidbody == null)
            {
                Rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        private void Start()
        {
            _input.OnMove += Move;
        }

        private void OnDestroy()
        {
            _input.OnMove -= Move;
        }

        private void FixedUpdate()
        {
            _drone.Rotate();
            _drone.Stabilize();
            _drone.SpeedClamp();
        }

        private void Move(Vector2 direction)
        {
            _drone.Move(direction);
        }
    }
}