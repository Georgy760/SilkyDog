using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.GameManager.Scripts.Menu
{
    public class SettingsTab : MonoBehaviour
    {
        public Button _close;

        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _close.onClick.AddListener(BackToMenu);
            _gameManager.OnToggleSetting += ToMenu;
        }

        private void OnDestroy()
        {
            _close.onClick.RemoveListener(BackToMenu);
        }
        private void ToMenu()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        private void BackToMenu()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}