using UnityEngine;
/// <summary>
/// UI管理　現在使用していない
/// </summary>
public class UIManager : MonoBehaviour
{
     private Health _health;

    public void SetHealth(Health health)
    {
        _health = health;
        _health.OnHealthDamaged += OnDamaged;
        _health.OnDeath += OnDeath;
    }
    private void OnDestroy()
    {
        _health.OnHealthDamaged -= OnDamaged;
        _health.OnDeath -= OnDeath;
    }

    public void OnDeath()
    {
        Debug.Log("Enemy Dead");
    }

    public void OnDamaged(float CurrentHP,float maxHP)
    {
        Debug.Log(CurrentHP);
    }

}
