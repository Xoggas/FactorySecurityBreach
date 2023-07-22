using System;
using UnityEngine;
using UnityEngine.UI;

namespace MelonJam4.Factory
{
    public sealed class GameOverPopup : PausePopup
    {
        public event Action OnRestartPressed;
        
        [SerializeField]
        private Button _restartButton;

        #region Unity

        protected override void Awake()
        {
            base.Awake();
            _restartButton.onClick.AddListener(OnRestart);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _restartButton.onClick.RemoveListener(OnRestart);
        }

        #endregion

        private void OnRestart()
        {
            OnRestartPressed?.Invoke();
        }
    }
}
