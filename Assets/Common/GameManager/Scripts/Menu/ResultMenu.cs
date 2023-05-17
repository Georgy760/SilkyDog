using Common.Scripts.ManagerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Scripts.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.GameManager.Scripts.Menu
{
    public class ResultMenu : MonoBehaviour
    {

        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private Button RestartButton;
        [SerializeField] private Button QuitButton;
        
        private IGameManager _gameManager;
        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.OnGameStateChanged += HandleGameStateChanged;
            QuitButton.onClick.AddListener(HandleQuitClick);
            RestartButton.onClick.AddListener(HandleRestartClick);
        }

        private void OnDestroy()
        {
            _gameManager.OnGameStateChanged -= HandleGameStateChanged;
            QuitButton.onClick.RemoveListener(HandleQuitClick);
            RestartButton.onClick.RemoveListener(HandleRestartClick);
        } 
        
        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            switch (currentState)
            {
                case GameState.RESULT:
                    gameObject.SetActive(true);
                    break;

                default:
                    gameObject.SetActive(false);
                    break;
            }
        }
         

        private void HandleQuitClick()
        {
            _gameManager.QuitLevel();
        }

        private void HandleRestartClick()
        {
            _gameManager.RestartGame();
        }

    }
}
