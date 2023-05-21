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
        [SerializeField] private Image _sprite;
        [SerializeField] GameObject _FloorEnemy;
        [SerializeField] GameObject _AirEnemy;
        [SerializeField] private RawImage _movingBack;
        [SerializeField] private Transform _parentObstacles;
        [SerializeField] private GameObject _moneyObstales;
        private LevelType _curretLevel;
        private Dictionary<LevelType, List<GameObject>> _countryObstaclesPrefab = new Dictionary<LevelType, List<GameObject>>();
        private Dictionary<LevelType, Sprite> _countryBackGround = new Dictionary<LevelType, Sprite>();
        private Dictionary<LevelType, Sprite> _countryFloor = new Dictionary<LevelType, Sprite>();
        private Dictionary<LevelType, Texture2D> _countryBack = new Dictionary<LevelType, Texture2D>();
        private Vector3 _startPosFirstPlatform;
        private Vector3 _startPosSecondPlatform;
        private float _offesetX;
        private float _offesetY;
        private int _curretPlatform = 0;
        private int _counterPlatforms = 0;
        private bool _isStop = true;

        IGameManager _manager;
        ISessionService _service;
        [Inject]
        void Constructor(ISessionService service, IGameManager manager)
        {
            _service = service;
            _curretLevel = _service.levelType;
            _service.OnRestartSession += RestartGeneration;
            _service.OnEndRun += EndLevel;
            foreach (ObstaclesScritableObjects obstacles in _service.obstacles)
            {
                _countryObstaclesPrefab.Add(obstacles.levelType, obstacles.obstaclesObjects);
                _countryBackGround.Add(obstacles.levelType, obstacles.BackGround);
                _countryFloor.Add(obstacles.levelType, obstacles.Floor);
                _countryBack.Add(obstacles.levelType, obstacles.BackImage);
            }

            _manager = manager;
            _manager.OnFadeCompleteLevelChange += StartLevelChange;
            ChangeCountry(_curretLevel);
        }
        private void OnDestroy()
        {
            _service.OnRestartSession -= RestartGeneration;
            _service.OnEndRun -= EndLevel;
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
        private void StartLevelChange(bool isChange)
        {
            if (isChange)
            {
                LevelType level = _curretLevel;
                while (level == _curretLevel)
                    level = (LevelType)Random.Range(0, 5);
                ChangeCountry(level);
                _manager.EndLevelChange();
            }
        }

        private void RestartGeneration()
        {
            _platforms[0].transform.position = _startPosFirstPlatform;
            _platforms[1].transform.position = _startPosSecondPlatform;
            _counterPlatforms = 0;
        }
        private void ChangeCountry(LevelType level)
        {
            _movingBack.texture = _countryBack[level];
            _sprite.sprite = _countryBackGround[level];
            foreach (GameObject floor in _platforms)
                floor.GetComponent<SpriteRenderer>().sprite = _countryFloor[level];
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
            _platforms[numPlatformSecond].transform.position = _platforms[numPlatform].transform.position + new Vector3(_offesetX, 0, 0);
            _curretPlatform = numPlatformSecond;
            List<MovingObj> prevsObj = _platforms[_curretPlatform].GetComponentsInChildren<MovingObj>().ToList();
            foreach (MovingObj obj in prevsObj)
            {
                if (obj != null)
                {
                    Destroy(obj.gameObject);
                }
            }
            int typeObstacles = Random.Range(0, 3);
            GameObject obst;
            switch (typeObstacles)
            {
                case 0:

                    GameObject obstacles = _countryObstaclesPrefab[_curretLevel][Random.Range(0, _countryObstaclesPrefab[_curretLevel].Count - 1)];
                    obst = Instantiate(obstacles, _platforms[_curretPlatform].transform.position + new Vector3(-_offesetX / 4f, _offesetY * 0.8f, 0f),
                                                 Quaternion.identity, _platforms[_curretPlatform].transform);
                    obst.transform.localScale /= _platforms[_counterPlatforms].transform.localScale.x;
                    break;
                case 1:
                    int typeEnemy = Random.Range(0, 2);
                    Debug.Log(typeEnemy);
                    switch (typeEnemy)
                    {
                        case 0:
                            Instantiate(_FloorEnemy, _platforms[_curretPlatform].transform.position + new Vector3(0f, _offesetY * 0.8f, 0f), Quaternion.identity, _parentObstacles);
                            break;
                        case 1:
                            Instantiate(_AirEnemy, _platforms[_curretPlatform].transform.position + new Vector3(0f, _offesetY * 1.8f, 0f), Quaternion.identity, _parentObstacles);
                            break;
                    }
                    break;
                case 2:
                    obst = Instantiate(_moneyObstales, _platforms[_curretPlatform].transform.position + new Vector3(-_offesetX / 4f, _offesetY * 0.8f, 0f),
                                                 Quaternion.identity, _platforms[_curretPlatform].transform);
                    obst.transform.localScale /= _platforms[_counterPlatforms].transform.localScale.x;
                    break;
            }
        }
    }
}
