using System;
using UnityEngine;
using UnityEngine.Audio;

namespace MelonJam4.Factory
{
    public static class GameSettings
    {
        public static event Action OnSettingsUpdated;

        private static AudioMixer s_mixer;
        
        public static AudioMixer Mixer
        {
            get
            {
                if (s_mixer == false)
                {
                    s_mixer = Resources.Load<AudioMixer>("Static/Mixer");
                }

                return s_mixer;
            }
        }

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
