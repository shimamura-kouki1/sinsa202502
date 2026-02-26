using System.Collections;
using UnityEngine;

/// <summary>
/// 敵の生成管理
/// </summary>
public class EnemySpawnerEscortGame : MonoBehaviour
{
    [SerializeField] private EnemyDataEscortGame[] _enemyTypes; //出現させるエネミーの種類のリスト
    [SerializeField] private Transform _target;                 // 敵が向かうターゲット
    [SerializeField] private Transform _spawnPoint;             // 基準となるスポーン位置
    [SerializeField] private ObjectPool _pool;                  // オブジェクトプール参照
    [SerializeField] private GameManager _gameManager;          // 生成した敵を登録するGameManager

    [Header("スポーン距離")]
    [SerializeField] private float _minSpawnDistance = 8f;   // 最小距離
    [SerializeField] private float _maxSpawnDistance = 12f;  // 最大距離
    [SerializeField] private float _spawnAngle = 50f;        // 扇の半角

    [SerializeField] private float _startDelay = 1f;         // ゲーム開始から最初のスポーンまでの待ち時間
    [SerializeField] private float _spawnInterval = 1.5f;    // 敵を生成する間隔

    private Coroutine _spawnCoroutine;

    void Start()
    {
        // 敵生成ループ開始
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    /// <summary>
    /// 一定間隔で敵を生成する
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnRoutine()
    {
        //開始時の遅延
        yield return new WaitForSeconds(_startDelay);

        while (true)//ゲームが続く限り
        {
            SpawnEnemy();

            // 次のスポーンまで待つ
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
    
    /// <summary>
    /// 敵の生成
    /// </summary>
    private void SpawnEnemy()
    {
        //ランダムでEnemyDataを取得
        EnemyDataEscortGame data = _enemyTypes[Random.Range(0,_enemyTypes.Length)];

        //出現一の取得
        Vector3 spawnPos = RandomSpawnPositon();

        //プールから取得
        GameObject obj = _pool.Spawn(data.prefab,spawnPos, Quaternion.identity);

        //EnemyController取得
        EnemyController enemy = obj.GetComponent<EnemyController>();

        //敵の初期化
        enemy.Initialize(data, _target, _pool);

        //GameMangerに登録
        _gameManager.RegisterEnemy(enemy);
    }

    /// <summary>
    /// ターゲット前方の扇状からランダム位置を計算
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomSpawnPositon()
    {
        //角度をランダム決定
        float angle = Random.Range(-_spawnAngle, _spawnAngle);

        // ターゲットの前方向を基準にY軸を回転
        Vector3 dir = Quaternion.Euler(0f, angle, 0f) * _target.forward;

        //距離をランダムに決定
        float distance = Random.Range(_minSpawnDistance, _maxSpawnDistance);

        //スポーン位置を最終的に返す
        return _target.position + dir * distance;
    }
}
