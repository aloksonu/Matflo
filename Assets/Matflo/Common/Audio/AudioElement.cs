using UnityEngine;

namespace Matflo.Common.Audio
{
    [System.Serializable]
    public class AudioElement
    {
        public enum SoundType
        {
            SoundFx,
            BackgroundMusic
           
        }

        public AudioName audioName;
        public AudioClip clip;
        public SoundType soundType = SoundType.SoundFx;

        [HideInInspector] public AudioSource[] audioSources;

        [Range(0, 1)] public float volume = 1;
        [Range(-3, 3)] public float pitch = 1;

        public bool loop;

        public bool playOnAwake;

        public bool rapidSingleShotSound;
    }
}