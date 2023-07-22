using System;
using UnityEngine;

namespace MelonJam4.Factory
{
    public static class GameSettings
    {
        public static event Action OnSettingsUpdated;
        
        public static float MasterVolume
        {
            get => PlayerPrefs.GetFloat("masterVolume", 1f);
            set
            {
                PlayerPrefs.SetFloat("masterVolume", value);
                OnSettingsUpdated?.Invoke();
            }
        }

        public static float SfxVolume
        {
            get => PlayerPrefs.GetFloat("sfxVolume", 1f);
            set
            {
                PlayerPrefs.SetFloat("sfxVolume", value);
                OnSettingsUpdated?.Invoke();
            }
        }
    }
}
