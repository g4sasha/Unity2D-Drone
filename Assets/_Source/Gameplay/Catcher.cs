using System;
using InputSystem;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class Catcher : MonoBehaviour
    {
        public event Action<CaughtState> OnStateChanged;
        public bool IsCaught => _isCaught;
        public Cargo CurrentCargo => _cargo;

        [SerializeField] private SpringJoint2D[] _attachmentPoints;
        [SerializeField] private string _matchingTag;

        private InputHandler _input;
        private Cargo _cargo;
        private Cargo _lastCargo;
        private bool _isCaught;

        [Inject]
        protected void Construct(InputHandler input)
        {
            _input = input;
        }

        private void OnEnable()
        {
            _input.OnCatch += OnCatch;
        }

        private void OnDisable()
        {
            _input.OnCatch -= OnCatch;
        }

        private void Start()
        {
            SetPointsActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_matchingTag) && !_isCaught)
            {
                _cargo = other.GetComponent<Cargo>();

                if (!_isCaught)
                {
                    OnStateChanged?.Invoke(CaughtState.CanCaught);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_matchingTag))
            {
                _cargo = null;

                if (!_isCaught)
                {
                    OnStateChanged?.Invoke(CaughtState.CannotCaught);
                }
            }
        }

        private void OnCatch()
        {
            if (_cargo && !_isCaught)
            {
                Catch(_cargo);
            }
            else
            {
                Release();
            }
        }

        private void Catch(Cargo cargo)
        {
            _isCaught = true;
            _lastCargo = cargo;
            OnStateChanged?.Invoke(CaughtState.Caught);

            SetPointsActive(true);
            SetPointsConnectedBody(cargo.Rigidbody);

            cargo.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            cargo.transform.rotation = Quaternion.identity;
        }

        private void Release()
        {
            _isCaught = false;
            _lastCargo.Rigidbody.constraints = RigidbodyConstraints2D.None;

            if (_cargo)
            {
                OnStateChanged?.Invoke(CaughtState.CanCaught);
            }
            else
            {
                OnStateChanged?.Invoke(CaughtState.CannotCaught);
            }

            SetPointsActive(false);
        }

        private void SetPointsActive(bool active)
        {
            foreach (var point in _attachmentPoints)
            {
                point.gameObject.SetActive(active);
            }
        }

        private void SetPointsConnectedBody(Rigidbody2D rigidbody)
        {
            foreach (var point in _attachmentPoints)
            {
                point.connectedBody = rigidbody;
            }
        }
    }
}