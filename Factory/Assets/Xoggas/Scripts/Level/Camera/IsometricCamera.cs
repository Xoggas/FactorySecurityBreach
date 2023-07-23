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
        private CameraTarget _target;

        [Header("Camera Settings")]
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

            var targetPosition = _target.transform.position + Vector3.up * _heightOffset;
            
            if(Vector3.Distance(_container.position, targetPosition) >= 0.01f)
            {
                _container.position = targetPosition;
            }

            _camera.orthographicSize = _target.Zoom;
            _container.localEulerAngles = new Vector3(_angle, _angle + _rotation, 0);
        }
    }
}
