using Core;
using InputSystem;
using UnityEngine;

namespace Drone
{
    public class CatcherInstaller : MonoInstaller
    {
        public Catcher Catcher => _catcher;

        [field: SerializeField] public SpringJoint2D LeftPoint { get; private set; }
        [field: SerializeField] public SpringJoint2D RightPoint { get; private set; }
        [field: SerializeField] public string PickTag { get; private set; }

        private Catcher _catcher;
        private CatcherController _controller;

        public override void Init()
        {
            var services = ServiceLocator.Instance;
            var input = services.Get<IInputHandler>();

            _catcher = new Catcher(this);
            _controller = new CatcherController(input, _catcher);
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

    public class CatcherController
    {
        private readonly IInputHandler _input;
        private readonly Catcher _catcher;
        private bool _canGrab;
        private GameObject _currentTarget;

        public CatcherController(IInputHandler input, Catcher catcher)
        {
            _input = input;
            _catcher = catcher;
        }

        public void Step()
        {
            if (_input.GetGrabActivation())
            {
                if (!_catcher.Caught && _canGrab && _currentTarget != null)
                {
                    _catcher.Grab(_currentTarget);
                }
                else if (_catcher.Caught)
                {
                    _catcher.Release();
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

    public class Catcher
    {
        public bool Caught { get; private set; }

        private readonly CatcherInstaller _installer;
        private GameObject _caughtObject;

        public Catcher(CatcherInstaller installer)
        {
            _installer = installer;
            _installer.LeftPoint.gameObject.SetActive(false);
            _installer.RightPoint.gameObject.SetActive(false);
        }

        public void Grab(GameObject target)
        {
            Caught = true;
            _caughtObject = target;

            _installer.LeftPoint.gameObject.SetActive(true);
            _installer.RightPoint.gameObject.SetActive(true);

            var rb = _caughtObject.GetComponent<Rigidbody2D>();
            _installer.LeftPoint.connectedBody = rb;
            _installer.RightPoint.connectedBody = rb;

            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void Release()
        {
            Caught = false;

            _installer.LeftPoint.gameObject.SetActive(false);
            _installer.RightPoint.gameObject.SetActive(false);

            _installer.LeftPoint.connectedBody = null;
            _installer.RightPoint.connectedBody = null;

            if (_caughtObject != null)
            {
                var rigidbody = _caughtObject.GetComponent<Rigidbody2D>();
                rigidbody.constraints = RigidbodyConstraints2D.None;
            }
            _caughtObject = null;
        }

        public bool TryGetCaught(out Rigidbody2D caught)
        {
            if (_caughtObject.TryGetComponent(out caught))
            {
                return true;
            }

            return false;
        }
    }
}