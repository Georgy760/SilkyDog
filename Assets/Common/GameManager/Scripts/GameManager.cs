using Common.Scripts.Legacy;
using Common.Scripts.ManagerService;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace Common.GameManager.Scripts
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public event Action<bool> ShowMainMenu;
        public event Action<bool> OnMainMenuFadeComplete;
        public event Action<GameState, GameState> OnGameStateChanged;

        private string _currentLevelName;
        private List<AsyncOperation> _loadOperations;
        private GameState CurrentGameState { get; set; } = GameState.PREGAME;
        private PlayerActions _playerActions;
      


        private void Awake()
        {
            _currentLevelName = SceneManager.GetActiveScene().name;
            
            _playerActions = new PlayerActions();
            _playerActions.Menu.Enable();
            _playerActions.Menu.EscTap.performed += EscTap;
            OnMainMenuFadeComplete += HandleMainMenuFadeComplete;
            OnGameStateChanged?.Invoke(GameState.PREGAME, CurrentGameState);
            MovingObj.OnDeath += ResultLevel;
        }
        
        private void Start()
        {
            _loadOperations = new List<AsyncOperation>();
        }
        private void OnDestroy()
        {
            _playerActions.Menu.EscTap.performed -= EscTap;
            OnMainMenuFadeComplete -= HandleMainMenuFadeComplete;
        }

        public void HandleMainMenuFadeComplete(bool fadeIn)
        {
            ShowMainMenu?.Invoke(!fadeIn);
            if (!fadeIn)
                if (SceneManager.GetSceneByName(_currentLevelName).isLoaded)
                    UnloadLevel(_currentLevelName);
        }
        public void ToggleSettings()
        {
            //TODO Add settings feature
        }
        public void StartGame()
        {
            LoadLevel("LinearLevel");
        }
        public void TogglePause()
        {
            UpdateState(CurrentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
        }
        public void RestartGame()
        {
            UpdateState(GameState.STARTRUNNING);
        }
        public void ResultLevel()
        {
            UpdateState(GameState.RESULT);
        }
        public void QuitLevel()
        {
            UpdateState(GameState.PREGAME);
        }
        private void EscTap(InputAction.CallbackContext obj)
        {
            if (CurrentGameState == GameState.PREGAME) return;
            TogglePause();
            
        }
        public void QuitGame()
        {
            Debug.Log("[GameManager] Quit Game.");

            Application.Quit();
        }
        public void OnFadeComplete(bool fadeIn)
        {
            OnMainMenuFadeComplete?.Invoke(fadeIn);
        }
        private void OnLoadOperationComplete(AsyncOperation ao)
        {
            if (_loadOperations.Contains(ao))
            {
                _loadOperations.Remove(ao);

                if (_loadOperations.Count == 0) UpdateState(GameState.RUNNING);
            }
        }
        private void OnUnloadOperationComplete(AsyncOperation ao)
        {
            //TODO Clean up level is necessary, go back to main menu 
        }
        private void UpdateState(GameState state)
        {
            var previousGameState = CurrentGameState;
            CurrentGameState = state;

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
                case GameState.STARTRUNNING:
                    Time.timeScale = 1.0f;
                    break;
                case GameState.PAUSED:
                    // Pause player, enemies etc, Lock other input in other systems
                    Time.timeScale = 0.0f;
                    break;
                case GameState.RESULT:
                    // Pause player, enemies etc, Lock other input in other systems
                    Time.timeScale = 0.0f;
                    break;
            }

            OnGameStateChanged?.Invoke(CurrentGameState, previousGameState);
        }
        private void LoadLevel(string levelName)
        {
            var ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.LogError("[GameManager] Unable to load level " + levelName);
                return;
            }

            ao.completed += OnLoadOperationComplete;
            _loadOperations.Add(ao);

            _currentLevelName = levelName;
        }
        private void UnloadLevel(string levelName)
        {
            var ao = SceneManager.UnloadSceneAsync(levelName);
            ao.completed += OnUnloadOperationComplete;
        }
    }
}