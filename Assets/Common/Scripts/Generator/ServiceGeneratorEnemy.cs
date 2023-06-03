using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceGeneratorEnemy : MonoBehaviour, IServiceGeneratorEnemy
{
    [SerializeField] List<GameObject> _enemysPrefab;
    public List<GameObject> enemyPrefab { get => _enemysPrefab; set => _enemysPrefab = value; }
    [SerializeField] GameObject  _moneyPrefab;
    public GameObject moneyPrefab { get => _moneyPrefab; set => _moneyPrefab = value; }

    public event Action<Vector3> OnSpawnEnemy;
    public event Action<Vector3> OnSpawnMoney;

    public void SpawnEnemy(Vector3 pos)
    {
        OnSpawnEnemy?.Invoke(pos);
    }

    public void SpawnMoney(Vector3 pos)
    {
        OnSpawnMoney?.Invoke(pos);
    }
}
