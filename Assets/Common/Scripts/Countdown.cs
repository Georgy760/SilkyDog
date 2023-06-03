using System;
using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    [RequireComponent(typeof(Animation))]
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private AnimationClip _countdownClip;
        private Animation _animation;
        private ISessionService _sessionService;
        [Inject]
        void Construct(ISessionService sessionService)
        {
            _sessionService = sessionService;
            _animation = GetComponent<Animation>();
            _animation.clip = _countdownClip;
            _sessionService.OnRestartSession += StartCountdown;
            
        }

        private void OnDestroy()
        {
            _sessionService.OnRestartSession -= StartCountdown;
        }

        private void Start()
        {
            StartCountdown();
        }

        private void StartCountdown()
        {
            _animation.Play();
        }
    }
}
