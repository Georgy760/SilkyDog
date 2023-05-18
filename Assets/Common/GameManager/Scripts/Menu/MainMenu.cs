using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.GameManager.Scripts.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public Button PlayButton;
        public Button SettingsButton;
        public Button QuitButton;

        [SerializeField] private Animation _mainMenuAnimator;
        [SerializeField] private AnimationClip _fadeOutAnimation;
        [SerializeField] private AnimationClip _fadeInAnimation;

        private IGameManager _gameManager;
        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.OnGameStateChanged += HandleGameStateChanged;

            PlayButton.onClick.AddListener(_gameManager.StartGame);
            SettingsButton.onClick.AddListener(HandleSettingsClick);
            QuitButton.onClick.AddListener(HandleQuitClick);
        }
        private void OnDestroy()
        {
            _gameManager.OnGameStateChanged -= HandleGameStateChanged;
            PlayButton.onClick.RemoveListener(_gameManager.StartGame);
            SettingsButton.onClick.RemoveListener(HandleSettingsClick);
            QuitButton.onClick.RemoveListener(HandleQuitClick);
        }
        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            if (_mainMenuAnimator == null) return;

            if (previousState == GameState.PREGAME && currentState != GameState.PREGAME)
            {
                _mainMenuAnimator.clip = _fadeOutAnimation;
                _mainMenuAnimator.Play();
                OnFadeOutComplete();
            }

            if (previousState != GameState.PREGAME && currentState == GameState.PREGAME)
            {
                _mainMenuAnimator.Stop();
                _mainMenuAnimator.clip = _fadeInAnimation;
                _mainMenuAnimator.Play();
                OnFadeInComplete();
            }
        }
        private void HandleSettingsClick()
        {
            _gameManager.ToggleSettings();
        }

        private void HandleQuitClick()
        {
            _gameManager.QuitGame();
        }

        public void OnFadeOutComplete()
        {
            _gameManager.HandleMainMenuFadeComplete(true);
        }

        public void OnFadeInComplete()
        {
            _gameManager.HandleMainMenuFadeComplete(false);
        }
    }
}