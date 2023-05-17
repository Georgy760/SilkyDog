using Common.Scripts.ManagerService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Player
{
    class StartScene : MonoBehaviour
    {
         
        private SessionService _session;
        [SerializeField] float _speed;
        [SerializeField] Transform _target;
        [Inject]
        private void Constuct(ISessionService service)
        {
            _session = (SessionService)service;
            _session.OnRestartSession += Start;
        }
        void Start()
        {
            StartCoroutine(StartCatscene());
        }
        IEnumerator StartCatscene()
        { 
            while (true)
            {
                if (transform.position.x >= _target.position.x) break;
                // Вычисляем новую позицию объекта только по оси X, используя интерполяцию
                float newX = Mathf.Lerp(transform.position.x, _target.position.x + 1f, _speed * Time.deltaTime);

                // Получаем текущую позицию объекта по остальным осям
                Vector3 currentPosition = transform.position;

                // Обновляем только позицию по оси X
                currentPosition.x = newX;

                // Применяем новую позицию к объекту
                transform.position = currentPosition;
                yield return new WaitForFixedUpdate();
            }
            _session.StartGame();
        }
    }
}
