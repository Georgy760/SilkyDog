using System;
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
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerToReplace?.Invoke(_numPlatform);
        }

    }
}
