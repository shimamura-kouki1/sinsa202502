using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _hp = 100;

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        Debug.Log(_hp);
        if (_hp <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Dei");
    }
}
