using UnityEngine;
using Zenject;

public class GeneratorMoney : MonoBehaviour
{
    private GameObject _moneyObjects;
    private GameObject _prevMoney;
    IServiceGeneratorEnemy _generatorEnemy;
    [Inject]
    void Construct(IServiceGeneratorEnemy generatorEnemy)
    {
        _generatorEnemy = generatorEnemy;
        _generatorEnemy.OnSpawnMoney += SpawnMoney;
        _moneyObjects = generatorEnemy.moneyPrefab;
    }

    private void OnDestroy()
    {
        _generatorEnemy.OnSpawnMoney -= SpawnMoney;
    }

    private void SpawnMoney(Vector3 cameraPos)
    {
        if (_prevMoney != null)
            Destroy(_prevMoney.gameObject);
        _prevMoney = Instantiate(_moneyObjects, cameraPos, Quaternion.identity);
    }
}
