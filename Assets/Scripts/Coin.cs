using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MovingObj
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            // TODO: добавить зву сбора монеток
            DogControl player = col.gameObject.GetComponent<DogControl>();
            player.coins += 1;

            EventManager.CallOnCoinsUpdate(player.coins);
            Destroy(this.gameObject);
        }
    }
}
