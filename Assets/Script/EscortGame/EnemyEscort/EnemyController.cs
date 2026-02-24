using System;
using UnityEngine;

/// <summary>
/// 敵の追尾制御
/// </summary>
public class EnemyController : MonoBehaviour
{

    private float _moveSpeed;
    private float _damage;

    private EnemyDataEscortGame _data;
    private Transform _target;
    private Transform _tr;
    private EnemyHelth _enemyHelth;

    public event Action<EnemyController> OnDied;
    public event Action<EnemyController> OnReachedGoal;

    /// <summary>
    /// ターゲットを設定
    /// </summary>
    /// <param name="target"></param>
    public void Initialize(EnemyDataEscortGame data, Transform target,ObjectPool pool)
    {
        _data = data;
        _target = target;
        _moveSpeed = data.moveSpeed;
        _damage = data.damage;

        _enemyHelth = GetComponent<EnemyHelth>();
        _enemyHelth.Initialize(data.maxHP,_data,pool);

        _enemyHelth.OnDeathEnemy -= HandleDeath;
        _enemyHelth.OnDeathEnemy += HandleDeath;
    }

    private void Awake()
    {
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
        Vector3 direction = (_target.position - _tr.position).normalized;

        _tr.forward = direction;

        _tr.position += direction * _moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_target == null) return;

        if (other.transform == _target)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
            _enemyHelth.TakeDamage(9999f);
        }
    }
    private void HandleDeath()
    {
        if (_data.deathEffectPrefab != null)
        {
            Instantiate(_data.deathEffectPrefab, transform.position, Quaternion.identity);
        }

        DeathSE();

        OnDied?.Invoke(this);
    }

    private void DeathSE()
    {
        if (_data.deathSE != null)
        {
            AudioManager.Instance.PlaySE(_data.deathSE);
        }
    }

}
