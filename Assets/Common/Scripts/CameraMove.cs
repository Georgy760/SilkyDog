using Common.Scripts.ManagerService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform _player;
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
    private void RestartGame()
    {
        StopCoroutine(StartChaseOnPlayer());
        transform.position = _StartPos;
    }
    private IEnumerator StartChaseOnPlayer()
    {
        while (_isStop)
        {
            transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
    }
}
