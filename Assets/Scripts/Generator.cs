using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private List<Sprite> static_backgrounds;
    [SerializeField] private List<GameObject> moving_backgrounds;
    [SerializeField] private Transform obstacle_spawn_pos;
    [SerializeField] private Transform back_spawn_pos;
    [SerializeField] private SpriteRenderer static_background;

    private int location_id;
    private locations location;

    private void Start()
    {
        StartCoroutine(Create_Obstacle());
        StartCoroutine(CreateMovingBack());
    }

    private IEnumerator CreateMovingBack()
    {
        GameObject moving_back = Instantiate(moving_backgrounds[location_id], back_spawn_pos);

        yield return new WaitForSeconds(2);

        StartCoroutine(CreateMovingBack());
    }

    private IEnumerator Create_Obstacle()
    {
        int n = Random.Range(0, obstacles.Count);
        GameObject obstacle = Instantiate(obstacles[n], obstacle_spawn_pos);

        float pass_time = 5/GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DogControl>().objects_speed * 3 + 2;

        yield return new WaitForSeconds(pass_time);

        StartCoroutine(Create_Obstacle());
    }

    public void ChangeLocation()
    {
        location_id = Random.Range(0, static_backgrounds.Count);
        switch (location_id)
        {
            case (int) locations.China:
                location = locations.China;
                break;
        }
        static_background.sprite = static_backgrounds[location_id];
    }
}
