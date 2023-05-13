using System;

namespace Common.Scripts.ManagerService
{
    public interface ISessionService
    {
        event Action OnStartRun;
        event Action OnEndRun;
        int record { get; set; }
        int money { get; set; }
    }
}