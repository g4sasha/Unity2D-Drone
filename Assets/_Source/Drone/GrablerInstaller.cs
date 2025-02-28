using Core;
using InputSystem;
using UnityEngine;

namespace Drone
{
    public class GrablerInstaller : MonoInitializable
    {
        [field: SerializeField] public SpringJoint2D LeftPoint { get; private set; }
        [field: SerializeField] public SpringJoint2D RightPoint { get; private set; }
        [field: SerializeField] public string PickTag { get; private set; }

        private Grabler _grabler;
        private GrablerController _controller;

        public override void Init()
        {
            var services = ServiceLocator.Instance;
            var input = services.Get<IInputHandler>();

            _grabler = new Grabler(this);
            _controller = new GrablerController(input, _grabler);
        }

        private void Update()
        {
            _controller.Step();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(PickTag))
            {
                _controller.SetCurrentTarget(other.gameObject);
                _controller.SetCanGrab(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(PickTag))
            {
                _controller.SetCanGrab(false);
                _controller.SetCurrentTarget(null);
            }
        }
    }

    public class GrablerController
    {
        private readonly IInputHandler _input;
        private readonly Grabler _grabler;
        private bool _canGrab;
        private GameObject _currentTarget;

        public GrablerController(IInputHandler input, Grabler grabler)
        {
            _input = input;
            _grabler = grabler;
        }

        public void Step()
        {
            if (_input.GetGrabActivation())
            {
                if (!_grabler.Catched && _canGrab && _currentTarget != null)
                {
                    _grabler.Grab(_currentTarget);
                }
                else if (_grabler.Catched)
                {
                    _grabler.Release();
                }
            }
        }

        public void SetCanGrab(bool canGrab)
        {
            _canGrab = canGrab;
        }

        public void SetCurrentTarget(GameObject target)
        {
            _currentTarget = target;
        }
    }

    public class Grabler
    {
        public bool Catched { get; private set; }

        private readonly GrablerInstaller _installer;
        private GameObject _catchedObject;

        public Grabler(GrablerInstaller installer)
        {
            _installer = installer;
            _installer.LeftPoint.gameObject.SetActive(false);
            _installer.RightPoint.gameObject.SetActive(false);
        }

        public void Grab(GameObject target)
        {
            Catched = true;
            _catchedObject = target;

            _installer.LeftPoint.gameObject.SetActive(true);
            _installer.RightPoint.gameObject.SetActive(true);

            var rb = _catchedObject.GetComponent<Rigidbody2D>();
            _installer.LeftPoint.connectedBody = rb;
            _installer.RightPoint.connectedBody = rb;

            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void Release()
        {
            Catched = false;

            _installer.LeftPoint.gameObject.SetActive(false);
            _installer.RightPoint.gameObject.SetActive(false);

            _installer.LeftPoint.connectedBody = null;
            _installer.RightPoint.connectedBody = null;

            if (_catchedObject != null)
            {
                var rb = _catchedObject.GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.None;
            }
            _catchedObject = null;
        }
    }
}