using UnityEngine;
using UnityEngine.Serialization;

namespace MelonJam4.Factory
{
    public sealed class MiniGameItem : MonoBehaviour
    {
        [HideInInspector]
        public bool IsWrong;

        [HideInInspector]
        public AudioSource Source;

        [HideInInspector]
        public float CurrentBeat;

        [FormerlySerializedAs("Start")]
        [HideInInspector]
        public Vector3 StartPos;

        [HideInInspector]
        public Vector3 End;

        [HideInInspector]
        public Vector3 Step;

        [HideInInspector]
        public Vector3 Target;

        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private MeshFilter _filter;

        [SerializeField]
        private Mesh _crashedMesh;

        [SerializeField]
        private float _perfectPrecision;

        [SerializeField]
        private float _goodPrecision;

        [SerializeField]
        private float _badPrecision;

        [SerializeField]
        private Color _normalColor;

        [SerializeField]
        private Color _okColor;

        [SerializeField]
        private Color _wrongColor;

        #region RuntimeVariables

        private bool _isSmashed;
        private bool _isInZone;
        private Color _targetColor;

        #endregion

        #region Unity

        private void Awake()
        {
            _targetColor = _normalColor;
            UpdateVisibility();
        }

        private void Update()
        {
            UpdateVisibility();
            UpdatePosition();
            ListenForInput();
            UpdateColor();
        }

        private void UpdateColor()
        {
            _renderer.material.color = _targetColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MiniGame>(out _))
            {
                _targetColor = IsWrong ? _wrongColor : _okColor;
                _isInZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<MiniGame>(out _))
            {
                _targetColor = _normalColor;
                _isInZone = false;
            }

            if (IsWrong == false && _isSmashed == false)
            {
                OnHit(HitType.Miss);
            }

            _isSmashed = true;
        }

        #endregion

        private bool IsCBetweenAB(Vector3 a, Vector3 b, Vector3 c)
        {
            return Vector3.Dot((b - a).normalized, (c - b).normalized) < 0f && Vector3.Dot((a - b).normalized, (c - a).normalized) < 0f;
        }

        private void UpdatePosition()
        {
            var deltaTime = Source.time - CurrentBeat;
            transform.position = Target + Step * deltaTime;
        }

        private void UpdateVisibility()
        {
            _renderer.enabled = IsCBetweenAB(StartPos, End, transform.position);
        }

        private void ListenForInput()
        {
            if (_isInZone == false || _isSmashed)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Smash();
            }
        }

        private void Smash()
        {
            var precision = Mathf.Abs(Source.time - CurrentBeat);

            if (IsWrong || precision > _badPrecision)
            {
                OnHit(HitType.Miss);
                UpdateMesh();
            }
            else if (precision <= _perfectPrecision)
            {
                OnHit(HitType.Perfect);
                UpdateMesh();
            }
            else if (precision <= _goodPrecision)
            {
                OnHit(HitType.Good);
                UpdateMesh();
            }
            else if (precision <= _badPrecision)
            {
                OnHit(HitType.Bad);
                UpdateMesh();
            }
        }

        private void OnHit(HitType type)
        {
            _isSmashed = true;
            HitView.Instance.OnHit(type);
        }

        private void UpdateMesh()
        {
            _filter.mesh = _crashedMesh;
        }
    }
}
