using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace MelonJam4.Factory
{
    [RequireComponent(typeof(AudioSource))]
    public class TextWriter : MonoBehaviour
    {
        [SerializeField]
        private float _delay;

        [SerializeField]
        private AudioClip _sfx;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private int _nextScene;
        
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(Writing());
        }

        private void Update()
        {
            _source.volume = GameSettings.SfxVolume;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                MySceneManager.LoadScene(_nextScene);
            }
        }

        private IEnumerator Writing()
        {
            var stringBuilder = new StringBuilder();
            var text = _text.text;
            var timer = new WaitForSeconds(_delay);

            _text.text = "";

            foreach (var t in text)
            {
                stringBuilder.Append(t);
                _text.text = stringBuilder.ToString();
                _source.PlayOneShot(_sfx);
                yield return timer;
            }
        }
    }
}
