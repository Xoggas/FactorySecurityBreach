using UnityEngine;

namespace MelonJam4.Factory
{
    [RequireComponent(typeof(Animation))]
    public sealed class Transition : MonoBehaviour
    {
        [SerializeField]
        private string _inClipName;

        [SerializeField]
        private string _outClipName;

        #region RuntimeVariables

        public bool IsPlaying => _animation.isPlaying;

        private Animation _animation;

        #endregion

        #region Unity

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        #endregion

        public void FadeIn()
        {
            _animation.Play(_inClipName, PlayMode.StopAll);
        }

        public void FadeOut()
        {
            _animation.Play(_outClipName, PlayMode.StopAll);
        }
    }
}
