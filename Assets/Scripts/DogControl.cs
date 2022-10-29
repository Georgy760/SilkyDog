using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;

public class DogControl : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float jump_speed = 1000;
    [SerializeField] private Text coins_text;
    [SerializeField] private Text metres_text;
    [SerializeField] internal GameObject canvas_play;
    [SerializeField] internal GameObject canvas_lose;
    

    internal int coins = 0;

    private Rigidbody2D rb;
    private bool is_grounded;
    private int coll_counts = 0;
    private double distance;

    private void Start()
    {
        coins = 0;
        distance = 0;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        is_grounded = true;

        EventManager.eventOnCoinsCollect += (int coins_num) => coins_text.text = coins_num.ToString();
    }

    private void FixedUpdate()
    {
        distance += 0.1;
        distance = Math.Round(distance, 2);
    }

    private void Update()
    {
        metres_text.text = "Metres: " + distance.ToString();
        this.transform.rotation = new Quaternion(0, 0, 0, 1);
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && is_grounded && coll_counts > 0) {
            is_grounded = false;
            rb.AddForce(transform.up * jump_speed * 100);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        coll_counts++;
        if (col.gameObject.layer == 6)
            is_grounded = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        coll_counts--;
    }
}
