using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump_speed = 1000;
    private Rigidbody2D rb;
    private bool IsGrounded;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        IsGrounded = true;
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && IsGrounded) {
            IsGrounded = false;
            rb.AddForce(transform.up * jump_speed * 100);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 6)
            IsGrounded = true;
    }
}
