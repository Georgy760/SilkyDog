using System;

namespace Common.Scripts.ManagerService
{
    public interface ISessionService
    {
        event Action StartRun;
        event Action EndRun;
        int record { get; set; }
        int money { get; set; }
    }
}