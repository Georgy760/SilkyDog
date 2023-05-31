using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] LevelType _levelTypeObstacles;
    [SerializeField] List<GameObject> _items;
    [SerializeField] Transform _leftPoint;
    [SerializeField] Transform _midPoint;
    [SerializeField] Transform _rightPoint;

    public LevelType level { get => _levelTypeObstacles; set => _levelTypeObstacles = value; }

    public List<GameObject> items { get => _items; set => _items = value; }

    public void Start()
    {
        StartGeneration();
    }
    public void StartGeneration()
    {
        GameObject item = _items[Random.Range(0, _items.Count)];

        int direct = Random.Range(0, 3);
        switch (direct)
        {
            case 1:
                Instantiate(item, _leftPoint);
                break;

            case 2:
                Instantiate(item, _midPoint);
                break;

            case 3:
                Instantiate(item, _rightPoint);
                break;
        } 
    }

}
