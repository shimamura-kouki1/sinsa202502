using System.Data.Common;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Enemyの移動と攻撃
/// </summary>
public class EnemyAi : MonoBehaviour
{
    [SerializeField] private EnemyDate _enemyDate;
    private float _moveSpeed;
    private float _attackRenge;
    private float _attakcDamage;

    private Transform _playerPos;

    /// <summary>
    /// プレイヤーを注入される
    /// </summary>
    /// <param name="player"></param>
    public void TargetSet(Transform player)
    {
        _playerPos = player;
    }
    void Start()
    {
        _moveSpeed = _enemyDate._moveSpeed;
        _attackRenge = _enemyDate._attacRenge;
        _attakcDamage = _enemyDate._attackValue;
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _playerPos.position);

        if (distance > _attackRenge)
        {
            MoveToTarget();
        }
        else
        {
            Attack();
        }

    }
    /// <summary>
    /// ターゲットへの移動
    /// </summary>
    private void MoveToTarget()
    {
        Vector3 direction = (transform.position - _playerPos.position).normalized;
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }
    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        if(_playerPos.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_attakcDamage);
        }
    }
    }
