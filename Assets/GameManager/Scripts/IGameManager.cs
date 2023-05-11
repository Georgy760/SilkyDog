using System;

namespace GameManager.Scripts
{
    public interface IGameManager
    {
        event Action<GameState, GameState> OnGameStateChanged;
    }
}