using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] private int _SpwanCount;
    private int _arriveEnemyCount;

    [SerializeField] Transform _player;

    /// <summary>
    /// エネミーを生成＋死亡通知に登録
    /// </summary>
    public void StartWave()
    {
        _arriveEnemyCount = _SpwanCount;

        for(int i = 0;i<_SpwanCount; i++)
        {
            var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyAi>().TargetSet(_player);
            enemy.GetComponent<Health>().OnDeath += OnEnemyDeath;
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
