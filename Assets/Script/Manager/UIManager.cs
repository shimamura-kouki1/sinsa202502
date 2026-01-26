using UnityEngine;

public class UIManager : MonoBehaviour
{
     private Health _health;
    public void SetHealth(Health health)
    {
        _health = health;
        _health.OnDamaged += OnDamaged;
        _health.OnDeath += OnDeath;
    }
    private void OnDestroy()
    {
        _health.OnDamaged -= OnDamaged;
        _health.OnDeath -= OnDeath;
    }

    public void OnDeath()
    {
        Debug.Log("Enemy Dead");
    }

    public void OnDamaged(float damage)
    {
        Debug.Log(damage);
    }

}
