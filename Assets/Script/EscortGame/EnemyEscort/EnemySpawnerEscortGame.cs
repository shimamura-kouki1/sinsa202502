using System.Collections;
using UnityEngine;

/// <summary>
/// 敵の生成管理
/// </summary>
public class EnemySpawnerEscortGame : MonoBehaviour
{
    [SerializeField] private EnemyDataEscortGame[] _enemyTypes;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private GameManager _gameManager;

    [Header("スポーン距離")]
    [SerializeField] private float _minSpawnDistance = 8f;   // 最小距離
    [SerializeField] private float _maxSpawnDistance = 12f;  // 最大距離
    [SerializeField] private float _spawnAngle = 50f;        // 扇の半角

    [SerializeField] private float _startDelay = 1f;
    [SerializeField] private float _spawnInterval = 1.5f;

    private Coroutine _spawnCoroutine;

    void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
        Debug.DrawRay(_target.position, _target.forward * 5f, Color.blue, 2f);
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_startDelay);
        while (true)
        {
            SpawnEnemy();

            // 次のスポーンまで待つ
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        //ランダムでEnemyDataを取得
        EnemyDataEscortGame data = _enemyTypes[Random.Range(0,_enemyTypes.Length)];

        Vector3 spawnPos = RandomSpawnPositon();

        //プールから取得
        GameObject obj = _pool.Spawn(data.prefab,spawnPos, Quaternion.identity);

        EnemyController enemy =
        obj.GetComponent<EnemyController>();

        enemy.Initialize(data, _target, _pool);
        //初期化
        //obj.GetComponent<EnemyController>()
        //   .Initialize(data, _target, _pool);

        _gameManager.RegisterEnemy(enemy);
    }

    private Vector3 RandomSpawnPositon()
    {
        float angle = Random.Range(-_spawnAngle, _spawnAngle);

        // 前方向を基準に回転
        Vector3 dir = Quaternion.Euler(0f, angle, 0f) * _target.forward;

        float distance = Random.Range(_minSpawnDistance, _maxSpawnDistance);

        return _target.position + dir * distance;
    }
}
