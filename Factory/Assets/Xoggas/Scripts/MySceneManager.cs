using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MelonJam4.Factory
{
    public sealed class MySceneManager : MonoBehaviour
    {
        private static MySceneManager s_instance;

        [SerializeField]
        private Transition _transition;

        public static void LoadScene(int sceneIdx)
        {
            if (s_instance == null)
            {
                Instantiate(Resources.Load<MySceneManager>("Static/SceneManager"));
            }

            s_instance.StartCoroutine(s_instance.WaitForTransitionEndAndLoadScene(sceneIdx));
        }

        private void Awake()
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        private IEnumerator WaitForTransitionEndAndLoadScene(int sceneIdx)
        {
            _transition.FadeIn();

            yield return new WaitWhile(() => _transition.IsPlaying);

            yield return SceneManager.LoadSceneAsync(sceneIdx);

            _transition.FadeOut();
        }
    }
}
