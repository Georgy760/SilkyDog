using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Generator
{
    public class GeneratorFloor : MonoBehaviour
    {
        private LevelType _curretLevel;
        private Dictionary<LevelType, List<GameObject>> _countryObstaclesPrefab = new Dictionary<LevelType, List<GameObject>>();
        [SerializeField] List<GameObject> _platforms; 
        private int _counterPlatforms = 0;
        private float _offeseX;
        private float _offeseY;
        private int _curretPlatform = 0;
        [SerializeField] GameObject _prevsObstacle;
        [Inject]
        void Constructor(ISessionService service, List<ObstaclesScritableObjects> _obstacles)
        {
            _curretLevel = service.levelType;
            service.OnStartRun += () => StartCoroutine(StartGeneration());
            foreach(ObstaclesScritableObjects obstacles in service.obstacles)
            {
                _countryObstaclesPrefab.Add(obstacles.levelType, obstacles.obstaclesObjects);     
            }

        }

        private void Awake()
        {
            TriggerForReplaceFloor.OnTriggerToReplace += ReplacePlatform;
            _offeseX = _platforms[0].GetComponent<BoxCollider2D>().size.x * _platforms[0].transform.localScale.x;
            _offeseY = _platforms[0].GetComponent<BoxCollider2D>().size.y * _platforms[0].transform.localScale.y;
            StartCoroutine(StartGeneration()); //This delete after create event for StartGame
        }

        private void ReplacePlatform(int numPlatform)
        {
            int numPlatformSecond = 1;
            if (numPlatform == 1) numPlatformSecond = 0;
            _platforms[numPlatformSecond].transform.position = _platforms[numPlatform].transform.position + new Vector3(_offeseX, 0,0);
            _curretPlatform = numPlatformSecond;
            _counterPlatforms += 1;
        }
        private IEnumerator StartGeneration()
        {
            while (true)
            {
                if (_counterPlatforms == 2)
                {
                    Destroy(_prevsObstacle.gameObject);
                    GameObject obstacles = _countryObstaclesPrefab[_curretLevel][Random.Range(0, _countryObstaclesPrefab[_curretLevel].Count - 1)];
                    _prevsObstacle = Instantiate(obstacles, _platforms[_curretPlatform].transform.position + new Vector3(0f, _offeseY, 0f), Quaternion.identity);
                    _counterPlatforms = 0;
                }
                yield return new WaitForEndOfFrame();
            }
            //Todo Generation
            yield return null;
        }
    }
}
