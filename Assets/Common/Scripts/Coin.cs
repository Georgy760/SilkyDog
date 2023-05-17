using System;
using UnityEngine;
using Zenject;

namespace Common.Scripts
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
