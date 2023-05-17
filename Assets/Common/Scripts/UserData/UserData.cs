using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Scripts.UserData
{
    public class UserData : MonoBehaviour, IUserData
    {
        [SerializeField] private int _playerID = 0;
        public int PlayerID
        {
            get => _playerID;
            set
            {
                _playerID = value;
            }
        }

        //TODO private PlayerCollection _collection; {crypto player collection}
        [SerializeField] private int _walletID = 0;
        public int WalletID
        {
            get => _walletID;
            set
            {
                _walletID = value;
            }
        }
        [SerializeField] private int _money = 0;
        public int Money
        {
            get => _money;
            set
            {
                _money = value;
            }
        }
        [SerializeField] private int _record = 0;
        public int Record
        {
            get => _record;
            set
            {
                _record = value;
            }
        }
        public bool Connected { get; set; } = false;
        
        public event Action CollectionReady;
        public void ConnectionCompleted()
        {
            Connected = true;
            CollectionReady?.Invoke();
        }
    }
}