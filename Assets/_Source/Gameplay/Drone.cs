using Providers;
using UnityEngine;

namespace Gameplay
{
    public class Drone
    {
        private DroneProvider _drone;

        public Drone(DroneProvider drone)
        {
            _drone = drone;
        }

        public void Move(Vector2 direction)
        {
            _drone.Rigidbody.velocity = new Vector2(direction.x * _drone.MaxSpeed * _drone.RotateSpeed, direction.y * _drone.MaxSpeed);
        }

        public void Rotate()
        {
            _drone.Transform.rotation = Quaternion.Euler(0f, 0f, -_drone.Rigidbody.velocity.x * _drone.MaxAngle * Time.fixedDeltaTime);
        }

        public void Stabilize()
        {
            _drone.Rigidbody.velocity = new Vector2(Mathf.Lerp(_drone.Rigidbody.velocity.x, 0f, _drone.RecoveryRotateSpeed), _drone.Rigidbody.velocity.y);
        }

        public void SpeedClamp()
        {
            _drone.Rigidbody.velocity = new Vector2(
                Mathf.Clamp(_drone.Rigidbody.velocity.x, -_drone.MaxSpeed, _drone.MaxSpeed),
                Mathf.Clamp(_drone.Rigidbody.velocity.y, -_drone.MaxSpeed, _drone.MaxSpeed)
            );
        }
    }
}