using System;
using System.Collections.Generic; 
using UnityEngine;

namespace Common.Scripts.ManagerService
{
    public class SessionService : MonoBehaviour, ISessionService
    {
        public int record { get; set; }
        public int money { get; set; }

        [SerializeField] private LevelType _levelType;
        public LevelType levelType { get => _levelType; set => _levelType = value; }

        [SerializeField] private List<ObstaclesScritableObjects> _obstacles = new List<ObstaclesScritableObjects>();
        public List<ObstaclesScritableObjects> obstacles { get => _obstacles; set => _obstacles = value; }
        public event Action OnStartRun;
        public event Action OnEndRun;
        public event Action OnRestartSession;
        public event Action<LevelType> OnLevelChange;
        public void ChangeLevel()
        {
            LevelType level = _levelType;
            while (level == _levelType)
                _levelType = (LevelType)UnityEngine.Random.Range(0, 5);
            OnLevelChange?.Invoke(levelType);
        }

       
        public void StartGame()
        {
            OnStartRun?.Invoke();
        }

        public void EndRun()
        {
            OnEndRun?.Invoke();
        }
        public void RestartGame()
        {
            OnRestartSession?.Invoke();
        }
    }
}
