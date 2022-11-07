using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum locations
{
    China = 0,
    India = 1,//
    Neon_Kazahstan = 2,//
    Desert_Egypt = 3,//
    Dubai = 4,//
    Rome = 5,
    Paris = 6,
    London = 7,//
    Tokyo = 8,
    New_York = 9//
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

    [SerializeField] private SpriteRenderer static_background;

    private int location_id;
    private int current_obstacles_start;
    private int current_obstacles_end;
    private GameObject current_moving_back;
    private GameObject current_floor;


    private void Start()
    {
        Vector3 start_pos_floor = Vector3.zero;
        Vector3 start_pos_mov_back = Vector3.zero;
        start_pos_floor.y = floor_spawn_pos.position.y;
        start_pos_mov_back.y = back_spawn_pos.position.y;

        ChangeLocation();

        Instantiate(current_floor, start_pos_floor, Quaternion.identity);
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

        float pass_time = sprite_ren.bounds.size.x / (mov_floor.Speed * 2);

        yield return new WaitForSeconds(pass_time);

        StartCoroutine(CreateFloor());
    }

    private IEnumerator CreateMovingBack()
    {
        GameObject moving_back = Instantiate(current_moving_back, back_spawn_pos);
        MovingObj back = moving_back.GetComponent<MovingObj>();
        SpriteRenderer sprite_ren = moving_back.GetComponent<SpriteRenderer>();

        float pass_time = sprite_ren.bounds.size.x / back.Speed;

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
/*switch (location_id)
{
    case (int) locations.China:
        location = locations.China;
        break;
    case (int)locations.India:
        location = locations.India;
        break;
    case (int)locations.Neon_Kazahstan:
        location = locations.Neon_Kazahstan;
        break;
    case (int)locations.Desert_Egypt:
        location = locations.Desert_Egypt;
        break;
    case (int)locations.Dubai:
        location = locations.Dubai;
        break;
    case (int)locations.Rome:
        location = locations.Rome;
        break;
    case (int)locations.Paris:
        location = locations.Paris;
        break;
    case (int)locations.London:
        location = locations.London;
        break;
    case (int)locations.Tokyo:
        location = locations.Tokyo;
        break;
    case (int)locations.New_York:
        location = locations.New_York;
        break;
}*/