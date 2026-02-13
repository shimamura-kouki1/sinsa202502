using UnityEngine;
/// <summary>
/// タワーの制御
/// </summary>
public class TowerController : MonoBehaviour
{
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _attackInterval = 3f;
    [SerializeField] private int _damage = 1;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _attackInterval) return;

        IDamageable target = FindTarget();
        if (target == null) return;

        _timer = 0f;
        target.TakeDamage(_damage);
    }

    private IDamageable FindTarget()
    {
        int bestProgress = -1;//アンカーと数を合わせるためのー1
        EnemyMoveCon bestEnemy = null;

        foreach (var enemy in EnemyManager.Instance.Enemies)
        {
            float sqr =
                  (enemy.transform.position - transform.position).sqrMagnitude;//敵との距離計算

            if (sqr > _range * _range) continue;//射程外ならスキップ

            if (enemy.CurrentNodeIndex > bestProgress)//ゴールに近い敵を選ぶ
            {
                bestProgress = enemy.CurrentNodeIndex;
                bestEnemy = enemy;
            }
        }
        if (bestEnemy == null) return null;

        return bestEnemy.Damageable;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
#endif
}
