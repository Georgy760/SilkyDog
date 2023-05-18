﻿using System;
using Common.Scripts.ManagerService;

namespace Common.Scripts.UserData
{
    public interface IUserData
    {
        public int PlayerID { get; set; }
        public int Money { get; set; }
        public int Record { get; set; }
        public int WalletID { get; set; }
        public event Action<ISessionService> DataUpdated;
        public event Action CollectionReady; 
        public void ConnectionCompleted();
        public bool Connected { get; }
        public void DataUpdate(ISessionService sessionService);
    }
}