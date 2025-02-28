// using UnityEngine;

// namespace Gameplay
// {
//     [RequireComponent(typeof(SpriteRenderer))]
//     public class GrabIndicator : MonoBehaviour
//     {
//         [SerializeField] private SpriteRenderer _spriteRenderer;
//         [SerializeField] private Color _notGrabled;
//         [SerializeField] private Color _canGrabled;
//         [SerializeField] private Color _grabled;
//         [SerializeField] private LayerMask _boxLayer;

//         private void OnValidate()
//         {
//             if (_spriteRenderer == null)
//             {
//                 _spriteRenderer = GetComponent<SpriteRenderer>();
//             }

//             _spriteRenderer.color = _notGrabled;
//         }

//         public void Awake()
//         {
//             _spriteRenderer.color = _notGrabled;
//         }

//         private void OnGrabStateChanged(GrabState state)
//         {
//             switch (state)
//             {
//                 case GrabState.NotGrabled:
//                     _spriteRenderer.color = _notGrabled;
//                     break;
//                 case GrabState.CanGrabled:
//                     _spriteRenderer.color = _canGrabled;
//                     break;
//                 case GrabState.Grabled:
//                     _spriteRenderer.color = _grabled;
//                     break;
//             }
//         }
//     }
// }