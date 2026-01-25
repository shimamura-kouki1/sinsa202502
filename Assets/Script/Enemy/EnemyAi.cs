using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private EnemyDate _enemyDate;
    private float _moveSpeed;
    private float _attackRenge;
    private float _attakcDamage;

    private Transform _playerPos;
    void Start()
    {
        _moveSpeed = _enemyDate._moveSpeed;
        _attackRenge = _enemyDate._attacRenge;
        _attakcDamage = _enemyDate._attackValue;
    }
    void Update()
    {
        
    }
}
