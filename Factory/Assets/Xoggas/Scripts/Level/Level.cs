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

        [SerializeField]
        private MiniGame _miniGame;

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
            _pausePopup.OnOpen += Lock;
            _pausePopup.OnClose += Unlock;
            _pausePopup.OnQuitPressed += Quit;
            _gameOverPopup.OnRestartPressed += Restart;
            _gameOverPopup.OnQuitPressed += Quit;
        }

        private void OnDestroy()
        {
            Instance = null;
            _player.OnCompromised -= OnGameOver;
            _pausePopup.OnQuitPressed -= Quit;
            _pausePopup.OnOpen -= Lock;
            _pausePopup.OnClose -= Unlock;
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

        public void Lock()
        {
            IsGameRunning = false;
        }

        public void Unlock()
        {
            IsGameRunning = true;
        }
        
        private void Restart()
        {
            MySceneManager.LoadScene(_sceneId);
        }

        private void Quit()
        {
            MySceneManager.LoadScene(0);
        }

        private void OnGameOver()
        {
            IsGameRunning = false;
            _gameOverPopup.Open();
        }
    }
}
