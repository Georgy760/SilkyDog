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
        
        
        private PlayerActions _playerActions;

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
        }

        private void OnDestroy()
        {
            var play = _playerActions.Play;
            play.LeftPress.performed -= LeftPress;
            play.LeftRelease.performed -= LeftRelease;
            play.RightPress.performed -= RightPress;
            play.RightRelease.performed -= RightRelease;
            play.SpaceTap.performed -= SpaceTap;
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