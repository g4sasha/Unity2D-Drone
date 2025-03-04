using UnityEngine;

namespace Gameplay
{
    public class Cargo : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [SerializeField] private Transform[] _attachmentPoints;

        public Vector3[] GetAttachments()
        {
            var attachments = new Vector3[_attachmentPoints.Length];

            for (int i = 0; i < _attachmentPoints.Length; i++)
            {
                attachments[i] = _attachmentPoints[i].position;
            }

            return attachments;
        }
    }
}