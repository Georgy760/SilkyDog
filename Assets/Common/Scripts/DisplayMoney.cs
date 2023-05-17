using Common.Scripts.ManagerService;
using Common.Scripts;
using UnityEngine;
using Zenject;

public class DisplayMoney : MonoBehaviour
{
    private int _coin;
    [SerializeField] private TMPro.TMP_Text text;
    [Inject]
    void Construct(ISessionService service)
    {
        _coin = service.money;
    }
    private void Awake()
    {
        Coin.OnPickUpCoin += AddCoin;
    }
    private void AddCoin()
    {
        _coin++;
        text.text = _coin.ToString();
    }
}
