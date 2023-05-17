using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scripts.ManagerService
{
    public class PlayerInputService : MonoBehaviour, IPlayerInputService
    {
        public event Action OnButtonLeftPress; //IPlayerInputService
        public event Action OnButtonLeftRelease; //IPlayerInputService
        public event Action OnButtonRightPress; //IPlayerInputService
        public event Action OnButtonRightRelease; //IPlayerInputService
        public event Action OnButtonSpaceTap; //IPlayerInputService
        public event Action<Vector2> OnLeftMouseButtonTap; 
        public event Action<Vector2> OnTouchStart;
        public event Action<Vector2> OnTouchEnd;
        
        private PlayerActions _playerActions;
        private Vector2 StartTouchPos;

        private void Awake()
        {
            _playerActions = new PlayerActions();
            _playerActions.Play.Enable();
            var play = _playerActions.Play;
            play.LeftPress.performed += LeftPress;
            play.LeftRelease.performed += LeftRelease;
            play.RightPress.performed += RightPress;
            play.RightRelease.performed += RightRelease;
            play.SpaceTap.performed += SpaceTap;
            
            _playerActions.MobilePlay.Enable();
            var mobilePlay = _playerActions.MobilePlay;
            mobilePlay.TouchPress.started += StartTouch;
            mobilePlay.TouchPress.canceled += EndTouch;
            
        }

        private void OnDestroy()
        {
            _playerActions.Play.Disable();
            var play = _playerActions.Play;
            play.LeftPress.performed -= LeftPress;
            play.LeftRelease.performed -= LeftRelease;
            play.RightPress.performed -= RightPress;
            play.RightRelease.performed -= RightRelease;
            play.SpaceTap.performed -= SpaceTap;
            _playerActions.MobilePlay.Disable();
            var mobilePlay = _playerActions.MobilePlay;
            mobilePlay.TouchPress.started -= StartTouch;
            mobilePlay.TouchPress.canceled -= EndTouch;
        }
        private void StartTouch(InputAction.CallbackContext context)
        {
            Debug.Log("Touch started " + _playerActions.MobilePlay.TouchPosition.ReadValue<Vector2>());
            StartTouchPos = _playerActions.MobilePlay.TouchPosition.ReadValue<Vector2>();
            OnTouchStart?.Invoke(_playerActions.MobilePlay.TouchPosition.ReadValue<Vector2>().normalized);
        }
        private void EndTouch(InputAction.CallbackContext context)
        {
            Debug.Log("Touch ended " + _playerActions.MobilePlay.TouchPosition.ReadValue<Vector2>() + "\n" +
                      $"Vector2: {(_playerActions.MobilePlay.TouchPosition.ReadValue<Vector2>() - StartTouchPos).normalized}");
            OnTouchEnd?.Invoke((_playerActions.MobilePlay.TouchPosition.ReadValue<Vector2>() - StartTouchPos).normalized);
        }
        private void SpaceTap(InputAction.CallbackContext obj)
        {
            OnButtonSpaceTap?.Invoke();
        }
        private void LeftPress(InputAction.CallbackContext obj)
        {
            OnButtonLeftPress?.Invoke();
        }
        private void LeftRelease(InputAction.CallbackContext obj)
        {
            OnButtonLeftRelease?.Invoke();
        }
        private void RightPress(InputAction.CallbackContext obj)
        {
            OnButtonRightPress?.Invoke();
        }
        private void RightRelease(InputAction.CallbackContext obj)
        {
            OnButtonRightRelease?.Invoke();
        }

        
    }
}