using System;

namespace Common.Scripts.ManagerService
{
    public interface IControllerService
    {
        event Action OnButtonLeftPress;
        event Action OnButtonLeftRelease; 
        
        event Action OnButtonRightPress;
        event Action OnButtonRightRelease; 
        
        event Action OnButtonSpaceTap;
    }
}