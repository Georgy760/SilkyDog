using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEnemy : MonoBehaviour
{
    [SerializeField] List<GameObject> _enemyObjects;

    private void Awake()
    {
        GeneratorObstacles.OnSpawnEnemy += SpawnEnemy;
    }

    private void SpawnEnemy(Vector2 cameraPos)
    {
        GameObject enemy = _enemyObjects[Random.Range(0,_enemyObjects.Count )];
        Instantiate(enemy, cameraPos, Quaternion.identity);
    }
}
