using UnityEngine;

namespace Gameplay
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _rotateSpeed;

        private void Update()
        {
            if (_rigidbody.velocity.magnitude >= _maxSpeed)
            {
                return;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector2.up * _maxSpeed, ForceMode2D.Force);
            }

            _rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * _rotateSpeed, ForceMode2D.Force);
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(0, 0, -_rigidbody.velocity.x * _maxAngle * Time.fixedDeltaTime);
        }
    }
}