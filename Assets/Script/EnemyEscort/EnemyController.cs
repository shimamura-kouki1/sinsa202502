using UnityEngine;

/// <summary>
/// 敵の追尾制御
/// </summary>
public class EnemyController : MonoBehaviour
{
    private float _moveSpeed;
    private float _damage;

    private EnemyData _data;
    private Transform _target;
    private Transform _tr;
    private EnemyHelth _enemyHelth;
    /// <summary>
    /// ターゲットを設定
    /// </summary>
    /// <param name="target"></param>
    public void Initialize(EnemyData data, Transform target)
    {
        _data = data;
        _target = target;
        _moveSpeed = data.moveSpeed;
        _damage = data.damage;
        _enemyHelth = GetComponent<EnemyHelth>();
        _enemyHelth.Initialize(data.maxHP);
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
}
