using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Generator
{
    public class GeneratorFloor : MonoBehaviour
    {
        [SerializeField] private LevelType _curretLevel;
        
        private Dictionary<LevelType, List<GameObject>> _countryObstaclesPrefab = new Dictionary<LevelType, List<GameObject>>();
        
         

        [Inject]
        void Constructor(ISessionService service, List<ObstaclesScritableObjects> _obstacles)
        {
            _curretLevel = service.levelType;
            service.OnStartRun += () => StartCoroutine(StartGeneration());
            _curretLevel = service.levelType;
            foreach(ObstaclesScritableObjects obstacles in service.obstacles)
            {
                _countryObstaclesPrefab.Add(obstacles.levelType, obstacles.obstaclesObjects);     
            }
        }

        private IEnumerator StartGeneration()
        {
            while (true)
            {
                

                
                yield return new WaitForEndOfFrame();
            }
            //Todo Generation
            yield return null;
        }
    }
}
