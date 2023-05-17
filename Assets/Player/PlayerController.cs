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
        private IPlayerInputService _playerInputService;
        private ISessionService _sessionService;
        private float _ofssetYCollider;
        private Vector3 _StartPos;
        private bool _stop = true;
        [Inject]
        void Construct(IPlayerInputService playerInputService, ISessionService sessionService)
        {
            _playerInputService = playerInputService;
            _playerInputService.OnButtonSpaceTap += () => StartCoroutine(Jump());
            _playerInputService.OnButtonRightPress += () => _deltaX += _speed;
            _playerInputService.OnButtonRightRelease += () => _deltaX -= _speed;
            _playerInputService.OnButtonLeftPress += () => _deltaX -= _speed;
            _playerInputService.OnButtonLeftRelease += () => _deltaX += _speed;
            _sessionService = sessionService;
            _sessionService.OnStartRun += () =>
            {
                _stop = true;
                StartCoroutine(StartRun());
            };
            _sessionService.OnRestartSession += RestartPlayer;
            _sessionService.OnEndRun += () =>
            {
                _stop = false;
            };
            _ofssetYCollider = GetComponent<BoxCollider2D>().size.y / 2f;
            _StartPos = transform.position;
            
        }

        private void RestartPlayer()
        {
            transform.position = _StartPos;
        }
        private IEnumerator StartRun()
        {
            while (_stop)
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
            // ��������� BoxCast ����, ����� ��������� ������������ � ����������� �����
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _ofssetYCollider, _layerGround);

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

                    transform.position = new Vector3(transform.position.x, startPos.y + _curveDeltay.Evaluate(progress), transform.position.z);
                    yield return new WaitForFixedUpdate();
                }
                _isJump = false;
            }
            
            yield return null;
        }
    }
}
