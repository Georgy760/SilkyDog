using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogControl : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float jump_speed = 1000;

    internal int coins = 0;

    private Rigidbody2D rb;
    private bool is_grounded;

    private void Start()
    {
        coins = 0;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        is_grounded = true;
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && is_grounded) {
            is_grounded = false;
            rb.AddForce(transform.up * jump_speed * 100);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 6)
            is_grounded = true;
    }
}
