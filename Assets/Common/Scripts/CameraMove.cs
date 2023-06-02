using System;
using Common.Scripts.ManagerService;
using System.Collections; 
using UnityEngine;
using Zenject; 

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _StartdeltaX = 5f; 
    [SerializeField] private float _deltaX;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxDeltaX = 10f;
    private float _levelTime = 0f;
    private Vector3 _StartPos;
    private bool _stop = true;
    
    private ISessionService _sessionService;
    
    [Inject]
    private void Constructor(ISessionService sessionService)
    {
        _sessionService = sessionService;
        _sessionService.OnStartRun += OnStartRun;
        _sessionService.OnEndRun += OnEndRun;
        _sessionService.OnRestartSession += RestartGame;
        
        _StartPos = transform.position;
    }

    private void OnDestroy()
    {
        _sessionService.OnStartRun -= OnStartRun;
        _sessionService.OnEndRun -= OnEndRun;
        _sessionService.OnRestartSession -= RestartGame;
    }

    private void OnStartRun()
    {
        _stop = true;
        _deltaX = _StartdeltaX;
        _levelTime = 0f;
        StartCoroutine(StartRun());
    }
    private void OnEndRun()
    { 
        _stop = false;
    }
    private IEnumerator StartRun()
    {
        while (_stop)
        {
            _levelTime += Time.deltaTime;
            if (_levelTime >= 5 && _maxDeltaX > _deltaX)
            {
                _deltaX++;
                _levelTime = 0f;
            }
            float NewX = Mathf.Clamp(_speed * Time.deltaTime, _deltaX * Time.deltaTime, _deltaX + _speed * Time.deltaTime);
            transform.position += new Vector3(NewX, 0,0);

            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
    
    private void RestartGame()
    { 
        transform.position = _StartPos;
        _stop = false; 
    } 
}
