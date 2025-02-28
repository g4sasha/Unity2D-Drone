// using System;
// using UnityEngine;

// namespace Gameplay
// {
//     [RequireComponent(typeof(GrappleRope))]
//     public class Grabler : MonoBehaviour
//     {
//         [field: SerializeField] public SpringJoint2D RightPoint { get; private set; }
//         [field: SerializeField] public SpringJoint2D LeftPoint { get; private set; }

//         public event Action<GrabState> OnGrabStateChanged;

//         [SerializeField] private GameObject _drone;

//         private Transform _target;
//         private Rigidbody2D _catchedObject;
//         private bool _catched;
//         private GrappleRope _grappleRope;

//         private void Awake()
//         {
//             _grappleRope = GetComponent<GrappleRope>();
//         }

//         private void OnTriggerEnter2D(Collider2D other)
//         {
//             if (other.gameObject.CompareTag("Box"))
//             {
//                 _target = other.transform;
//                 OnGrabStateChanged?.Invoke(GrabState.CanGrabled);
//             }
//         }

//         private void OnTriggerExit2D(Collider2D other)
//         {
//             if (other.gameObject.CompareTag("Box"))
//             {
//                 _target = null;
//                 OnGrabStateChanged?.Invoke(GrabState.NotGrabled);
//             }
//         }

//         private void Update()
//         {
//             if (Input.GetKeyDown(KeyCode.E))
//             {
//                 if (!_catched && _target != null)
//                 {
//                     Grab();
//                 }
//                 else if (_catched)
//                 {
//                     Release();
//                 }
//             }
//         }

//         private void FixedUpdate()
//         {
//             if (_catched)
//             {
//                 OnGrabStateChanged?.Invoke(GrabState.Grabled);
//                 _grappleRope.UpdateRope(_catchedObject);
//             }
//         }

//         private void Grab()
//         {
//             LeftPoint.gameObject.SetActive(true);
//             RightPoint.gameObject.SetActive(true);

//             _catchedObject = _target.GetComponent<Rigidbody2D>();
//             LeftPoint.connectedBody = _catchedObject;
//             RightPoint.connectedBody = _catchedObject;

//             _catchedObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
//             _catchedObject.constraints = RigidbodyConstraints2D.FreezeRotation;

//             _catched = true;
//             _grappleRope.SetActive(true);
//         }

//         private void Release()
//         {
//             LeftPoint.gameObject.SetActive(false);
//             RightPoint.gameObject.SetActive(false);

//             LeftPoint.connectedBody = null;
//             RightPoint.connectedBody = null;

//             _catchedObject.constraints = RigidbodyConstraints2D.None;
//             _catchedObject = null;
//             _catched = false;
//             _grappleRope.SetActive(false);
//         }
//     }
// }