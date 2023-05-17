using System;
using UnityEngine;

namespace Common.Scripts.AudioService
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private AudioSource _backgroundMusic;
        [SerializeField] private AudioSource _coin;
        [SerializeField] private AudioSource _jump;
        private event Action<AudioType> OnPlaySound;
        private void Awake()
        {
            OnPlaySound += PlaySoundOfType;
            if (!_backgroundMusic.isPlaying)
                _backgroundMusic.Play();
        }

        private void OnDestroy()
        {
            OnPlaySound -= PlaySoundOfType;
        }

        private void PlaySoundOfType(AudioType type)
        {
            switch (type)
            {
                case AudioType.COIN:
                    _coin.Play();
                    break;
                case AudioType.JUMP:
                    _jump.Play();
                    break;
                case AudioType.BACKGROUND:
                    if(!_backgroundMusic.isPlaying) 
                        _backgroundMusic.Play();
                    break;
            }
        }

        public void PlaySound(AudioType type)
        {
            OnPlaySound?.Invoke(type);
        }
    }
}