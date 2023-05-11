using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scripts.ManagerService
{
    public class ControllerService : MonoBehaviour, IControllerService
    {
        public event Action OnButtonLeftPress;
        public event Action OnButtonLeftRelease;
        public event Action OnButtonRightPress;
        public event Action OnButtonRightRelease;
        public event Action OnButtonSpaceTap;
        
        private PlayerActions _playerActions;

        private void Awake()
        {
            _playerActions = new PlayerActions();
            _playerActions.Keyboard.Enable();
            var keyboard = _playerActions.Keyboard;
            keyboard.LeftPress.performed += LeftPress;
            keyboard.LeftRelease.performed += LeftRelease;
            keyboard.RightPress.performed += RightPress;
            keyboard.RightRelease.performed += RightRelease;
            keyboard.SpaceTap.performed += SpaceTap;
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