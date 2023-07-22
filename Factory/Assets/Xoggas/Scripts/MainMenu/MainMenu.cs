using UnityEngine;

namespace MelonJam4.Factory.MainMenu
{
    public sealed class MainMenu : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                MySceneManager.LoadScene(1);
            }
        }
    }
}
