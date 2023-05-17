using System;
using UnityEngine.Audio;

namespace Common.Scripts
{
    public interface IAudioService
    {
        public void PlaySound(AudioType type);
    }
}