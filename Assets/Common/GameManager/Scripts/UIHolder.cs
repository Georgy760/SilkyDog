using UnityEngine;
using Zenject;

namespace Common.GameManager.Scripts
{
    public class UIHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _settingsMenu;
        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.ShowMainMenu += ToggleMainMenu;
        }

        private void ToggleMainMenu(bool status)
        {
            _mainMenu.SetActive(status);
        }
    }
}