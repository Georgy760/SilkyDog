using System;

namespace Common.GameManager.Scripts
{
    public interface IGameManager
    {
        event Action<bool> ShowMainMenu;
        event Action<GameState, GameState> OnGameStateChanged;
        event Action<bool> OnFadeCompleteLevelChange;
        event Action OnToggleSetting;
        void TogglePause();
        void QuitLevel();
        void QuitGame();
        void RestartGame();
        void ResultLevel();
        void StartGame();
        void EndLevelChange();
        void HandleMainMenuFadeComplete(bool fadeIn);
        void HandleFadeComplete(bool fadeIn);
        void ToggleSettings();
    }
}