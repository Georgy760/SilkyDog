using System;
using System.Collections.Generic;

namespace Common.Scripts.ManagerService
{
    public interface ISessionService
    {
        event Action OnStartRun;
        event Action OnEndRun;
        event Action OnRestartSession;
        int record { get; set; }
        int money { get; set; }

        LevelType levelType { get; set; }

        List<ObstaclesScritableObjects> obstacles { get; set; }
    }
}