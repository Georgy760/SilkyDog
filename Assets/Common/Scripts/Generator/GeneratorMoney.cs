using Common.Scripts.ManagerService;
using UnityEngine;
using Zenject;

public class GeneratorMoney : MonoBehaviour
{ 
    [SerializeField] private Transform _parentMoney;
    private GameObject _moneyObjects;
    private GameObject _prevMoney;

    IServiceGeneratorEnemy _generatorEnemy;
    ISessionService _sessionService;
    [Inject]
    void Construct(IServiceGeneratorEnemy generatorEnemy, ISessionService sessionService)
    {
        _generatorEnemy = generatorEnemy;
        _generatorEnemy.OnSpawnMoney += SpawnMoney;
        _moneyObjects = generatorEnemy.moneyPrefab;

        _sessionService = sessionService;
        _sessionService.OnRestartSession += DestroyMoney;
    }

    private void OnDestroy()
    {
        _generatorEnemy.OnSpawnMoney -= SpawnMoney;
        _sessionService.OnRestartSession -= DestroyMoney;
    }
    private void DestroyMoney()
    {
        if (_prevMoney != null)
            Destroy(_prevMoney.gameObject);
    }

    private void SpawnMoney(Vector3 cameraPos)
    {
        DestroyMoney();
        _prevMoney = Instantiate(_moneyObjects, cameraPos, Quaternion.identity,_parentMoney);
    }
}
