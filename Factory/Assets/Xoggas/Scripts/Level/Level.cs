using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MelonJam4.Factory
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField]
        private PausePopup _pausePopup;

        [SerializeField]
        private GameOverPopup _gameOverPopup;

        [SerializeField]
        private Player _player;

        #region RuntimeVariables

        public static Level Instance { get; private set; }

        public bool IsGameRunning { get; private set; } = true;

        private int _sceneId;

        #endregion

        #region Unity

        private void Awake()
        {
            if (_pausePopup == false || _gameOverPopup == false || _player == false)
            {
                enabled = false;
                throw new Exception($"Missing components in {nameof(Level)} script!");
            }

            Instance = this;
            _sceneId = SceneManager.GetActiveScene().buildIndex;
            _player.OnCompromised += OnGameOver;
            _pausePopup.OnOpen += OnGamePause;
            _pausePopup.OnClose += OnGameResume;
            _pausePopup.OnQuitPressed += Quit;
            _gameOverPopup.OnRestartPressed += Restart;
            _gameOverPopup.OnQuitPressed += Quit;
        }

        private void OnDestroy()
        {
            Instance = null;
            _player.OnCompromised -= OnGameOver;
            _pausePopup.OnQuitPressed -= Quit;
            _pausePopup.OnOpen -= OnGamePause;
            _pausePopup.OnClose -= OnGameResume;
            _gameOverPopup.OnRestartPressed -= Restart;
            _gameOverPopup.OnQuitPressed -= Quit;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && IsGameRunning && !_pausePopup.IsOpened)
            {
                _pausePopup.Open();   
            }
        }

        #endregion

        private void Restart()
        {
            MySceneManager.LoadScene(_sceneId);
        }

        private void Quit()
        {
            MySceneManager.LoadScene(0);
        }

        private void OnGamePause()
        {
            IsGameRunning = false;
        }

        private void OnGameResume()
        {
            IsGameRunning = true;
        }

        private void OnGameOver()
        {
            IsGameRunning = false;
            _gameOverPopup.Open();
        }
    }
}
