using UnityEngine;

namespace Gameplay
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _recoveryRotateSpeed;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector2.up * _maxSpeed, ForceMode2D.Force);
            }

            var moveX = Input.GetAxis("Horizontal");
            _rigidbody.AddForce(Vector2.right * moveX * _rotateSpeed, ForceMode2D.Force);

            if (moveX == 0f)
            {
                _rigidbody.velocity = new Vector2(Mathf.Lerp(_rigidbody.velocity.x, 0f, _recoveryRotateSpeed), _rigidbody.velocity.y);
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(
                Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed),
                Mathf.Clamp(_rigidbody.velocity.y, -_maxSpeed, _maxSpeed)
            );

            transform.rotation = Quaternion.Euler(0f, 0f, -_rigidbody.velocity.x * _maxAngle * Time.fixedDeltaTime);
        }
    }
}