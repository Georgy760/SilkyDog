using Common.Scripts.ManagerService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Scripts 
{ 
    public class ParallaxEffect : MonoBehaviour
    {
        public float speed = 0; //эта публичная переменная отобразится в инспекторе, там же мы ее можем и менять. Это очень удобно, чтобы настроить скорость разным слоям картинки

        float pos = 0; //переменная для позиции картинки

        private RawImage image; //создаем объект нашей картинки

        private bool _isStop = true;

        [Inject]
        void Construct(ISessionService service)
        {
            service.OnStartRun += () =>
            {
                _isStop = true;
                StartCoroutine(StartRun());
            };
            service.OnEndRun += () =>
            {
                _isStop = false;
                StopCoroutine(StartRun());
            };
        }

        void Start()
        {

            image = GetComponent<RawImage>();//в старте получаем ссылку на картинку

        }



        IEnumerator StartRun()
        {

            //в апдейте прописываем как, с какой скоростью и куда мы будем двигать нашу картинку
            while (_isStop)
            {
                pos += speed * Time.deltaTime;

                if (pos > 1.0F)
                    pos -= 1.0F;

                image.uvRect = new Rect(pos, 0, 1, 1);
                yield return new WaitForFixedUpdate();
            }
            
        }

    }
}
