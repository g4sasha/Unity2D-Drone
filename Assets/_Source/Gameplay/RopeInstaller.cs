using Core;
using Drone;
using UnityEngine;

namespace Gameplay
{
    public class RopeInstaller : MonoInstaller
    {
        [field: SerializeField] public CatcherInstaller CatcherInstaller { get; private set; }
        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }
        [field: SerializeField] public Transform[] Points { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public Color StartColor { get; private set; }
        [field: SerializeField] public Color EndColor { get; private set; }
        [field: SerializeField] public float StartWidth { get; private set; }
        [field: SerializeField] public float EndWidth { get; private set; }

        private RopeController _controller;
        private Rope _rope;

        public override void Init()
        {
            _rope = new(this);
            _controller = new(this, _rope, CatcherInstaller.Catcher);

            LineRenderer.positionCount = Points.Length;
            LineRenderer.startWidth = StartWidth;
            LineRenderer.endWidth = EndWidth;
            LineRenderer.material = Material;
            LineRenderer.startColor = StartColor;
            LineRenderer.endColor = EndColor;
        }

        private void Update()
        {
            _controller.Step();
        }
    }

    public class RopeController
    {
        private readonly RopeInstaller _installer;
        private readonly Rope _rope;
        private readonly Catcher _catcher;

        public RopeController(RopeInstaller installer, Rope rope, Catcher catcher)
        {
            _installer = installer;
            _rope = rope;
            _catcher = catcher;
        }

        public void Step()
        {
            if (_catcher.TryGetCaught(out Rigidbody2D caught))
            {
                _rope.Draw(caught);
            }
            else
            {
                _rope.Draw(null);
            }
        }

        public void SetActive(bool active)
        {
            _installer.gameObject.SetActive(active);
        }
    }

    public class Rope
    {
        private readonly RopeInstaller _installer;

        public Rope(RopeInstaller installer)
        {
            _installer = installer;
        }

        public void Draw(Rigidbody2D caught)
        {
            if (!_installer.isActiveAndEnabled || caught == null) return;

            for (int i = 0; i < _installer.Points.Length; i++)
            {
                _installer.LineRenderer.SetPosition(i, _installer.Points[i].position);
            }
        }
    }
}