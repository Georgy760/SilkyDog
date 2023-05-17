using Common.Scripts.ManagerService;
using System;
using UnityEditor.PackageManager;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Legacy
{
    public class Coin : MonoBehaviour
    {
        public static event Action OnPickUpCoin;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player")) {
                OnPickUpCoin?.Invoke();
            }
            Destroy(gameObject);
        }
    }
}
