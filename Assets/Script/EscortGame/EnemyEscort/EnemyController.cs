using System;
using UnityEngine;

/// <summary>
/// 敵の追尾制御
/// </summary>
public class EnemyController : MonoBehaviour
{

    private float _moveSpeed;
    private float _damage;

    private EnemyDataEscortGame _data;　// 敵のステータスデータ
    private Transform _target;          // 追尾対象
    private Transform _tr;
    private EnemyHealth _enemyHelth;

    /// <summary>
    /// GameManagerが購読
    /// </summary>
    public event Action<EnemyController> OnDied;//死亡通知
    public event Action<EnemyController> OnReachedGoal;//ゴール通知（護衛対象との接触通知）使用してない
    public event Action<int> OnDiedWithScore;//スコア通知用

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="target"></param>
    public void Initialize(EnemyDataEscortGame data, Transform target,ObjectPool pool)
    {
        _data = data;
        _target = target;

        //データからパラメーターを取得
        _moveSpeed = data.moveSpeed;
        _damage = data.damage;

        _enemyHelth = GetComponent<EnemyHealth>();

        //HPの初期化
        _enemyHelth.Initialize(data.maxHP,_data,pool);

        _enemyHelth.OnDeathEnemy -= HandleDeath;
        _enemyHelth.OnDeathEnemy += HandleDeath;
    }

    private void Awake()
    {
        // transformは頻繁に使うのでキャッシュ
        _tr = transform;
    }
    
    void Update()
    {
        if (_target == null) return;

        MoveTowardsTarget();
    }

    /// <summary>
    /// ターゲットの方向移動
    /// </summary>
    private void MoveTowardsTarget()
    {
        //進行方向を計算
        Vector3 direction = (_target.position - _tr.position).normalized;

        //向きをターゲットに
        _tr.forward = direction;
        
        //前進
        _tr.position += direction * _moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// ゴール（護衛対象）に接触したときの処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (_target == null) return;

        if (other.transform == _target)
        {
            //ターゲットにダメージを与える
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
            //自身にダメージで死亡させる
            _enemyHelth.TakeDamage(9999f);
        }
    }
    /// <summary>
    /// 死亡時の処理
    /// </summary>
    private void HandleDeath()
    {
        //死亡のエフェクトを生成
        if (_data.deathEffectPrefab != null)
        {
            Instantiate(_data.deathEffectPrefab, transform.position, Quaternion.identity);
        }
        //スコア通知
        OnDiedWithScore?.Invoke(_data.score);

        //死亡SEの再生
        DeathSE();

        //外部への死亡通知
        OnDied?.Invoke(this);
    }

    /// <summary>
    /// 死亡SE再生（AudioManager経由）
    /// </summary>
    private void DeathSE()
    {
        if (_data.deathSE != null)
        {
            AudioManager.Instance.PlaySE(_data.deathSE);
        }
    }

}
