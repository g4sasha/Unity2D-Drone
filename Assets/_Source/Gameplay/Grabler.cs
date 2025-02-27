using UnityEngine;

namespace Gameplay
{
    public class Grabler : MonoBehaviour
    {
        [SerializeField] private GameObject _drone;
        [SerializeField] private SpringJoint2D _p1;
        [SerializeField] private SpringJoint2D _p2;
        [SerializeField] private SpriteRenderer _grabIndicator;
        [SerializeField] private LineRenderer _lineRenderer; // Добавляем LineRenderer

        private Transform _target;
        private Rigidbody2D _catchedObject;
        private bool _catched;

        private void Awake()
        {
            _grabIndicator.color = Color.red;

            // Настройка LineRenderer
            _lineRenderer.positionCount = 4; // Две линии (p1 -> объект, p2 -> объект)
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = Color.black;
            _lineRenderer.endColor = Color.black;
            _lineRenderer.enabled = false; // Скрываем линию в начале
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                _target = other.transform;

                if (!_catched)
                {
                    _grabIndicator.color = Color.green;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                _target = null;

                if (!_catched)
                {
                    _grabIndicator.color = Color.red;
                }
            }
        }

        private void Update()
        {
            if (_catched)
            {
                _grabIndicator.color = Color.blue;
                UpdateRope(); // Обновляем верёвку
            }

            if (Input.GetKeyDown(KeyCode.E) && _target != null && !_catched)
            {
                _p1.gameObject.SetActive(true);
                _p2.gameObject.SetActive(true);
                _catchedObject = _target.GetComponent<Rigidbody2D>();
                _p1.connectedBody = _catchedObject;
                _p2.connectedBody = _catchedObject;
                _target.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                _catchedObject.constraints = RigidbodyConstraints2D.FreezeRotation;
                _catched = true;
                _lineRenderer.enabled = true; // Включаем линию
                return;
            }

            if (Input.GetKeyDown(KeyCode.E) && _catched)
            {
                _p1.gameObject.SetActive(false);
                _p2.gameObject.SetActive(false);
                _p1.connectedBody = null;
                _p2.connectedBody = null;
                _catchedObject.constraints = RigidbodyConstraints2D.None;
                _catched = false;
                _lineRenderer.enabled = false; // Отключаем линию
                return;
            }
        }

        private void UpdateRope()
        {
            if (_catchedObject != null)
            {
                _lineRenderer.SetPosition(0, _p1.transform.position);
                _lineRenderer.SetPosition(1, _catchedObject.position + Vector2.left * 0.375f + Vector2.down * 0.375f);
                _lineRenderer.SetPosition(2, _catchedObject.position + Vector2.right * 0.375f + Vector2.down * 0.375f);
                _lineRenderer.SetPosition(3, _p2.transform.position);
            }
        }
    }
}