using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseHealth _baseHealth;
    public void RegisterEnemy(EnemyMoveCon enemy)
    {
        enemy.OnReachedGoal += HandleEnemyReachedGoal;
    }

    private void HandleEnemyReachedGoal(EnemyMoveCon enemy)
    {
        enemy.OnReachedGoal -= HandleEnemyReachedGoal;
        _baseHealth.TakeDamage(1);
    }
}