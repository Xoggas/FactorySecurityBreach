using UnityEngine;

namespace MelonJam4.Factory
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Policeman : Robot
    {
        [Header("Robot Properties")]
        [SerializeField]
        private float _movementSpeed;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private float _dangerZoneRadius = 1f;

        [Header("Movement Properties")]
        [SerializeField]
        private PathPoint[] _points;

        #region Components

        private SphereCollider _collider;

        #endregion

        #region RuntimeProperties

        private int _direction;
        private int _startPoint;
        private int _endPoint;

        #endregion

        #region Unity

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _direction = 1;
            _startPoint = 0;
            _endPoint = 1;
        }

        private void Update()
        {
            if (_points.Length < 2)
            {
                Debug.LogWarning("You must add at least two path points!");
                enabled = false;
                return;
            }
            
            if (Level.Instance.IsGameRunning == false)
            {
                return;
            }

            Transform();
            UpdatePoint();
            UpdateDangerZone();
        }

        private void OnDrawGizmos()
        {
            if (_points.Length < 2)
            {
                return;
            }

            Gizmos.color = Color.yellow;

            for (var i = 0; i < _points.Length - 1; i++)
            {
                Gizmos.DrawLine(_points[i].Position, _points[i + 1].Position);
            }
        }

        private void OnValidate()
        {
            if (_movementSpeed < 0f)
            {
                _movementSpeed = 0f;
            }

            if (_rotationSpeed < 0f)
            {
                _rotationSpeed = 0f;
            }

            if (_dangerZoneRadius < 0f)
            {
                _dangerZoneRadius = 0f;
            }
        }

        #endregion

        private void Transform()
        {
            var targetRotation = Quaternion.LookRotation(_points[_endPoint].Position - _points[_startPoint].Position, Vector3.up);

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * _rotationSpeed);

            if (Quaternion.Angle(transform.localRotation, targetRotation) <= 0.000001f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _points[_endPoint].Position, Time.deltaTime * _movementSpeed);
            }
        }

        private void UpdatePoint()
        {
            if (Vector3.Distance(transform.localPosition, _points[_endPoint].Position) > 0.000001f)
            {
                return;
            }

            if (_endPoint + 1 == _points.Length)
            {
                _startPoint = _points.Length - 1;
                _endPoint = _startPoint - 1;
                _direction = -1;
            }
            else if (_endPoint - 1 == -1)
            {
                _startPoint = 0;
                _endPoint = 1;
                _direction = 1;
            }
            else
            {
                _startPoint += _direction;
                _endPoint += _direction;
            }
        }

        private void UpdateDangerZone()
        {
            _collider.radius = _dangerZoneRadius;
        }
    }
}
