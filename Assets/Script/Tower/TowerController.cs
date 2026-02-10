using UnityEngine;
/// <summary>
/// É^ÉèÅ[ÇÃêßå‰
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
        Debug.Log($"Enemies Count: {EnemyManager.Instance.Enemies.Count}");
        foreach (var enemy in EnemyManager.Instance.Enemies)
        {
            float sqr =
                (enemy.transform.position - transform.position).sqrMagnitude;

            if (sqr > _range * _range) continue;
            Debug.Log("Target Found!");
            return enemy.GetComponent<IDamageable>();
        }
        return null;
    }
}
