using System.Collections;
using TMPro;
using UnityEngine;

namespace MelonJam4.Factory
{
    [RequireComponent(typeof(TextMeshPro))]
    public sealed class HitText : MonoBehaviour
    {
        [SerializeField]
        private float _distance;

        [SerializeField]
        private float _lifeTime = 0.5f;

        [SerializeField]
        [Range(0f, 1f)]
        private float _speed;

        #region RuntimeVariables

        private TextMeshPro _textMesh;

        private Vector3 _targetPosition;

        #endregion

        #region Unity

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshPro>();
        }

        private void Start()
        {
            GenerateDirection();
            StartCoroutine(Living());
        }

        private void Update()
        {
            var textTransform = transform;
            var position = textTransform.position;
            position += (_targetPosition - position) * _speed;
            textTransform.position = position;
        }

        #endregion

        public void SetText(string text)
        {
            _textMesh.text = text;
        }

        private void GenerateDirection()
        {
            var randomVector = Random.insideUnitCircle;
            _targetPosition = transform.position + Vector3.up * _distance + new Vector3(randomVector.x, 0, randomVector.y) * _distance * 0.5f;
        }

        private IEnumerator Living()
        {
            yield return new WaitForSeconds(_lifeTime);

            for (var alpha = 1f; alpha > 0; alpha -= 0.1f)
            {
                _textMesh.alpha = alpha;
                yield return new WaitForEndOfFrame();
            }

            Destroy(gameObject);
        }
    }
}
