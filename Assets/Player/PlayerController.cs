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
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _duratation = 1f;
        [SerializeField] private AnimationCurve _curveDeltay;
        [SerializeField] private LayerMask _layerGround;
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
                float NewX = Mathf.Clamp(_speed * Time.deltaTime, _deltaX * Time.deltaTime, _deltaX + _speed * Time.deltaTime);
                transform.position += new Vector3(NewX, 0,0);

                yield return new WaitForFixedUpdate();
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

        private bool IsGrounded()
        {
            // Выполняем BoxCast вниз, чтобы проверить столкновение с коллайдером земли
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.down, .1f, _layerGround.value);

            return hit.collider != null;
        }
        private IEnumerator Jump()
        {
           
            if (IsGrounded() && !_isJump)
            {
                _isJump = true;
                float expiredTime = 0f;
                float progress = 0f;
                Vector2 startPos = transform.position;
                while (progress < 1f)
                {
                    expiredTime += Time.deltaTime;
                    progress = expiredTime / _duratation;

                    transform.position += new Vector3(0, startPos.y + _curveDeltay.Evaluate(progress), 0);
                    yield return new WaitForFixedUpdate();
                }
                _isJump = false;
            }
            
            yield return null;
        }
    }
}
