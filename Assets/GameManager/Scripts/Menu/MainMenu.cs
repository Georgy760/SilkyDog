using System;
using UnityEngine;
using Zenject;

namespace GameManager.Scripts.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Animation _mainMenuAnimator;
        [SerializeField] private AnimationClip _fadeOutAnimation;
        [SerializeField] private AnimationClip _fadeInAnimation;

        public event Action OnFadeComplete;

        private IGameManager _gameManager;

        [Inject]
        void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        private void Start()
        {
            _gameManager.OnGameStateChanged += HandleGameStateChanged;
        }

        void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            if (_mainMenuAnimator == null)
            {
                return;
            }

            if (previousState == GameState.PREGAME && currentState != GameState.PREGAME)
            {
                //UIManager.Instance.SetDummyCameraActive(false); //TODO ADD UIMANAGER
                _mainMenuAnimator.clip = _fadeOutAnimation;
                _mainMenuAnimator.Play();
            }

            if (previousState != GameState.PREGAME && currentState == GameState.PREGAME)
            {
                _mainMenuAnimator.Stop();
                _mainMenuAnimator.clip = _fadeInAnimation;
                _mainMenuAnimator.Play();
            }
        }
    }
}