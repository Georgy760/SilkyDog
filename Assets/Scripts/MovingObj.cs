using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    private float SPEED = 500;

    [SerializeField] internal bool is_killing = false;
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
            DogControl player = col.gameObject.GetComponent<DogControl>();
            player.canvas_play.SetActive(false);
            player.canvas_lose.SetActive(true);
            // TODO: прописать изменения при поражении

            Time.timeScale = 0;
        }
    }
}
