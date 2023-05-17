using UnityEngine;

namespace Common.Scripts.Legacy
{
    public class Coin : MovingObj
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player")) {
                DogControl player = col.gameObject.GetComponent<DogControl>();
                player.coins += 1;

                AudioManager.instance.PlayOneShot(AudioManager.instance.GetSound("coin_collect"), SoundType.Effects);
                EventManager.CallOnCoinsUpdate(player.coins);
                Destroy(this.gameObject);
            }
        }
    }
}
