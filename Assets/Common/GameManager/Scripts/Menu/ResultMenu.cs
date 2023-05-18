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
        private IUserData _userData;
        [Inject]
        private void Construct(IGameManager gameManager, IUserData userData)
        {
            _gameManager = gameManager;
            _userData = userData;
            _gameManager.OnGameStateChanged += HandleGameStateChanged;
            _userData.DataUpdated += SetData;
            QuitButton.onClick.AddListener(HandleQuitClick);
            RestartButton.onClick.AddListener(HandleRestartClick);
        }

        private void OnDestroy()
        {
            _gameManager.OnGameStateChanged -= HandleGameStateChanged;
            _userData.DataUpdated -= SetData;
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

        private void SetData(ISessionService service)
        {
            _text.text = service.record.ToString();
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
