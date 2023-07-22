using UnityEngine;
using UnityEngine.UI;

namespace MelonJam4.Factory.MainMenu
{
    public sealed class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Animation _animation;

        [SerializeField]
        private AnimationClip _openClip;

        [SerializeField]
        private AnimationClip _closeClip;

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _optionsButton;

        [SerializeField]
        private Button _quitButton;

        [SerializeField]
        private OptionsPopup _options;

        #region Unity

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayPressed);
            _optionsButton.onClick.AddListener(OnOptionsPressed);
            _quitButton.onClick.AddListener(OnQuitPressed);
            _options.OnClose += Show;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                MySceneManager.LoadScene(1);
            }
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayPressed);
            _optionsButton.onClick.RemoveListener(OnOptionsPressed);
            _quitButton.onClick.RemoveListener(OnQuitPressed);
            _options.OnClose -= Show;
        }

        #endregion

        private void OnPlayPressed()
        {
            MySceneManager.LoadScene(1);
        }

        private void OnOptionsPressed()
        {
            Hide();
            _options.Open();
        }

        private void OnQuitPressed()
        {
            Application.Quit();
        }

        private void Hide()
        {
            _animation.Play(_closeClip.name);
        }

        private void Show()
        {
            _animation.Play(_openClip.name);
        }
    }
}
