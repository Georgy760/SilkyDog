using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _deltaX = 5f;
        [SerializeField] private float _deltaY = 5f;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _airTime = 2f;
        private bool _isJump = false;
        private IControllerService _controllerService;
        private ISessionService _sessionService;

        [Inject]
        void Construct(IControllerService controllerService, ISessionService sessionService)
        {
            _controllerService = controllerService;
            _controllerService.OnButtonSpaceTap += () => StartCoroutine(Jump());
            _controllerService.OnButtonRightPress += () => _deltaX += _speed;
            _controllerService.OnButtonRightRelease += () => _deltaX -= _speed;
            _controllerService.OnButtonLeftPress += () => _deltaX -= _speed;
            _controllerService.OnButtonLeftRelease += () => _deltaX += _speed;
            _sessionService = sessionService;
            _sessionService.OnStartRun += () => StartCoroutine(StartRun());
            _sessionService.OnEndRun += () =>
            {
                StopCoroutine(StartRun());
                //StartCoroutine(EndR)
            };
        }

        private IEnumerator StartRun()
        {
            while (true)
            {
                float NewY = transform.position.y;
                float NewX = Mathf.Clamp(transform.position.x + _deltaX * Time.deltaTime, transform.position.x, transform.position.x + _speed);

                if(_isJump)
                   NewY = Mathf.Clamp(transform.position.y + _deltaY * Time.deltaTime,transform.position.y, transform.position.y + _speed);

                transform.position = new Vector2(NewX, NewY);

                yield return new WaitForEndOfFrame();
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
        private IEnumerator Jump()
        {
            if (!_isJump)
            {
                _isJump = true;
                _deltaY += _jumpForce;
                yield return new WaitForSeconds(_airTime);
                _isJump = false;
                _deltaY = 0f;
            }
            yield return null;
        }
    }
}
