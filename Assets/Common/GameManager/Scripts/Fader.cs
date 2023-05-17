using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Common.GameManager.Scripts
{
    
    public class Fader : MonoBehaviour
    {
        [SerializeField] private Animation _mainMenuAnimator;
        [SerializeField] private AnimationClip _fadeOutAnimation;
        [SerializeField] private AnimationClip _fadeInAnimation;

        private IGameManager _gameManager;

        [Inject]
        void Construct(IGameManager manager)
        {
            _gameManager = manager;
            _gameManager.OnGameStateChanged += HandleGameStateChanged;
        }
        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            if (_mainMenuAnimator == null) return;

            if (previousState == GameState.RUNNING && currentState == GameState.FadePhase)
            {
               
                _mainMenuAnimator.clip = _fadeOutAnimation;
                _mainMenuAnimator.Play(); 
                

            }
            if (previousState == GameState.FadePhase && currentState == GameState.RUNNING)
            {
                _mainMenuAnimator.clip = _fadeInAnimation;
                _mainMenuAnimator.Play();
            }
        }
        public void OnFadeInComplete()
        {
            
        }
        public void OnFadeOutComplete()
        {
            _gameManager.HandleFadeComplete(true);
        }
    }
}
