using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] internal bool is_killing = false;

    public float Speed 
    {
        get { return speed; }
        private set { speed = value; } 
    }

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0) {
            if (speed <= 0)
                speed = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DogControl>().objects_speed;
        }
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(speed * Time.deltaTime * -1, 0, 0);
    }
    
    private void OnBecameInvisible()
    {
        if (this.gameObject.activeInHierarchy)
            StartCoroutine(DestroyAfterInvis());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && is_killing) {
            DogControl player = col.gameObject.GetComponent<DogControl>();
            player.panel_play.SetActive(false);
            player.panel_lose.SetActive(true);
            Time.timeScale = 0;
        }
    }
    IEnumerator DestroyAfterInvis()
    {
        yield return new WaitForSeconds(7);
        if (this.gameObject)
            Destroy(this.gameObject);
    }
}
