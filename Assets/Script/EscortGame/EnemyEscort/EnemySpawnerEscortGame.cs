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
        //後方方向のベクトル
        Vector3 backDir = -_target.forward;

        //基準の角度
        float baseAngle = Mathf.Atan2(backDir.x, backDir.z) * Mathf.Deg2Rad;

        //扇形の角度をラジアンに
        float halfAngleRad = _spawnAngle * Mathf.Deg2Rad;

        //ランダムの角度
        float randomAngle = baseAngle + Random.Range(-halfAngleRad,halfAngleRad);

        //ランダム距離
        float distance = Random.Range(_minSpawnDistance, _maxSpawnDistance);

        Vector3 dir = new Vector3(Mathf.Sin(randomAngle), 0f, Mathf.Cos(randomAngle));

        return _target.position + dir * distance;
    }
}
