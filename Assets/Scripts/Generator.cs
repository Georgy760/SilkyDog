using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum locations
{
    China = 0,
    India = 1,//back tiles
    Neon_Kazahstan = 2,//back
    Desert_Egypt = 3,//back tiles
    Dubai = 4,//back
    Rome = 5,
    Paris = 6,//back
    London = 7,//back
    Tokyo = 8,//back
    New_York = 9//back
}

public class Generator : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private List<Sprite> static_backgrounds;
    [SerializeField] private List<GameObject> moving_backgrounds;
    [SerializeField] private List<GameObject> floors;

    [Header("Spawn Points")]
    [SerializeField] private Transform obstacle_spawn_pos;
    [SerializeField] private Transform back_spawn_pos;
    [SerializeField] private Transform floor_spawn_pos;
    [SerializeField] private Transform main_back_spawn_pos;

    [SerializeField] private SpriteRenderer static_background;

    private int location_id;
    private int current_obstacles_start;
    private int current_obstacles_end;
    private GameObject current_moving_back;
    private GameObject current_floor;

    private GameObject old_back;


    private void Start()
    {
        Vector3 start_pos_floor = new Vector3(0,floor_spawn_pos.position.y,0);
        Vector3 start_pos_mov_back = Vector3.zero;
        start_pos_mov_back.y = back_spawn_pos.position.y;

        ChangeLocation();

        GameObject FirstFloor = Instantiate(current_floor, floor_spawn_pos.transform.position, Quaternion.identity, floor_spawn_pos);
        FirstFloor.transform.localPosition = new Vector3(-13, -0.1622f, 0);
        Instantiate(current_moving_back, start_pos_mov_back, Quaternion.identity);


        StartCoroutine(CreateObstacle());
        StartCoroutine(CreateMovingBack());
        StartCoroutine(CreateFloor());
    }

    private IEnumerator CreateFloor()
    {
        GameObject floor = Instantiate(current_floor, floor_spawn_pos);
        MovingObj mov_floor = floor.GetComponent<MovingObj>();
        SpriteRenderer sprite_ren = floor.GetComponent<SpriteRenderer>();

        float pass_time = sprite_ren.bounds.size.x / (mov_floor.Speed*2);

        yield return new WaitForSeconds(pass_time);

        StartCoroutine(CreateFloor());
    }

    private IEnumerator CreateMovingBack()
    {
        if (old_back && current_moving_back) {
            if (old_back.GetComponent<SpriteRenderer>().sprite != current_moving_back.GetComponent<SpriteRenderer>().sprite)
                old_back.GetComponent<SpriteRenderer>().sprite = current_moving_back.GetComponent<SpriteRenderer>().sprite;
        }

        GameObject moving_back = Instantiate(current_moving_back, back_spawn_pos);
        MovingObj back = moving_back.GetComponent<MovingObj>();
        SpriteRenderer sprite_ren = moving_back.GetComponent<SpriteRenderer>();

        float pass_time = sprite_ren.bounds.size.x / back.Speed;

        old_back = moving_back;

        yield return new WaitForSeconds(pass_time);

        StartCoroutine(CreateMovingBack());
    }

    private IEnumerator CreateObstacle()
    {
        int n = Random.Range(current_obstacles_start, current_obstacles_end);
        GameObject obstacle = Instantiate(obstacles[n], obstacle_spawn_pos);

        float pass_time = 5/GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DogControl>().objects_speed * 3 + 3;

        yield return new WaitForSeconds(pass_time);

        StartCoroutine(CreateObstacle());
    }

    public void ChangeLocation()
    {
        location_id = Random.Range(0, static_backgrounds.Count);

        static_background.sprite = static_backgrounds[location_id];

        current_moving_back = moving_backgrounds[location_id];
        current_floor = floors[location_id];
        current_obstacles_start = location_id * 5;
        current_obstacles_end = (location_id * 5) + 5;
    }
}