using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] internal bool is_killing = false;

    private void Start()
    {
        if(speed <= 0)
            speed = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DogControl>().objects_speed;
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(speed * Time.deltaTime * -1, 0, 0);
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
            Time.timeScale = 0;
        }
    }
}
