using Common.Scripts.ManagerService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class GeneratorObstacles : MonoBehaviour
{

    [SerializeField] private float _offsetYmin;
    [SerializeField] private Transform _partentObstacles;
    [SerializeField] private Camera _camera;
    
    private List<GameObject> _prevsObstacles = new List<GameObject>();
    private Dictionary<LevelType, GameObject> _baseObstacles = new Dictionary<LevelType, GameObject>();
    private LevelType _level;
    private bool _spawnEnemy = false;

    public static event Action<Vector2> OnSpawnEnemy;
    
    private ISessionService _sessionService;
    private IServiceGeneratorEnemy _generatorEnemy;
    [Inject]
    void Construct(ISessionService sessionService , IServiceGeneratorEnemy generatorEnemy)
    {
        _sessionService = sessionService;
        _sessionService.OnStartRun += StartGeneration;
        _sessionService.OnEndRun += DestroyObstacle;
        foreach (ObstaclesScritableObjects obstaclesScritableObjects in _sessionService.obstacles)
            _baseObstacles.Add(obstaclesScritableObjects.levelType, obstaclesScritableObjects.obstacle);
        _level = _sessionService.levelType;

        _generatorEnemy = generatorEnemy;

    }

    void StartGeneration()
    {
        StartCoroutine(Generation());
    }

    void DestroyObstacle()
    {
        foreach (GameObject obstacle in _prevsObstacles)
            Destroy(obstacle);
        _prevsObstacles.Clear();
    }

    IEnumerator Generation()
    {
        while (true)
        {
            float sizeCamera = _camera.orthographicSize * 3;
            float distanceBetweenCameraAndObstale = -sizeCamera - 1;
            if (_prevsObstacles.Count > 0)
                distanceBetweenCameraAndObstale = _prevsObstacles.Last().transform.position.x - _camera.transform.position.x;
            if (distanceBetweenCameraAndObstale < 0 && _spawnEnemy)
            {

                Vector3 pos = new Vector3(_camera.transform.position.x + sizeCamera, _offsetYmin,0);
                if(Random.Range(0,2) == 0)
                    _generatorEnemy.SpawnEnemy(pos);
                else
                    _generatorEnemy.SpawnMoney(pos);

                _spawnEnemy = false;
            }
            if (distanceBetweenCameraAndObstale < -sizeCamera)
            {

                DestroyObstacle();
                int counteObstacle = Random.Range(1, 4);

                float minX = _camera.transform.position.x + sizeCamera;
                float maxX = minX + sizeCamera;
                float minY = _offsetYmin; //Magic Y
                float maxY = sizeCamera;
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, minY + 2);//Magic 2

                _prevsObstacles.Add(Instantiate(_baseObstacles[_level], new Vector2(x, y), Quaternion.identity, _partentObstacles));
                counteObstacle--;
                int tryPlace = 0;

                while (counteObstacle != 0 && tryPlace < 100)
                {
                    x = Random.Range(_prevsObstacles.Last().transform.position.x, _prevsObstacles.Last().transform.position.x + sizeCamera);
                    y = Random.Range(minY, maxY);

                    if (y > 2f + minY && y <= 4f + minY) continue; //Magic 2 and 4
                    bool isCanPlace = false;

                    foreach (GameObject obstacles in _prevsObstacles)
                    {
                        if (Vector2.Distance(obstacles.transform.position, new Vector2(x, y)) < 12) // Magic 12 distanceBetweenObject
                            break;
                        isCanPlace = true;
                    }

                    if (isCanPlace)
                    {
                        _prevsObstacles.Add(Instantiate(_baseObstacles[_level], new Vector2(x, y), Quaternion.identity, _partentObstacles));
                        counteObstacle--;
                    }
                    tryPlace++;
                }
                if (counteObstacle > 0)
                    Debug.Log("Can't place obstacle");

                _spawnEnemy = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
