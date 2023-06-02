using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Scripts.Generator
{
    public class TriggerForReplaceFloor : MonoBehaviour
    {
        public static event Action<int> OnTriggerToReplace;
        [SerializeField] private int _numPlatform;
        [SerializeField] private Camera _camera;
        void Start ()
        {
            StartCoroutine(CheckOnCamera());
        }

        IEnumerator CheckOnCamera()
        {
            while(true)
            {
                if (_camera.transform.position.x >= transform.position.x && _camera.transform.position.x <= transform.position.x + 1)
                    OnTriggerToReplace.Invoke(_numPlatform);
                yield return new WaitForFixedUpdate();
            }
        }

    }
}
