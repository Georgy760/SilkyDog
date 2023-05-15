using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "obstacles",menuName ="Obstacles/Create obstacle",order = 0)]
public class ObstaclesScritableObjects : ScriptableObject
{
    public List<GameObject> obstaclesObjects = new List<GameObject>();
    public LevelType levelType;
}
