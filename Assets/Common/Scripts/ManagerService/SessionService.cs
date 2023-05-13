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
        public int record { get => record; set => record = value; }
        public int money { get => money; set => money = value; }

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
