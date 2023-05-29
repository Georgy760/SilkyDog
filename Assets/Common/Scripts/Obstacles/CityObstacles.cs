using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class CityObstacles : MonoBehaviour, IObstacles
{
    [SerializeField] LevelType _levelTypeObstacles;
    [SerializeField] List<GameObject> _items;
    [SerializeField] Transform _leftPoint;
    [SerializeField] Transform _midPoint;
    [SerializeField] Transform _rightPoint;

    public LevelType level { get =>  _levelTypeObstacles; set => _levelTypeObstacles = value; }
    
    public List<GameObject> items { get => _items; set => _items = value; }

    public void DestroyObstacle()
    {
        throw new System.NotImplementedException();
    }

    public void StartGeneration()
    {
        int countItems = Random.Range(0, 3);
        for(int i = 0; i <  countItems; i++) { }

    }
     
}
