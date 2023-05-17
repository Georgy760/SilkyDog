using Common.GameManager.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseButton : MonoBehaviour
{
    Button _button;

    IGameManager _gameManager;
    [Inject]
    void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PauseTap);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(PauseTap);
    }
    private void PauseTap()
    {
        _gameManager.TogglePause();
    }

}
