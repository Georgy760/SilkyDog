using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameManager.Scripts
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        
        public event Action<GameState, GameState> OnGameStateChanged;
        List<AsyncOperation> _loadOperations;
        GameState _currentGameState = GameState.PREGAME;
        
        string _currentLevelName;
        
        public GameState CurrentGameState
        {
            get { return _currentGameState; }
            private set { _currentGameState = value; }
        }

        void Awake()
        {
            OnGameStateChanged?.Invoke(GameState.PREGAME, _currentGameState);
        }
        
        void OnLoadOperationComplete(AsyncOperation ao)
        {
            if (_loadOperations.Contains(ao))
            {
                _loadOperations.Remove(ao);

                if (_loadOperations.Count == 0)
                {
                    UpdateState(GameState.RUNNING);
                }
            }
        }
        void OnUnloadOperationComplete(AsyncOperation ao)
        {
            // Clean up level is necessary, go back to main menu
        }
        void UpdateState(GameState state)
        {
            GameState previousGameState = _currentGameState;
            _currentGameState = state;

            switch (CurrentGameState)
            {
                case GameState.PREGAME:
                    // Initialize any systems that need to be reset
                    Time.timeScale = 1.0f;
                    break;

                case GameState.RUNNING:
                    //  Unlock player, enemies and input in other systems, update tick if you are managing time
                    Time.timeScale = 1.0f;
                    break;

                case GameState.PAUSED:
                    // Pause player, enemies etc, Lock other input in other systems
                    Time.timeScale = 0.0f;
                    break;

                default:
                    break;
            }

            OnGameStateChanged.Invoke(_currentGameState, previousGameState);
        }
        void HandleMainMenuFadeComplete(bool fadeIn)
        {
            if (!fadeIn)
            {
                UnloadLevel(_currentLevelName);
            }
        }
        public void LoadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.LogError("[GameManager] Unable to load level " + levelName);
                return;
            }

            ao.completed += OnLoadOperationComplete;
            _loadOperations.Add(ao);

            _currentLevelName = levelName;
        }
        public void UnloadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
            ao.completed += OnUnloadOperationComplete;
        }
        public void TogglePause()
        {
            UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
        }
        public void RestartGame()
        {
            UpdateState(GameState.PREGAME);
        }
        public void StartGame()
        {
            //LoadLevel("Main");
        }
        public void QuitGame()
        {
            Debug.Log("[GameManager] Quit Game.");

            Application.Quit();
        }


    }
}