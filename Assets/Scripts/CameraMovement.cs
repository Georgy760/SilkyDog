using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 camera_position;

    private float offset_x;
    private float offset_y;

    private void Start()
    {
        offset_x = Mathf.Abs(transform.position.x - target.position.x);
        offset_y = Mathf.Abs(transform.position.y - target.position.y);
        camera_position = transform.position;
    }

    private void Update()
    {
        camera_position.y = target.position.y + offset_y;
        transform.position = new Vector3(camera_position.x, camera_position.y, camera_position.z);
    }
}
