using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Health _health;

    private void OnEnable()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += Die;
    }
    private void OnDisable()
    {
        _health.OnDeath -= Die;
    }

    private void Die()
    {
        Debug.Log("GameOver");
    }

}