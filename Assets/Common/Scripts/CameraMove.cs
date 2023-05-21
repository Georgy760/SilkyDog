using Common.Scripts.ManagerService;
using System.Collections; 
using UnityEngine;
using Zenject; 

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _smoothSpeed = 0.125f;
    private float _offsetY;
    private Vector3 _StartPos;
    private bool _isStop = true;
    [Inject]
    private void Constructor(ISessionService service)
    {
        service.OnStartRun += () =>
        {
            _isStop = true;
            StartCoroutine(StartChaseOnPlayer());
        };
        service.OnEndRun += () =>
            _isStop = false; 
     
        service.OnRestartSession += RestartGame;
        _StartPos = transform.position;
    }
    private void Awake()
    {
        _offsetY = transform.position.y - _player.position.y; 
    }
    private void RestartGame()
    {
        StopCoroutine(StartChaseOnPlayer());
        transform.position = _StartPos;
    }
    private IEnumerator StartChaseOnPlayer()
    {
        while (_isStop)
        {
             
            Vector3 desiredPosition = new Vector3(_player.position.x + _offsetX, transform.position.y, transform.position.z); 
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed); 
            transform.position = smoothedPosition; 
            yield return new WaitForFixedUpdate();
        }
    }
}
