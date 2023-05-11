using System.Collections;
using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _deltaX = 15f;
        [SerializeField] private float _deltaY = 20f;
        [SerializeField] private float _speed = 10f;
        private Vector2 _deltaPos = Vector2.zero;
        private IControllerService _controllerService;
        private ISessionService _sessionService;
        [Inject]
        void Construct(IControllerService controllerService, ISessionService sessionService)
        {
            _controllerService = controllerService;
            _sessionService = sessionService;
            _controllerService.OnButtonSpaceTap += () => Debug.Log("Space");
            _controllerService.OnButtonRightPress += () => Debug.Log("RightPress");
            _controllerService.OnButtonRightRelease += () => Debug.Log("RightRelease");
            _controllerService.OnButtonLeftPress += () => Debug.Log("LeftPress");
            _controllerService.OnButtonLeftRelease += () => Debug.Log("LeftRelease");
            _sessionService.StartRun += () => StartCoroutine(StartRun());
            _sessionService.EndRun += () => 
            {
                StopCoroutine(StartRun());
                //StartCoroutine(EndR)
            };
        }
        
        private IEnumerator StartRun()
        {
            while (true)
            {
                
            }
            yield return null;
        }
        private IEnumerator EndRun()
        {
            while (true)
            {
                
            }
            yield return null;
        }
    }
}
