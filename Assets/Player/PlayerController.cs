using System;
using System.Collections;
using Common.Scripts;
using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;
using AudioType = Common.Scripts.AudioType;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _StartdeltaX = 5f; 
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _duratation = 1f;
        [SerializeField] private AnimationCurve _curveDeltay;
        [SerializeField] private LayerMask _layerGround;
        
        [SerializeField] private bool _isJump = false;
        [SerializeField] private float _ofssetYCollider;
        private float _levelTime = 0f;
        [SerializeField] private float _deltaX;
        private Vector3 _startPos;
        private bool _stop = true;
        
        private IPlayerInputService _playerInputService;
        private ISessionService _sessionService;
        private IAudioService _audioService;
        [Inject]
        void Construct(IPlayerInputService playerInputService, 
            ISessionService sessionService, 
            IAudioService audioService)
        {
            _playerInputService = playerInputService;
            _playerInputService.OnButtonSpaceTap += CoroutineJump;
            _playerInputService.OnButtonRightPress += MoveRightPress;
            _playerInputService.OnButtonRightRelease += MoveRightRelease;
            _playerInputService.OnButtonLeftPress += MoveLeftPress;
            _playerInputService.OnButtonLeftRelease += MoveLeftRelease;
            _playerInputService.OnTouchStart += TouchTriggerPress;
            _playerInputService.OnTouchEnd += TouchTriggerRelease;
            
            _sessionService = sessionService;
            _sessionService.OnStartRun += OnStartRun;
            _sessionService.OnRestartSession += RestartPlayer;
            _sessionService.OnEndRun += OnEndRun;
            
            _ofssetYCollider = GetComponent<BoxCollider2D>().size.y / 2f + 0.1f ;
            _startPos = transform.position;
            
            _audioService = audioService;
        }
        private void OnDestroy()                                          
        {
            _playerInputService.OnButtonSpaceTap -= CoroutineJump;        
            _playerInputService.OnButtonRightPress -= MoveRightPress;     
            _playerInputService.OnButtonRightRelease -= MoveRightRelease; 
            _playerInputService.OnButtonLeftPress -= MoveLeftPress;       
            _playerInputService.OnButtonLeftRelease -= MoveLeftRelease;  
            _playerInputService.OnTouchStart -= TouchTriggerPress;
            _playerInputService.OnTouchEnd -= TouchTriggerRelease;
            _sessionService.OnStartRun -= OnStartRun;
            _sessionService.OnRestartSession -= RestartPlayer;
            _sessionService.OnEndRun -= OnEndRun;
        }

        private void OnEndRun()
        { 
            _stop = false;
        }

        private void OnStartRun()
        {
            _stop = true;
            _deltaX = _StartdeltaX;
            _levelTime = 0f;
            StartCoroutine(StartRun());
        }

        private void TouchTriggerPress(Vector2 obj)
        {
            Debug.Log($"Press: {obj}");
            if(obj.x > 0f) MoveRightPress();
            if(obj.x < 0f) MoveLeftPress();
            if(obj.y > 0f && obj.x < 0f) CoroutineJump();
        }
        private void TouchTriggerRelease(Vector2 obj)
        {
            Debug.Log($"Release: {obj}");
            if(obj.x > 0f) MoveRightRelease();
            if(obj.x < 0f) MoveLeftRelease();
        }
        private void MoveLeftRelease()
        {
            _deltaX = 0;
        }

        private void MoveLeftPress()
        {
            _deltaX = -1;
        }

        private void MoveRightRelease()
        {
            _deltaX = 0;
        }

        private void MoveRightPress()
        {
            _deltaX = 1;
        }
        private IEnumerator StartRun()
        {
            while (_stop)
            {
                transform.Translate(_deltaX/10 * _speed, 0, 0); // Magic division by 10
                
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }

        private void CoroutineJump()
        {
            if(IsGrounded())
                StartCoroutine(Jump());
        }
        private void RestartPlayer()
        {
            transform.position = _startPos;
        }
        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _ofssetYCollider, _layerGround);
            
            return hit.collider != null;
        }
        private IEnumerator Jump()
        { 
            Debug.Log(IsGrounded());
            if (IsGrounded() && !_isJump)
            {
                _audioService.PlaySound(AudioType.JUMP);
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
