using UnityEngine;
using UnityEngine.SceneManagement;

namespace MelonJam4.Factory
{
    public sealed class Level : MonoBehaviour
    {
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
            Instance = this;
            _sceneId = SceneManager.GetActiveScene().buildIndex;
            _player.OnCompromised += OnGameOver;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Restart();
            }
        }

        private void OnDestroy()
        {
            Instance = null;
            _player.OnCompromised -= OnGameOver;
        }

        private void OnEnable()
        {
            _player.OnCompromised += OnGameOver;
        }

        private void OnDisable()
        {
            _player.OnCompromised -= OnGameOver;
        }

        #endregion

        private void Restart()
        {
            MySceneManager.LoadScene(_sceneId);
        }

        private void OnQuit()
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
        }
    }
}
