using System;
using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    public class Coin : MonoBehaviour
    {
        public static event Action OnPickUpCoin;

        private IAudioService _audioService;

        [Inject]
        void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player")) {
                _audioService.PlaySound(AudioType.COIN);
                OnPickUpCoin?.Invoke();
            }
            Destroy(gameObject);
        }
    }
}
