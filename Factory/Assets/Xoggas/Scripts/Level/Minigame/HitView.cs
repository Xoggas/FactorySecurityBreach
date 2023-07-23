using System;
using UnityEngine;
using UnityEngine.Events;

namespace MelonJam4.Factory
{
    public sealed class HitView : MonoBehaviour
    {
        public UnityEvent OnHitEvent;
        
        [SerializeField]
        private float _height = 1f;

        [SerializeField]
        private HitText _textPrefab;

        #region RuntimeVartiables

        public static HitView Instance { get; private set; }

        #endregion

        #region Unity

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        #endregion

        public void OnHit(HitType type)
        {
            CreateText(type);
            OnHitEvent?.Invoke();
        }
        
        private void CreateText(HitType type)
        {
            var text = type switch
            {
                HitType.Perfect => "Perfect!",
                HitType.Good => "Good!",
                HitType.Bad => "Bad!",
                HitType.Miss => "Miss!",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var hitText = Instantiate(_textPrefab, transform.position + Vector3.up * _height, Quaternion.Euler(new Vector3(45, 45)));
            hitText.SetText(text);
        }
    }
}
