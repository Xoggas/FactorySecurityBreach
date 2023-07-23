using UnityEngine;

namespace MelonJam4.Factory
{
    public sealed class Press : MonoBehaviour
    {
        [SerializeField]
        private Animation _animation;

        [SerializeField]
        private AnimationClip _clip;

        public void Shoot()
        {
            _animation.Play(_clip.name);
        }
    }
}
