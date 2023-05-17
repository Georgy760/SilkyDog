using UnityEngine.UI;
using Zenject;

namespace Common.GameManager.Scripts.Menu
{
    public class SettingsTab
    {
        public Button _close;

        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _close.onClick.AddListener(_gameManager.TogglePause);
        }
    }
}