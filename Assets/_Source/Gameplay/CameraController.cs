using UnityEngine;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, 0f);
        }
    }
}