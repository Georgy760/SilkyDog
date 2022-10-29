using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private Transform spawn_pos;

    private void Start()
    {
        StartCoroutine(Create_Obstacle());
    }

    private IEnumerator Create_Obstacle()
    {
        int n = Random.Range(0, obstacles.Count);
        GameObject obj = Instantiate(obstacles[n], spawn_pos);

        float pass_time = 5/GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DogControl>().objects_speed * 3 + 1;

        yield return new WaitForSeconds(pass_time);

        StartCoroutine(Create_Obstacle());
    }
}
