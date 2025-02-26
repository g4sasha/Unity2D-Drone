using UnityEngine;

namespace Gameplay
{
    public class Grabler : MonoBehaviour
    {
        [SerializeField] private GameObject _drone;
        [SerializeField] private SpringJoint2D _p1;
        [SerializeField] private SpringJoint2D _p2;

        private Transform _target;
        private bool _catched;
        private Rigidbody2D _droneRigidbody;

        private void Awake()
        {
            _droneRigidbody = _drone.GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Box")
            {
                _target = other.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Box")
            {
                _target = null;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _target != null && !_catched)
            {
                _p1.gameObject.SetActive(true);
                _p2.gameObject.SetActive(true);
                _p1.connectedBody = _target.GetComponent<Rigidbody2D>();
                _p2.connectedBody = _target.GetComponent<Rigidbody2D>();
                _catched = true;
                return;
            }

            if (Input.GetKeyDown(KeyCode.E) && _catched)
            {
                _p1.gameObject.SetActive(false);
                _p2.gameObject.SetActive(false);
                _p1.connectedBody = null;
                _p2.connectedBody = null;
                _catched = false;
                return;
            }
        }
    }
}