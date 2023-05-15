using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void Start()
        {
            StartGame();
        }
        void StartGame()
        {
            OnStartRun?.Invoke();
        }

        void EndRun()
        {
            OnEndRun?.Invoke();
        }

    }
}
