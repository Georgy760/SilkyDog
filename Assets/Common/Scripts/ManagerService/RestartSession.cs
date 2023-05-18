using System;
using Common.GameManager.Scripts;
using UnityEngine;
using Zenject;

namespace Common.Scripts.ManagerService
{
    public class RestartSession : MonoBehaviour
    {
        private ISessionService _sessionService;
        private IGameManager _gameManager;
        [Inject]
        void Construct(ISessionService sessionService, IGameManager gameManager)
        {
            _gameManager = gameManager;
            _sessionService = sessionService;
            _gameManager.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDestroy()
        {
            _gameManager.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            if (currentState != GameState.STARTRUNNING ||
                (previousState != GameState.RESULT && GameState.PAUSED != previousState)) return;
            _sessionService.EndRun();
            _sessionService.RestartGame();
        }

        
    }
}
