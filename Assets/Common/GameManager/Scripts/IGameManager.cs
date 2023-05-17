using System;

namespace Common.GameManager.Scripts
{
    public interface IGameManager
    {
        event Action<bool> ShowMainMenu;
        event Action<GameState, GameState> OnGameStateChanged;
        void TogglePause();
        void QuitLevel();
        void QuitGame();
        void RestartGame();
        void ResultLevel();
        void StartGame();
        void HandleMainMenuFadeComplete(bool fadeIn);
        void ToggleSettings();
    }
}