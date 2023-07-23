using UnityEngine;

namespace MelonJam4.Factory
{
    public sealed class CameraTarget : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        [SerializeField]
        private float _speed;

        #region RuntimeVariables

        public bool FollowPlayer = true;
        public Vector3 TargetPosition;
        public float TargetZoom = 50f;
        public float Zoom = 50f;

        #endregion

        #region Unity

        private void LateUpdate()
        {
            if (FollowPlayer)
            {
                transform.position = _player.transform.position;
                Zoom = TargetZoom;
            }
            else
            {
                var t = transform;
                var position = t.position;
                position += (TargetPosition - position) * _speed;
                t.position = position;
                Zoom += (TargetZoom - Zoom) * _speed;
            }
        }

        #endregion
    }
}
