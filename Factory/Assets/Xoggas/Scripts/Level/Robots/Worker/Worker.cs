using System;
using UnityEngine;

namespace MelonJam4.Factory.Worker
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Worker : Robot
    {
        public event Action OnHack;
        
        [SerializeField]
        private Transform _progressBar;

        [SerializeField]
        private CanvasGroup _progressBarGroup;

        [SerializeField]
        private SphereCollider _collider;

        [SerializeField]
        private float _activeZoneRadius = 1f;

        [SerializeField]
        private float _hackingDuration = 4f;

        #region RuntimeVariables

        public bool IsHacked { get; private set; }

        private float _hackingTimer;

        private bool _isBeingHacked;

        #endregion

        #region Unity

        private void Update()
        {
            _collider.radius = _activeZoneRadius;

            UpdateProgressBarVisibility();

            if (IsHacked)
            {
                return;
            }

            UpdateProgressBar();
            UpdateTimer();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (IsHacked)
            {
                _isBeingHacked = false;
                return;
            }

            if (collision.TryGetComponent<Player>(out _))
            {
                _isBeingHacked = true;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (IsHacked)
            {
                _isBeingHacked = false;
                return;
            }

            if (collision.TryGetComponent<Player>(out _))
            {
                _isBeingHacked = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (IsHacked)
            {
                _isBeingHacked = false;
                return;
            }

            if (collision.TryGetComponent<Player>(out _))
            {
                _isBeingHacked = false;
            }
        }

        #endregion

        private void UpdateTimer()
        {
            if (_hackingTimer >= _hackingDuration)
            {
                Debug.Log("You've hacked the robot!");
                IsHacked = true;
                OnHack?.Invoke();
            }

            if (_isBeingHacked)
            {
                _hackingTimer += Time.deltaTime;
            }
            else if (_hackingTimer > 0)
            {
                _hackingTimer -= Time.deltaTime;
            }
            else
            {
                _hackingTimer = 0f;
            }
        }

        private void UpdateProgressBarVisibility()
        {
            var speed = Time.deltaTime * 2f;

            if (_isBeingHacked && _progressBarGroup.alpha < 1f)
            {
                _progressBarGroup.alpha += speed;
            }
            else if (IsHacked || _progressBarGroup.alpha > 0f)
            {
                _progressBarGroup.alpha -= speed;
            }
        }

        private void UpdateProgressBar()
        {
            _progressBar.transform.localScale = new Vector3(_hackingTimer / _hackingDuration, 1f);
        }
    }
}
