using UnityEngine;

public class GameManeger : MonoBehaviour
{
    private EnemySpwaner _enemySpawner;
    void Start()
    {
        _enemySpawner = GetComponent<EnemySpwaner>();
        _enemySpawner.StartWave();
    }
}
