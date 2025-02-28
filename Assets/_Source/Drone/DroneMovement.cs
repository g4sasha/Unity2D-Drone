using UnityEngine;

namespace Drone
{
    public class DroneMovement
    {
        private readonly DroneInstaller _drone;

        public DroneMovement(DroneInstaller drone)
        {
            _drone = drone;
        }

        public void Move(Vector2 direction)
        {
            _drone.Rigidbody.AddForce(direction * _drone.FlightSpeed, ForceMode2D.Force);
        }

        public void RotateBySpeed()
        {
            var angle = Mathf.Clamp(-_drone.Rigidbody.velocity.x * _drone.RotationIntensity, -_drone.MaxAngle, _drone.MaxAngle);
            _drone.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void Stabilize()
        {
            var speedX = Mathf.Lerp(_drone.Rigidbody.velocity.x, 0f, _drone.StabilizationSpeed);
            var speedY = _drone.Rigidbody.velocity.y;
            _drone.Rigidbody.velocity = new Vector2(speedX, speedY);
        }

        public void ClipSpeed()
        {
            var speedX = Mathf.Clamp(_drone.Rigidbody.velocity.x, -_drone.MaxSpeed, _drone.MaxSpeed);
            var speedY = Mathf.Clamp(_drone.Rigidbody.velocity.y, -_drone.MaxSpeed, _drone.MaxSpeed);
            _drone.Rigidbody.velocity = new Vector2(speedX, speedY);
        }
    }
}