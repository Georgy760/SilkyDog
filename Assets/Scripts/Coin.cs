using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MovingObj
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) {
            DogControl player = col.gameObject.GetComponent<DogControl>();
            player.coins += 1;

            AudioManager.instance.PlayOneShot(AudioManager.instance.GetSound("coin_collect"), SoundType.Effects);
            Debug.Log("Coin Collected-0");
            EventManager.CallOnCoinsUpdate(player.coins);
            Debug.Log("Coin Collected-1");
            Destroy(this.gameObject);
        }
    }
}
