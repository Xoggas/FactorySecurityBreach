using UnityEngine;

namespace MelonJam4.Factory
{
    public class PathPoint : MonoBehaviour
    {
        [SerializeField]
        private Color _gizmoColor = Color.cyan;

        [SerializeField]
        private float _gizmoRadius = 1f;

        public Vector3 Position => transform.localPosition;
        
        #region Unity

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(Position, _gizmoRadius);
        }

        #endregion
    }
}
