using UnityEngine;

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

        [HideInInspector]
        public Vector3 Start;

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

        #region RuntimeVariables

        private bool _isSmashed;
        private bool _isInZone;

        #endregion

        #region Unity

        private void Awake()
        {
            UpdateVisibility();
        }

        private void Update()
        {
            UpdateVisibility();
            UpdatePosition();
            ListenForInput();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MiniGame>(out _))
            {
                _isInZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<MiniGame>(out _))
            {
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
            _renderer.enabled = IsCBetweenAB(Start, End, transform.position);
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
            }
            else if (precision <= _perfectPrecision)
            {
                OnHit(HitType.Perfect);
            }
            else if (precision <= _goodPrecision)
            {
                OnHit(HitType.Good);
            }
            else if (precision <= _badPrecision)
            {
                OnHit(HitType.Bad);
            }
        }

        private void OnHit(HitType type)
        {
            _isSmashed = true;
            //_filter.mesh = _crashedMesh;
            HitView.Instance.OnHit(type);
        }
    }
}
