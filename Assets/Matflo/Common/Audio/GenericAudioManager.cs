using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;
using Logger = Utilities.Logger;

namespace Matflo.Common.Audio
{
    public class GenericAudioManager : PersistentMonoSingleton<GenericAudioManager>
    {
        private const string MasterVolume = "MasterVolume";
        private const string SfxVolume = "SfxVolume";
        private const string BackgroundMusicVolume = "BackgroundMusicVolume";
        [Header("Configuration")]
        [SerializeField]
        [Range(-80, 20)]
        private float maxVolume = 0f;

        [SerializeField] [Range(-80, 20)] private float minVolume = -80f;

        [Header("References")]
        [SerializeField]
        private AudioMixer masterMixer;

        [SerializeField] private AudioMixerGroup sfxMixer;

        [SerializeField] private AudioMixerGroup backgroundMusicMixer;
        [SerializeField] private AudioData audioDataAsset;
        private bool _isMute = false;
        private float _currentMasterVolume;
        private float _currentBgmVolume;

        private  readonly Dictionary<AudioName, AudioElement> _audioDb = new Dictionary<AudioName, AudioElement>();

        void Start()
        {
            foreach (AudioElement element in audioDataAsset.data)
            {
                _audioDb[element.audioName] = element;
            }

            foreach (AudioElement audioElement in _audioDb.Values)
            {
                audioElement.audioSources = new AudioSource[1];

                for (int i = 0; i < audioElement.audioSources.Length; i++)
                {
                    audioElement.audioSources[i] = gameObject.AddComponent<AudioSource>();
                    audioElement.audioSources[i].spatialBlend = 0;
                    audioElement.audioSources[i].clip = audioElement.clip;
                    audioElement.audioSources[i].volume = audioElement.volume;
                    audioElement.audioSources[i].pitch = audioElement.pitch;
                    audioElement.audioSources[i].playOnAwake = audioElement.playOnAwake;
                    audioElement.audioSources[i].loop = audioElement.loop;

                    // Assign Mixer Group based on sound type
                    switch (audioElement.soundType)
                    {
                        case AudioElement.SoundType.BackgroundMusic:
                            audioElement.audioSources[i].outputAudioMixerGroup = backgroundMusicMixer;
                            break;
                        case AudioElement.SoundType.SoundFx:
                            audioElement.audioSources[i].outputAudioMixerGroup = sfxMixer;
                            break;
                    }
                }

                if (audioElement.playOnAwake)
                {
                    PlaySound(audioElement.audioName);
                }
            }

            SetSfxVolume(maxVolume);
            SetMasterVolume(maxVolume);
            SetMusicVolume(maxVolume);
        }

        public void PlaySound(AudioName audioName)
        {
            if (_audioDb.ContainsKey(audioName))
            {
                AudioElement audioElement = _audioDb[audioName];
                foreach (AudioSource audioSource in audioElement.audioSources)
                {
                    if (audioElement.rapidSingleShotSound)
                        audioSource.PlayOneShot(audioSource.clip);
                    else
                        audioSource.Play();
                }
            }
            else
            {
                Logger.LogWarningFormat("No Entry for <b>{0}</b> in AudioDataAsset named <b>{1}</b> ",
                    audioName.ToString(), audioDataAsset.name);
            }
        }

        public void StopSound(AudioName audioName)
        {
            if (_audioDb.ContainsKey(audioName))
            {
                AudioElement audioElement = _audioDb[audioName];
                foreach (AudioSource audioSource in audioElement.audioSources)
                {
                    audioSource.Stop();
                }
            }
            else
            {
                Logger.LogErrorFormat("No Entry for <b>{0}</b> in AudioDataAsset named <b>{1}</b> ",
                    audioName.ToString(), audioDataAsset.name);
            }
        }

        private void SetSfxVolume(float sfxVol)
        {
            masterMixer.SetFloat(SfxVolume, sfxVol);
        }

        private void SetMusicVolume(float bgmVol)
        {
            _currentBgmVolume = bgmVol;
            masterMixer.SetFloat(BackgroundMusicVolume, _currentBgmVolume);
        }

        private void SetMasterVolume(float masterVol)
        {
            _currentMasterVolume = masterVol;
            masterMixer.SetFloat(MasterVolume, _currentMasterVolume);
        }

        public void ToggleMute()
        {
            _isMute = !_isMute;

            if (_isMute)
            {
                masterMixer.SetFloat(MasterVolume, minVolume);
                masterMixer.SetFloat(BackgroundMusicVolume, minVolume);
            }
            else
            {
                masterMixer.SetFloat(MasterVolume, _currentMasterVolume);
                masterMixer.SetFloat(BackgroundMusicVolume, _currentBgmVolume);
            }
        }
        public float GetAudioLength(AudioName uiAudioName)
        {
            float audiolenght=0;
            if (_audioDb.ContainsKey(uiAudioName))
            {
                AudioElement audioElement = _audioDb[uiAudioName];
                foreach (AudioSource audioSource in audioElement.audioSources)
                {
                    audiolenght = audioSource.clip.length;
                   
                }
            }
            else
            {
                Logger.LogErrorFormat("No Entry for <b>{0}</b> in AudioDataAsset named <b>{1}</b> ",
                    uiAudioName.ToString(), audioDataAsset.name);
               
            }
            return audiolenght;
        }

        internal void StopAllSounds()
        {
            foreach (AudioElement audioElement in audioDataAsset.data)
            {
                foreach (AudioSource audioSource in audioElement.audioSources)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}