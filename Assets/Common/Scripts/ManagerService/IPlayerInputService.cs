using System;
using Vector2 = UnityEngine.Vector2;

namespace Common.Scripts.ManagerService
{
    public interface IPlayerInputService
    {
        event Action OnButtonLeftPress;
        event Action OnButtonLeftRelease;
        event Action OnButtonRightPress;
        event Action OnButtonRightRelease;
        event Action OnButtonSpaceTap;
        event Action<Vector2> OnLeftMouseButtonTap;
        event Action<Vector2> OnTouchStart;
        event Action<Vector2> OnTouchEnd;
    }
}