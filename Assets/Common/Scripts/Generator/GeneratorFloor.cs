 using Common.Scripts.ManagerService; 
using System.Collections.Generic; 
using UnityEngine; 
using Zenject; 

namespace Common.Scripts.Generator
{
    public class GeneratorFloor : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _platforms;  
        private Dictionary<LevelType, Sprite >_countryObstaclesPrefab = new Dictionary<LevelType, Sprite>();
        private Vector3 _startPosFirstPlatform;
        private Vector3 _startPosSecondPlatform;
        private float _offsetX;

        ISessionService _service;
        [Inject]
        void Constructor(ISessionService service)
        {
            _service = service; 
            _service.OnRestartSession += RestartGeneration; 
            _service.OnLevelChange += ChangeCountry;
            foreach (ObstaclesScritableObjects obstacles in _service.obstacles)
            {
                _countryObstaclesPrefab.Add(obstacles.levelType, obstacles.Floor);
            }
        }
        private void OnDestroy()
        {
            _service.OnRestartSession -= RestartGeneration; 
            _service.OnLevelChange -= ChangeCountry;
            TriggerForReplaceFloor.OnTriggerToReplace -= ReplacePlatform;
        }
         
       

        private void RestartGeneration()
        {
            _platforms[0].transform.position = _startPosFirstPlatform;
            _platforms[1].transform.position = _startPosSecondPlatform; 
        }

        private void ChangeCountry(LevelType level)
        {
            foreach (GameObject floor in _platforms)
                floor.GetComponent<SpriteRenderer>().sprite = _countryObstaclesPrefab[level]; 
        }

        private void Awake()
        {
            _startPosFirstPlatform = _platforms[0].transform.position;
            _startPosSecondPlatform = _platforms[1].transform.position;

            TriggerForReplaceFloor.OnTriggerToReplace += ReplacePlatform;
            _offsetX = _platforms[0].GetComponent<BoxCollider2D>().size.x * _platforms[0].transform.localScale.x; 
        }

        private void ReplacePlatform(int numPlatform)
        {
            int numPlatformSecond = 1;
            if (numPlatform == 1) numPlatformSecond = 0; 
            _platforms[numPlatformSecond].transform.position = _platforms[numPlatform].transform.position + new Vector3(_offsetX, 0, 0); 
        }
 

    }
} 