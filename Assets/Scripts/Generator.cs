using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum locations
{
    China = 0,
    India = 1,
    Neon_Kazahstan = 2,
    Desert_Egypt = 3,
    Dubai = 4,
    Rome = 5,
    Paris = 6,
    London = 7,
    Tokyo = 8,
    New_York = 9
}

public class Generator : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private Transform spawn_pos;
    [SerializeField] private List<Sprite> backgrounds;
    [SerializeField] private SpriteRenderer background;

    private void Start()
    {
        StartCoroutine(Create_Obstacle());
    }

    private IEnumerator Create_Obstacle()
    {
        int n = Random.Range(0, obstacles.Count);
        GameObject obj = Instantiate(obstacles[n], spawn_pos);

        float pass_time = 5/GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DogControl>().objects_speed * 3 + 2;

        yield return new WaitForSeconds(pass_time);

        StartCoroutine(Create_Obstacle());
    }

    public void ChangeLocation()
    {
        int n = Random.Range(0, backgrounds.Count);
        background.sprite = backgrounds[n];
    }
}
