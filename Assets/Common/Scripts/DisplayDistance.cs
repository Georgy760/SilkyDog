using Common.Scripts.ManagerService;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DisplayDistance : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private Transform player;
    [SerializeField] private int _distanceBettwenLevel = 50;
    private int _curretLevelToChange = 50;
    bool _isStop = true;
    private ISessionService _service;
    public static event Action OnChangeLevel;
    [Inject]
    void Construct(ISessionService service)
    {
        _service = service;

        _service.OnStartRun += StartGame;
        _service.OnEndRun += EndGame;
    }
    private void OnDestroy()
    {
        _service.OnStartRun -= StartGame;
        _service.OnEndRun -= EndGame;
    }
    private void StartGame()
    {
        _isStop = true;
        _curretLevelToChange = _distanceBettwenLevel;
        StartCoroutine(StartRun());
    }

    private void EndGame()
    {
        _isStop = false;
    }
    IEnumerator StartRun()
    {
        int x = (int)player.position.x;
        while (_isStop)
        {
            _service.record = (int)player.position.x - x;
            _text.text = "Distance:" + _service.record.ToString();
            //Debug.Log(_curretLevelToChange);
            if ((float)_service.record / (float)_curretLevelToChange >= 1)
            {
                Debug.Log("Chaneg level");
                OnChangeLevel?.Invoke();
                _curretLevelToChange += _distanceBettwenLevel;
            }
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
