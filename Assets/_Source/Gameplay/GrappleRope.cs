// using UnityEngine;

// namespace Gameplay
// {
//     [RequireComponent(typeof(LineRenderer))]
//     public class GrappleRope : MonoInitializable
//     {
//         private LineRenderer _lineRenderer;
//         private bool _isActive;

//         private void OnValidate()
//         {
//             if (_lineRenderer == null)
//             {
//                 _lineRenderer = GetComponent<LineRenderer>();
//             }
//         }

//         public override void Initialize(object data)
//         {
//             _lineRenderer.positionCount = 4;
//             _lineRenderer.startWidth = 0.05f;
//             _lineRenderer.endWidth = 0.05f;
//             _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
//             _lineRenderer.startColor = Color.black;
//             _lineRenderer.endColor = Color.black;
//             _lineRenderer.enabled = false;
//         }

//         public void SetActive(bool active)
//         {
//             _isActive = active;
//             _lineRenderer.enabled = active;
//         }

//         public void UpdateRope(Rigidbody2D catchedObject)
//         {
//             if (!_isActive || catchedObject == null) return;

//             _lineRenderer.SetPosition(0, transform.parent.GetComponent<Grabler>().LeftPoint.transform.position);
//             _lineRenderer.SetPosition(1, catchedObject.position + Vector2.left * 0.375f + Vector2.down * 0.375f);
//             _lineRenderer.SetPosition(2, catchedObject.position + Vector2.right * 0.375f + Vector2.down * 0.375f);
//             _lineRenderer.SetPosition(3, transform.parent.GetComponent<Grabler>().RightPoint.transform.position);
//         }
//     }
// }
