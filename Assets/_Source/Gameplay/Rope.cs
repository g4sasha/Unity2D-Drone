using UnityEngine;
using VContainer;
using System.Collections.Generic;

namespace Gameplay
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform[] _attachmentPoints;

        private Catcher _catcher;
        private Cargo _currentCargo;

        [Inject]
        protected void Construct(Catcher catcher)
        {
            _catcher = catcher;
        }

        private void LateUpdate()
        {
            if (_catcher.CurrentCargo != null)
            {
                _currentCargo = _catcher.CurrentCargo;
            }

            if (_catcher != null && _catcher.IsCaught)
            {
                Draw(_currentCargo);
            }
            else
            {
                _lineRenderer.positionCount = 0;
            }
        }

        private void Draw(Cargo cargo)
        {
            Vector3[] cargoAttachments = cargo.GetAttachments();
            var linePoints = new List<Vector3>();

            if (_attachmentPoints.Length > 0)
            {
                linePoints.Add(_attachmentPoints[0].position);
            }

            linePoints.AddRange(cargoAttachments);

            for (int i = _attachmentPoints.Length - 1; i >= 1; i--)
            {
                linePoints.Add(_attachmentPoints[i].position);
            }

            _lineRenderer.positionCount = linePoints.Count;
            _lineRenderer.SetPositions(linePoints.ToArray());
        }
    }
}