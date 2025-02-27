using Core;
using InputSystem;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Drone : MonoInitializable
    {
        [SerializeField] private float _flightSpeed;
        [SerializeField] private float _rotationIntensity;
        [SerializeField] private float _stabilizationSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxAngle;

        [SerializeField] private Rigidbody2D _rigidbody;

        private InputHandler _input;

        public override void Initialize(object data)
        {
            _input = (InputHandler)data;
        }

        private void OnValidate()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        private void FixedUpdate()
        {
            var moveDirection = _input.GetMoveDirection();

            Move(moveDirection);
            Stabilize();
            SpeedClamp();
            Rotate();
        }

        public void Move(Vector2 direction)
        {
            _rigidbody.AddForce(direction * _flightSpeed, ForceMode2D.Force);
        }

        public void Rotate()
        {
            var angle = Mathf.Clamp(-_rigidbody.velocity.x * _rotationIntensity, -_maxAngle, _maxAngle);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void Stabilize()
        {
            var speedX = Mathf.Lerp(_rigidbody.velocity.x, 0f, _stabilizationSpeed);
            var speedY = _rigidbody.velocity.y;
            _rigidbody.velocity = new Vector2(speedX, speedY);
        }

        public void SpeedClamp()
        {
            var speedX = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
            var speedY = Mathf.Clamp(_rigidbody.velocity.y, -_maxSpeed, _maxSpeed);
            _rigidbody.velocity = new Vector2(speedX, speedY);
        }
    }
}