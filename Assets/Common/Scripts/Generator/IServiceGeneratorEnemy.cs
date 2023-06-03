using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServiceGeneratorEnemy  
{
    List<GameObject> enemyPrefab { get; set; }

    GameObject moneyPrefab { get; set; }

    event Action<Vector3> OnSpawnEnemy; 

    event Action<Vector3> OnSpawnMoney; 



    void SpawnEnemy(Vector3 pos);
    void SpawnMoney(Vector3 pos);
}
