using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.GameManager.Scripts.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button ResumeButton;
        [SerializeField] private Button QuitButton;
        [SerializeField] private Button RestartButton;
        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.OnGameStateChanged += HandleGameStateChanged;
            ResumeButton.onClick.AddListener(HandleResumeClick);
            QuitButton.onClick.AddListener(HandleQuitClick);
            RestartButton.onClick.AddListener(HandleRestartClick);
        }

        private void OnDestroy()
        {
            _gameManager.OnGameStateChanged -= HandleGameStateChanged;
            ResumeButton.onClick.RemoveListener(HandleResumeClick);
            QuitButton.onClick.RemoveListener(HandleQuitClick);
            RestartButton.onClick.RemoveListener(HandleRestartClick);
        }

        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            switch (currentState)
            {
                case GameState.PAUSED:
                    gameObject.SetActive(true);
                    break;

                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private void HandleResumeClick()
        {
            _gameManager.TogglePause();
        }

        private void HandleQuitClick()
        {
            _gameManager.QuitLevel();
        }

        private void HandleRestartClick()
        {
            //TODO Add restart feature
        }
    }
}