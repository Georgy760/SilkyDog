using System;

namespace Common.Scripts.ManagerService
{
    public interface IPlayerInputService
    {
        event Action OnButtonLeftPress;
        event Action OnButtonLeftRelease;
        event Action OnButtonRightPress;
        event Action OnButtonRightRelease;
        event Action OnButtonSpaceTap;
    }
}