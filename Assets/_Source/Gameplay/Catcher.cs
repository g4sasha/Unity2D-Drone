using InputSystem;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class Catcher : MonoBehaviour
    {
        [SerializeField] private SpringJoint2D[] _attachmentPoints;
        [SerializeField] private string _matchingTag;

        private InputHandler _input;
        private GameObject _target;
        private bool _isCaught;

        [Inject]
        private void Construct(InputHandler input)
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
            if (other.gameObject.CompareTag(_matchingTag))
            {
                _target = other.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_matchingTag))
            {
                _target = null;
            }
        }

        private void OnCatch()
        {
            if (_target && !_isCaught)
            {
                Catch(_target);
            }
            else
            {
                Release();
            }
        }

        private void Catch(GameObject target)
        {
            _isCaught = true;
            var targetRigidbody = target.GetComponent<Rigidbody2D>();

            SetPointsActive(true);
            SetPointsConnectedBody(targetRigidbody);

            targetRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            target.transform.rotation = Quaternion.identity;
        }

        private void Release()
        {
            _isCaught = false;

            if (_target)
            {
                var rigidbody = _target.GetComponent<Rigidbody2D>();
                rigidbody.constraints = RigidbodyConstraints2D.None;
            }

            _target = null;
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