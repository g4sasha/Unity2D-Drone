using InputSystem;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _flightSpeed;
        [SerializeField] private float _rotationIntensity;
        [SerializeField] private float _stabilizationSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxAngle;

        private InputHandler _input;

        [Inject]
        protected void Construct(InputHandler input)
        {
            _input = input;
        }

        private void OnEnable()
        {
            _input.OnMove += Move;
        }

        private void OnDisable()
        {
            _input.OnMove -= Move;
        }

        private void FixedUpdate()
        {
            RotateBySpeed();
            Stabilize();
            ClipSpeed();
        }

        private void Move(Vector2 direction)
        {
            _rigidbody.AddForce(direction * _flightSpeed * Time.deltaTime * 1000f, ForceMode2D.Force);
        }

        private void RotateBySpeed()
        {
            var angle = Mathf.Clamp(-_rigidbody.velocity.x * _rotationIntensity, -_maxAngle, _maxAngle);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void Stabilize()
        {
            var speedX = Mathf.Lerp(_rigidbody.velocity.x, 0f, _stabilizationSpeed);
            var speedY = _rigidbody.velocity.y;
            _rigidbody.velocity = new Vector2(speedX, speedY);
        }

        private void ClipSpeed()
        {
            var speedX = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
            var speedY = Mathf.Clamp(_rigidbody.velocity.y, -_maxSpeed, _maxSpeed);
            _rigidbody.velocity = new Vector2(speedX, speedY);
        }
    }
}