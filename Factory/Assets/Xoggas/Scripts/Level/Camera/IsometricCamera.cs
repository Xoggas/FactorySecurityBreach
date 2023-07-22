using UnityEngine;
using UnityEngine.Serialization;

namespace MelonJam4.Factory
{
    [ExecuteAlways]
    public sealed class IsometricCamera : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private Camera _camera;

        [FormerlySerializedAs("_camera")]
        [SerializeField]
        private Transform _cameraTransform;

        [SerializeField]
        private Transform _target;

        [Header("Camera Settings")]
        [SerializeField]
        private float _distance;

        [SerializeField]
        private float _angle;

        [SerializeField]
        private float _heightOffset;

        [SerializeField]
        private float _rotation;

        private void LateUpdate()
        {
            if (_container == false || _cameraTransform == false || _target == false)
            {
                return;
            }

            _camera.orthographicSize = _distance;
            _cameraTransform.localPosition = new Vector3(0, 0, -_distance);
            _container.localEulerAngles = new Vector3(_angle, _angle + _rotation, 0);
            _container.position = _target.position + Vector3.up * _heightOffset;
        }

        private void OnValidate()
        {
            if (_distance < 0)
            {
                _distance = 0;
            }
        }
    }
}
