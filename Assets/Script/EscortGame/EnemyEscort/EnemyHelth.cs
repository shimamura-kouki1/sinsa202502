using UnityEngine;

public class EnemyHelth : MonoBehaviour,IDamageable
{
    private float _currentHp;
    private EnemyDataEscortGame _data;
    private ObjectPool _pool;

    public void Initialize(float maxHp,EnemyDataEscortGame data,ObjectPool pool)
    {
        _data = data;
        _pool = pool;
        _currentHp = maxHp;
    }

    public void TakeDamage(float damege)
    {
        _currentHp -= damege;

        if(_currentHp <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _pool.Despawn(gameObject, _data.prefab);
    }
}
