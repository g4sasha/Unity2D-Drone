using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class CaughtIndicator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _notCaught;
        [SerializeField] private Color _canCaught;
        [SerializeField] private Color _caught;

        private Dictionary<CaughtState, Color> _states;
        private Catcher _catcher;

        [Inject]
        private void Construct(Catcher catcher)
        {
            _catcher = catcher;
        }

        private void Awake()
        {
            _states = new Dictionary<CaughtState, Color>()
            {
                { CaughtState.CannotCaught, _notCaught },
                { CaughtState.CanCaught, _canCaught },
                { CaughtState.Caught, _caught }
            };
        }

        private void OnEnable()
        {
            _catcher.OnStateChanged += Change;
        }

        private void OnDisable()
        {
            _catcher.OnStateChanged -= Change;
        }

        public void Change(CaughtState state)
        {
            _spriteRenderer.color = _states[state];
        }
    }
}