using System;
using MelonJam4.Factory.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace MelonJam4.Factory
{
    public class PausePopup : OptionsPopup
    {
        public event Action OnQuitPressed;
        
        [SerializeField]
        private Button _quitButton;

        #region Unity

        protected override void Awake()
        {
            base.Awake();
            _quitButton.onClick.AddListener(OnQuit);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _quitButton.onClick.RemoveListener(OnQuit);
        }

        #endregion

        private void OnQuit()
        {
            OnQuitPressed?.Invoke();
        }
    }
}
