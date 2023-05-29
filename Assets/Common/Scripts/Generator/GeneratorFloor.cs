using System;
using Common.GameManager.Scripts;
using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;
using Common.Scripts.Legacy;
using System.Linq;

namespace Common.Scripts.Generator
{
    public class GeneratorFloor : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _platforms;
        [SerializeField] private Transform _parentObstacles;
        private LevelType _curretLevel;
        private Dictionary<LevelType, ObstaclesScritableObjects> _countryObstaclesPrefab = new Dictionary<LevelType, ObstaclesScritableObjects>();
        private Vector3 _startPosFirstPlatform;
        private Vector3 _startPosSecondPlatform;
        private float _offesetX;
        private float _offesetY;
         
        ISessionService _service;
        [Inject]
        void Constructor(ISessionService service)
        {
            _service = service;
            _curretLevel = _service.levelType;
            _service.OnRestartSession += RestartGeneration;
            _service.OnEndRun += EndLevel;
            _service.OnLevelChange += ChangeCountry;
            foreach (ObstaclesScritableObjects obstacles in _service.obstacles)
            {
                _countryObstaclesPrefab.Add(obstacles.levelType, obstacles);
            }
        }
        private void OnDestroy()
        {
            _service.OnRestartSession -= RestartGeneration;
            _service.OnEndRun -= EndLevel;
            _service.OnLevelChange -= ChangeCountry;
            TriggerForReplaceFloor.OnTriggerToReplace -= ReplacePlatform;
        }

        private void EndLevel()
        {
            foreach (var plat in _platforms)
            {
                List<MovingObj> prevsObj = plat.GetComponentsInChildren<MovingObj>().ToList();
                foreach (MovingObj obj in prevsObj)
                    Destroy(obj.gameObject);

                List<Coin> MoneyObjs = plat.GetComponentsInChildren<Coin>().ToList();
                foreach (Coin obj in MoneyObjs)
                    Destroy(obj.gameObject);
            }
        }
       

        private void RestartGeneration()
        {
            _platforms[0].transform.position = _startPosFirstPlatform;
            _platforms[1].transform.position = _startPosSecondPlatform;
            DestroyObstacles(true);
        }

        private void ChangeCountry(LevelType level)
        {
            foreach (GameObject floor in _platforms)
                floor.GetComponent<SpriteRenderer>().sprite = _countryObstaclesPrefab[level].Floor;
            _curretLevel = level;
        }

        private void Awake()
        {
            _startPosFirstPlatform = _platforms[0].transform.position;
            _startPosSecondPlatform = _platforms[1].transform.position;

            TriggerForReplaceFloor.OnTriggerToReplace += ReplacePlatform;
            _offesetX = _platforms[0].GetComponent<BoxCollider2D>().size.x * _platforms[0].transform.localScale.x;
            _offesetY = _platforms[0].GetComponent<BoxCollider2D>().size.y * _platforms[0].transform.localScale.y;
        }

        private void ReplacePlatform(int numPlatform)
        {
            int numPlatformSecond = 1;
            if (numPlatform == 1) numPlatformSecond = 0;
            DestroyObstacles(false);
            _platforms[numPlatformSecond].transform.position = _platforms[numPlatform].transform.position + new Vector3(_offesetX, 0, 0);
            GameObject obstacles = _countryObstaclesPrefab[_curretLevel].
                                   obstaclesObjects[Random.Range(0, _countryObstaclesPrefab[_curretLevel].obstaclesObjects.Count - 1)];

            Instantiate(obstacles, _platforms[numPlatformSecond].transform.position + new Vector3(0f, _offesetY, 0f),
                                         Quaternion.identity, _parentObstacles);

        }
        private void DestroyObstacles(bool isRestart)
        {
            var children = new List<GameObject>();

            foreach (Transform child in _parentObstacles) children.Add(child.gameObject);

            if(children.Count > 1 && !isRestart)
                Destroy(children[0].gameObject);
            else if (isRestart)
                children.ForEach(x => Destroy(x.gameObject));
        }

    }
} 