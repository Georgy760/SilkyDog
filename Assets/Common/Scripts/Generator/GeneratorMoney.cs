using UnityEngine;

public class GeneratorMoney : MonoBehaviour
{
    [SerializeField] private GameObject _moneyObjects;
    private GameObject _prevMoney;
    private void Awake()
    {
        GeneratorObstacles.OnSpawnEnemy += SpawnEnemy;
    }

    private void SpawnEnemy(Vector2 cameraPos)
    {
        if (_prevMoney != null)
            Destroy(_prevMoney.gameObject);
        _prevMoney = Instantiate(_moneyObjects, cameraPos, Quaternion.identity);
    }
}
