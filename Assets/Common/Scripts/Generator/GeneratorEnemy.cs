using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GeneratorEnemy : MonoBehaviour
{
    [SerializeField] Transform _parentEnemy;


    private List<GameObject> _enemyObjects = new List<GameObject>();

    private GameObject _prevEnemy;

    IServiceGeneratorEnemy _generatorEnemy;
    ISessionService _sessionService;
    [Inject]
    void Construct(IServiceGeneratorEnemy generatorEnemy, ISessionService sessionService)
    {
        _generatorEnemy = generatorEnemy;
        _generatorEnemy.OnSpawnEnemy += SpawnEnemy;
        _enemyObjects = generatorEnemy.enemyPrefab;

        _sessionService = sessionService;
        _sessionService.OnEndRun += DestroyEnemy;
    }

    private void OnDestroy()
    {
        _generatorEnemy.OnSpawnEnemy -= SpawnEnemy;
        _sessionService.OnEndRun -= DestroyEnemy;
    }

    private void DestroyEnemy()
    {
        if (_prevEnemy != null)
            Destroy(_prevEnemy.gameObject);
    }

    private void SpawnEnemy(Vector3 cameraPos)
    { 
        DestroyEnemy();
        GameObject enemy = _enemyObjects[Random.Range(0, _enemyObjects.Count)];
        _prevEnemy = Instantiate(enemy, cameraPos, Quaternion.identity, _parentEnemy);
    }
}
