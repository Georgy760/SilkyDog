using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GeneratorObstacles : MonoBehaviour
{

    [SerializeField] private float _offsetYmin = 0;
    [SerializeField] private Transform _partentObstacles;
    private List<GameObject> _prevsObstacles = new List<GameObject>();
    private Dictionary<LevelType, GameObject> _baseObstacles = new Dictionary<LevelType, GameObject>();
    private LevelType _level;

    ISessionService _sessionService;
    [Inject]
    void Constuct(ISessionService sessionService)
    {
        _sessionService = sessionService;
        _sessionService.OnStartRun += StartGeneration;
        foreach (ObstaclesScritableObjects obstaclesScritableObjects in _sessionService.obstacles)
            _baseObstacles.Add(obstaclesScritableObjects.levelType, obstaclesScritableObjects.obstacle);
        _level = _sessionService.levelType;
       
    }

    void StartGeneration()
    {
        StartCoroutine(Generation());
    }


    IEnumerator Generation()
    { 
        while(true)
        {
            float _sizeCamera = Camera.main.orthographicSize;
            float distanceBetweenCameraAndObstale = Camera.main.transform.position.x - _prevsObstacles.Last().transform.position.x;
            if (_prevsObstacles.Count == 0 || distanceBetweenCameraAndObstale <= -_sizeCamera)
            {
                int counteObstacle = Random.Range(1, 4);
                
                float minX = Camera.main.transform.position.x  + _sizeCamera;
                float maxX = minX + (_sizeCamera *  counteObstacle);
                float minY = _offsetYmin; //Magic Y
                float maxY = _sizeCamera;
               
                while(counteObstacle != 0)
                {
                    float x = Random.Range(minX, maxX);
                    float y =  Random.Range(minY, maxY);
                        
                    if(Vector2.Distance(_prevsObstacles.Last().transform.position,new Vector2(x,y)) >=  5 || _prevsObstacles.Count == 0) // Magic 5 distanceBetweenObject
                    {
                        Instantiate(_baseObstacles[_level], new Vector2(x, y), Quaternion.identity,_partentObstacles);
                        counteObstacle--;
                    }
                }

            }


        }
    }
}
