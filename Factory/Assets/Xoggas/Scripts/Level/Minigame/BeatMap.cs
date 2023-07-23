using System.Collections.Generic;
using UnityEngine;

namespace MelonJam4.Factory
{
    [CreateAssetMenu(fileName = "BeatMap", menuName = "Factory/New BeatMap", order = 0)]
    public sealed class BeatMap : ScriptableObject
    {
        public AudioClip Track;
        public string TrackName;
        public string Duration;
        public int Bpm;
        public float Offset;
        public List<Beat> Beats;

        public void Add(float time, bool isWrong)
        {
            Beats.Add(new Beat { Time = time, IsWrong = isWrong });
        }
    }
}
