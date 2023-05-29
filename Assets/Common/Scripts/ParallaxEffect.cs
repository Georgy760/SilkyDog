using Common.Scripts.ManagerService; 
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Scripts
{
    public class ParallaxEffect : MonoBehaviour
    {
        public float speed = 0;
        private float pos = 0;
        private RawImage _backImage;
        private bool _isStop = true;
        private Dictionary<LevelType, Texture2D> _backImages = new Dictionary<LevelType, Texture2D>();
        ISessionService _service;
        [Inject]
        void Construct(ISessionService service)
        {
            _service = service;
            _service.OnStartRun += StartMoveBackImage;
            _service.OnEndRun += EndMoveBackImage;
            _service.OnLevelChange += ChangeBackImage;

            foreach (ObstaclesScritableObjects obs in service.obstacles)
            {

                _backImages.Add(obs.levelType, obs.BackImage);
            }

            _backImage = GetComponent<RawImage>();
            ChangeBackImage(_service.levelType);
        }

        void StartMoveBackImage()
        {
            _isStop = true;
            StartCoroutine(StartRun());
        }
        void EndMoveBackImage()
        {
            _isStop = false;
            StopCoroutine(StartRun());
        }
  

        void ChangeBackImage(LevelType level)
        {
            Debug.Log(level.ToString());
            _backImage.texture = _backImages[level];
        }

        IEnumerator StartRun()
        {
            while (_isStop)
            {
                pos += speed * Time.deltaTime;

                if (pos > 1.0F)
                    pos -= 1.0F;

                _backImage.uvRect = new Rect(pos, 0, 1, 1);
                yield return new WaitForFixedUpdate();
            }

        }

    }
}
