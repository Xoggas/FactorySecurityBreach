using System;
using System.Collections;
using UnityEngine;

namespace MelonJam4.Factory
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class MiniGame : MonoBehaviour
    {
        public event Action OnMiniGameEnd;

        [SerializeField]
        private Worker _targetWorker;

        [SerializeField]
        private PathPoint _startPoint;

        [SerializeField]
        private PathPoint _endPoint;

        [SerializeField]
        [Range(0f, 1f)]
        private float _targetPoint;

        [SerializeField]
        [Range(0f, 10f)]
        private float _step;

        [SerializeField]
        private MiniGameItem _item;

        [SerializeField]
        private BeatMap _beatMap;

        [SerializeField]
        private IsometricCamera _camera;

        [SerializeField]
        private CameraTarget _cameraTarget;

        [SerializeField]
        private PathPoint _targetView;

        [SerializeField]
        private float _targetZoom;

        #region RuntimeVariables

        private AudioSource _source;
        private float _lastZoom;

        #endregion

        #region Unity

        private void Awake()
        {
            if (_beatMap == false)
            {
                enabled = false;
                throw new Exception("A beat map should be attached to the mini game script!");
            }

            _source = GetComponent<AudioSource>();

            if (_targetWorker == false)
            {
                enabled = false;
                throw new Exception("A worker should be attached to the mini game script!");
            }

            _targetWorker.OnHack += StartMiniGame;
        }

        private void OnDrawGizmos()
        {
            if (_startPoint == false || _endPoint == false)
            {
                return;
            }

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(_startPoint.Position, _endPoint.Position);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Vector3.Lerp(_startPoint.Position, _endPoint.Position, _targetPoint), 2f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(Vector3.LerpUnclamped(_startPoint.Position, _endPoint.Position, _targetPoint - _step), 2f);
        }

        private void OnDestroy()
        {
            _targetWorker.OnHack -= StartMiniGame;
        }

        #endregion

        private void StartMiniGame()
        {
            StartCoroutine(MiniGameStarting());
        }

        private IEnumerator WaitingForEndOfTrack()
        {
            yield return new WaitWhile(() => _source.isPlaying);

            SwitchCameraBack();
            OnMiniGameEnd?.Invoke();

            yield return new WaitForSeconds(0.2f);

            Level.Instance.Unlock();
            _cameraTarget.FollowPlayer = true;
        }

        private IEnumerator MiniGameStarting()
        {
            Level.Instance.Lock();
            SwitchCameraToView();

            yield return new WaitForSeconds(0.2f);

            PlaySound();
            SpawnCubes();
            StartCoroutine(WaitingForEndOfTrack());
        }

        private void SwitchCameraToView()
        {
            _lastZoom = _cameraTarget.Zoom;
            _cameraTarget.TargetPosition = _targetView.Position;
            _cameraTarget.TargetZoom = _targetZoom;
            _cameraTarget.FollowPlayer = false;
        }

        private void SwitchCameraBack()
        {
            _cameraTarget.TargetPosition = Player.Instance.transform.position;
            _cameraTarget.TargetZoom = _lastZoom;
        }

        private void PlaySound()
        {
            _source.clip = _beatMap.Track;
            _source.Play();
        }

        private void SpawnCubes()
        {
            var targetPoint = Vector3.Lerp(_startPoint.Position, _endPoint.Position, _targetPoint);
            var startPoint = Vector3.LerpUnclamped(_startPoint.Position, _endPoint.Position, _targetPoint - _step);
            var step = targetPoint - startPoint;

            for (var i = 1; i < _beatMap.Beats.Count; i++)
            {
                var item = Instantiate(_item, targetPoint - step * i, Quaternion.identity);
                item.Source = _source;
                item.Step = step;
                item.Target = targetPoint;
                item.Start = _startPoint.Position;
                item.End = _endPoint.Position;
                item.CurrentBeat = _beatMap.Beats[i].Time;
                item.IsWrong = _beatMap.Beats[i].IsWrong;
            }
        }
    }
}
