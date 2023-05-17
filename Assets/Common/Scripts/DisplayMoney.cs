using Common.Scripts.ManagerService;
using Common.Scripts;
using UnityEngine;
using Zenject;

public class DisplayMoney : MonoBehaviour
{ 
    [SerializeField] private TMPro.TMP_Text text;
    private IAudioService _audioService;
    private ISessionService _sessionService;
    [Inject]
    void Construct(ISessionService service, IAudioService audioService)
    {
        _sessionService = service;
        _audioService = audioService;
    }
    private void Awake()
    {
        Coin.OnPickUpCoin += AddCoin;
    }
    private void AddCoin()
    {
        _sessionService.money++;
        text.text = _sessionService.money.ToString();
        _audioService.PlaySound(Common.Scripts.AudioType.COIN); 
    }
}
