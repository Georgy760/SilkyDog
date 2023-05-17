using Common.GameManager.Scripts;
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
            _userData = userData;
            _sessionService = sessionService;
            _sessionService.OnEndRun += SetResults;
        }

        private void SetResults()
        {
            _userData.Money += _sessionService.money;
            if (_userData.Record < _sessionService.record) 
                _userData.Record = _sessionService.record;
        }
    }
}