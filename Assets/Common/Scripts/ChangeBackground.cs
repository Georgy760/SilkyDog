using Common.GameManager.Scripts;
using Common.Scripts.ManagerService; 
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace Common.Scripts
{
     public class ChangeBackground : MonoBehaviour
    {
        private Image _backGround;

        private Dictionary<LevelType,Sprite> _backGrounds = new Dictionary<LevelType, Sprite>();

        public ISessionService _service;
        public IGameManager _gameManager;
        [Inject]
        void Construct(ISessionService service, IGameManager gameManager)
        {
            _service = service;
            _service.OnLevelChange += ChangeBackGround;
            foreach (ObstaclesScritableObjects obs in service.obstacles)
            {
                _backGrounds.Add(obs.levelType, obs.BackGround);
            }
            _gameManager = gameManager;
            _gameManager.OnFadeCompleteLevelChange += ChangeLevel;
            _backGround = GetComponent<Image>();

            ChangeBackGround(_service.levelType);
        }

        private void OnDestroy()
        {
            _service.OnLevelChange -= ChangeBackGround;
        }
        
        private void ChangeLevel(bool isCompleteFade)
        {
            _service.ChangeLevel(); 
            _gameManager.EndLevelChange();
        }

        private void ChangeBackGround(LevelType level)
        {
            _backGround.sprite = _backGrounds[level];
        }
        
    }
}
