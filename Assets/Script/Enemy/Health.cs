using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyDate _enemyDate;

    private float _hp;

    private void Start()
    {
        _hp = _enemyDate._maxHp;
    }
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
        transform.position =  _enemyDate._despoilAnchor.position;
        Debug.Log("Dei");
    }
}
