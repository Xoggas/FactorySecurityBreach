using UnityEngine;
using UnityEngine.UI;

namespace MelonJam4.Factory
{
    public sealed class StealthBar : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        [SerializeField]
        private float _barWidth;

        [SerializeField]
        private RectTransform _leftBarTransform;

        [SerializeField]
        private Image _leftBarImage;

        [SerializeField]
        private RectTransform _rightBarTransform;

        [SerializeField]
        private Image _rightBarImage;

        [SerializeField]
        private Gradient _gradient;

        #region Unity

        private void Awake()
        {
            _player.OnCompromiseTimerUpdate += OnTimerUpdate;
        }

        private void OnDestroy()
        {
            _player.OnCompromiseTimerUpdate -= OnTimerUpdate;
        }

        #endregion

        private void OnTimerUpdate(float time, float timeLimit)
        {
            SetWidth(_leftBarTransform, _barWidth * time / timeLimit);
            UpdateColor(_leftBarImage, time / timeLimit);
            SetWidth(_rightBarTransform, _barWidth * time / timeLimit);
            UpdateColor(_rightBarImage, time / timeLimit);
        }

        private void SetWidth(RectTransform rectTransform, float width)
        {
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }

        private void UpdateColor(Image image, float progress)
        {
            image.color = _gradient.Evaluate(Mathf.Clamp01(progress));
        }
    }
}
