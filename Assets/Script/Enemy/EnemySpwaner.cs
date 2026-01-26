using Unity.VisualScripting;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] private int _SpwanCount;
    private int _arriveEnemyCount;

    [SerializeField] Transform _player;

    private GameObject[] _enemyList;

    private void Start()
    {
        for (int i = 0; i < _SpwanCount; i++)
        {
            var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// エネミーを生成＋死亡通知に登録
    /// </summary>
    public void StartWave()
    {
        _arriveEnemyCount = _SpwanCount;

        for (int i = 0; i < _SpwanCount; i++)
        {
            var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyAi>().TargetSet(_player);
            enemy.GetComponent<Health>().OnDeath += OnEnemyDeath;
            //後々エネミーを生成ではなく場所移動で使いまわしたい
        }
    }
    /// <summary>
    ///　Wave終了後の処理
    /// </summary>
    private void OnEnemyDeath()
    {
        _arriveEnemyCount--;

        if (_arriveEnemyCount <= 0)
        {
            Debug.Log("GameClear");
        }
    }
}
