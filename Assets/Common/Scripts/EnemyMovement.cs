﻿using Common.Scripts.ManagerService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] float _deltaX;
        [SerializeField] float _speed;
        private Vector3 StartPos;
 
        private void Start()
        {
            StartPos = transform.position;
            StartCoroutine(StartRun());
        }
         

        private IEnumerator StartRun()
        {
            while (true)
            {
                float NewX = Mathf.Clamp(_speed * Time.deltaTime, _deltaX * Time.deltaTime, _deltaX + _speed * Time.deltaTime);
                transform.position += new Vector3(NewX, 0, 0);
                if (Vector3.Distance(StartPos, transform.position) > 40) Destroy(gameObject);
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }
    }
}