using Common.Scripts;
using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private int _addCoin = 100;

    private IAudioService _audioService;
    private ISessionService _sessionService; 
    [Inject]
    void Construct(ISessionService service, IAudioService audioService)
    {
        _sessionService = service;
        _audioService = audioService;
        _sessionService.OnLevelChange += AddCoinForEndLevel;
    }

    private void Awake()
    {
        Coin.OnPickUpCoin += AddCoin;
        
    }

    private void OnDestroy()
    {
        _sessionService.OnLevelChange -= AddCoinForEndLevel;
        Coin.OnPickUpCoin -= AddCoin;
    }

    private void AddCoin()
    {
        _sessionService.money++;
        text.text = _sessionService.money.ToString();
        _audioService.PlaySound(Common.Scripts.AudioType.COIN);
    }

    private void AddCoinForEndLevel(LevelType level)
    {
        _sessionService.money += _addCoin;
    } 
}