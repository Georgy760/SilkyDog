using Common.Scripts.ManagerService; 
using System.Collections; 
using UnityEngine;
using Zenject; 

namespace Assets.Player
{
    class StartScene : MonoBehaviour
    {
         
        private SessionService _session;
        [SerializeField] float _speed;
        private bool _stop = false;

        private Vector3 _startPos;
        [Inject]
        private void Constuct(ISessionService service)
        {
            _session = (SessionService)service;
            _session.OnRestartSession += Starts;
            _startPos = transform.localPosition;
        } 
        private void OnDestroy()
        {
            _session.OnRestartSession -= Starts;  
        }
        private void Start()
        {
            StartCoroutine(StartCatscene());
        }
        void Starts()
        {
            transform.localPosition = _startPos;
            if (!_stop)
            { 
                
                StartCoroutine(StartCatscene());
            }
        }
        IEnumerator StartCatscene()
        { 
            _stop  = true;
            while (true)
            { 
                if (transform.localPosition.x >= 0f) break; 
                float newX = Mathf.Lerp(transform.localPosition.x, 0 + 1f, _speed * Time.deltaTime);
                 
                Vector3 currentPosition = transform.localPosition;
                 
                currentPosition.x = newX;
                 
                transform.localPosition = currentPosition;
                yield return new WaitForFixedUpdate();
            }
            _stop = false;
            _session.StartGame();  
        }
    }
}
