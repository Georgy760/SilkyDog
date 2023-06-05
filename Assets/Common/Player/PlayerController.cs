using System;
using System.Collections;
using Common.Scripts;
using Common.Scripts.ManagerService;
using UnityEditor;
using UnityEngine;
using Zenject;
using AudioType = Common.Scripts.AudioType;

namespace Common.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _StartdeltaX = 5f;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _duratation = 1f;
        [SerializeField] private AnimationCurve _curveDeltay;
        [SerializeField] private LayerMask _layerGround;

        [SerializeField] private bool _isJump = false;
        [SerializeField] private float _ofssetYCollider;
        
        [SerializeField] private float _deltaX;
        [SerializeField] private Camera _camera;
        private bool _stop = true;
        private bool _leftPress = false;
        private bool _rightPress = false;
        private BoxCollider2D _collider2D;
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
            _sessionService.OnEndRun += OnEndRun;

            _collider2D = GetComponent<BoxCollider2D>();
            _ofssetYCollider = _collider2D.size.y / 2f + 0.1f;

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
            _sessionService.OnEndRun -= OnEndRun;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector2.down * 1.8f); //TODO 1.8f - magic value of sobaka bottom
            //Gizmos.DrawSphere(transform.position, 5f);
            Gizmos.color = Color.red;
        }
        #endif

        private void OnEndRun()
        {
            _stop = false;
        }

        private void OnStartRun()
        {
            _stop = true;
            StartCoroutine(StartRun());
        }

        private void TouchTriggerPress(Vector2 obj)
        {
            Debug.Log($"Press: {obj} ");
            Debug.Log("left" + _leftPress + "right" + _rightPress);
            if (obj.y > Screen.height / 2f && !_leftPress && !_rightPress)
            {
                CoroutineJump();
                return;
            }
            if (obj.x > Screen.width / 2f && obj.y < Screen.height / 2f) MoveRightPress();
            if (obj.x < Screen.width / 2f && obj.y < Screen.height / 2f) MoveLeftPress();
            
        }
        private void TouchTriggerRelease(Vector2 obj)
        {
            Debug.Log($"Release: {obj}");
            if ( _rightPress) MoveRightRelease();
            if (_leftPress) MoveLeftRelease();
        }
        private void MoveLeftRelease()
        { 
            _leftPress = false;
            if (_deltaX + 1 <= _StartdeltaX + 1)
                _deltaX += 1;
            else
                _deltaX = 0;
        }

        private void MoveLeftPress()
        {
            Debug.Log("Press Left");
            _leftPress = true;
            if (_deltaX - 1 >= _StartdeltaX - 1)
                _deltaX += -1;
            else
                _deltaX = 0;
        }

        private void MoveRightRelease()
        {

            _rightPress = false;
            if (_deltaX - 1 >= _StartdeltaX - 1)
                _deltaX += -1;
            else
                _deltaX = 0;
        }

        private void MoveRightPress()
        {
            Debug.Log("RightPress");
            _rightPress = true;
            if (_deltaX + 1 <= _StartdeltaX + 1)
                _deltaX += 1;
            else
                _deltaX = 0;
        }
        private IEnumerator StartRun()
        {
            while (_stop)
            {
                var playerX = transform.position.x;
                var position = _camera.transform.position;
                var MaxCam = position.x + _camera.orthographicSize * 2;
                
                var MinCam = position.x - _camera.orthographicSize * 2;
                
                if (playerX <= MaxCam && playerX >= MinCam)
                    transform.Translate(_deltaX / 10 * _speed, 0, 0); // Magic division by 10
                else if (playerX <= MaxCam)
                    transform.position += new Vector3(0.01f, 0, 0);
                else
                    transform.position += new Vector3(-0.01f, 0, 0);
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }

        private void CoroutineJump()
        {
            Debug.Log("Press jump");
            if (IsGrounded())
                StartCoroutine(Jump());
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
                GetComponent<Rigidbody2D>().gravityScale = 0;
                while (progress < 1f)
                {
                    expiredTime += Time.deltaTime;
                    progress = expiredTime / _duratation;
                    
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, _ofssetYCollider / 2, _layerGround);
                    if (hit.collider != null)
                        break;

                    transform.position = new Vector3(transform.position.x, startPos.y + _curveDeltay.Evaluate(progress), transform.position.z);
                    yield return new WaitForFixedUpdate();
                }
                GetComponent<Rigidbody2D>().gravityScale = 1;
                _isJump = false;
            }

            yield return null;
        }
    }
}
