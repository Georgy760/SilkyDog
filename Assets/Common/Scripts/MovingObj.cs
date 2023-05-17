using Common.GameManager.Scripts;
using Common.Scripts.ManagerService;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Legacy
{
    public class MovingObj : MonoBehaviour
    {
        public static event Action OnDeath;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                OnDeath?.Invoke();
            }
        }
    }
}
