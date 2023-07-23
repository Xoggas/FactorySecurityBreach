using UnityEngine;

namespace MelonJam4.Factory
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class Press : MonoBehaviour
    {
        [SerializeField]
        private Animation _animation;

        [SerializeField]
        private AnimationClip _clip;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && MiniGame.IsRunning)
            {
                _animation.Play(_clip.name);
                //_source.Play();
            }
        }
    }
}
