using UnityEngine;

public class EnemyHelth : MonoBehaviour,IDamageable
{
    private float _currentHp;

    public void Initialize(float maxHp)
    {
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
        Destroy(gameObject);
    }
}
