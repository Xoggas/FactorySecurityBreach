using System;
using UnityEngine;
using UnityEngine.UI;

namespace MelonJam4.Factory.MainMenu
{
    public class OptionsPopup : MonoBehaviour
    {
        public event Action OnOpen;
        public event Action OnClose;
        public bool IsOpened => _isOpened;

        [SerializeField]
        private Animation _animation;

        [SerializeField]
        private AnimationClip _openClip;

        [SerializeField]
        private AnimationClip _closeClip;

        [SerializeField]
        private Slider _masterSlider;

        [SerializeField]
        private Slider _sfxSlider;

        [SerializeField]
        private Button _closeButton;

        #region RuntimeVariables

        private bool _isOpened;

        #endregion

        #region Unity

        protected virtual void Awake()
        {
            _masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            _sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
            _closeButton.onClick.AddListener(Close);
            _masterSlider.value = GameSettings.MasterVolume;
            _sfxSlider.value = GameSettings.SfxVolume;
        }

        protected virtual void OnDestroy()
        {
            _masterSlider.onValueChanged.RemoveListener(OnMasterVolumeChanged);
            _sfxSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
            _closeButton.onClick.RemoveListener(Close);
        }

        #endregion

        public void Open()
        {
            _isOpened = true;
            _animation.Play(_openClip.name);

            OnOpen?.Invoke();
        }

        private void Close()
        {
            _isOpened = false;
            _animation.Play(_closeClip.name);

            OnClose?.Invoke();
        }

        private void OnMasterVolumeChanged(float value)
        {
            GameSettings.MasterVolume = value;
        }

        private void OnSfxVolumeChanged(float value)
        {
            GameSettings.SfxVolume = value;
        }
    }
}
