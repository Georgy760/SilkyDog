using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    public class BackgroundMusic : MonoBehaviour
    {
        private IAudioService _audioService;

        [Inject]
        void Construct(IAudioService audioService)
        {
            _audioService = audioService;
            _audioService.PlaySound(AudioType.BACKGROUND);
        }
    }
}