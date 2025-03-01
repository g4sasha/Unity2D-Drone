using UnityEngine;

namespace Simple
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, 0f);
        }
    }
}