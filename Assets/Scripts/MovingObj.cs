using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    private float SPEED = 500;

    internal bool is_killing = false;
    private float life_time = 100;

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(5 * Time.deltaTime * -1, 0, 0);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && is_killing) {
            Debug.Log("Game-Over");

            // TODO: прописать изменения при поражении

            Time.timeScale = 0;
        }
    }
}
