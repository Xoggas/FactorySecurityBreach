using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MelonJam4.Factory
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class BeatMapWriter : MonoBehaviour
    {
        [SerializeField]
        private BeatMap _beatMap;

        [SerializeField]
        private float _delay;

        #region RuntimeVariables

        private AudioSource _source;
        private float _nextBeat;
        private float _step;

        #endregion

        #region Unity

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _beatMap.Beats = new List<Beat>();
            _step = 60f / _beatMap.Bpm;
            _nextBeat = _beatMap.Offset;
        }

        private void Start()
        {
            _source.clip = _beatMap.Track;
            _source.PlayDelayed(_delay);
        }

        private void Update()
        {
            if (_source.isPlaying == false)
            {
                return;
            }

            var isWrong = Input.GetKey(KeyCode.Space);

            if (_source.time >= _nextBeat)
            {
                _beatMap.Add(_nextBeat, isWrong);
                _nextBeat += _step;
            }

            if (_nextBeat > _beatMap.Track.length)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            #if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
            EditorUtility.SetDirty(_beatMap);
            AssetDatabase.SaveAssetIfDirty(_beatMap);
            #endif
        }

        #endregion
    }
}
