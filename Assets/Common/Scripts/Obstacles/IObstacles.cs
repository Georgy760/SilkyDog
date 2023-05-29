using Common.Scripts.ManagerService;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacles
{
    public LevelType level { get; set; }
    
    public List<GameObject> items { get; set; }

    public void StartGeneration();

    public void DestroyObstacle();

}
