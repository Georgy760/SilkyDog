using System;
using Common.GameManager.Scripts;
using Common.Scripts.Legacy;
using Common.Scripts.ManagerService;
using Common.Scripts.UserData;
using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    public class Results : MonoBehaviour
    {
        private IUserData _userData;
        private ISessionService _sessionService;
        [Inject]
        private void Construct(IUserData userData, ISessionService sessionService)
        {
            Debug.Log("Inject Record");
            _userData = userData;
            _sessionService = sessionService;
            MovingObj.OnDeath += SetResults;
        }

        private void OnDestroy()
        {
            MovingObj.OnDeath -= SetResults;
        }

        private void SetResults()
        {
            Debug.Log($"Money {_sessionService.money}\nDistance {_sessionService.record}"  );
            _userData.DataUpdate(_sessionService);
        }
    }
}