using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseHealth _baseHealth;
    [SerializeField] private EscortMover _escort;

    private void OnEnable()
    {
        _escort.OnReachedGoal += HandleGameClear;
        _escort.OnEscortDead += HandleGameOver;
    }

    private void OnDisable()
    {
        _escort.OnReachedGoal -= HandleGameClear;
        _escort.OnEscortDead -= HandleGameOver;
    }
    public void RegisterEnemy(EnemyController enemy)
    {
        //二重に登録されるのを防ぐため
        enemy.OnReachedGoal -= HandleEnemyReachedGoal;
        enemy.OnDied -= HandleEnemyDied;

        enemy.OnReachedGoal += HandleEnemyReachedGoal;
        enemy.OnDied += HandleEnemyDied;
    }
    private void HandleGameClear()
    {
        Debug.Log("GAME CLEAR");
    }

    private void HandleGameOver()
    {
        Debug.Log("GAME OVER");
    }

    
    private void HandleEnemyDied(EnemyController enemy)
    {
        CleanupEnemy(enemy);
    }
    private void CleanupEnemy(EnemyController enemy)
    {
        enemy.OnReachedGoal -= HandleEnemyReachedGoal;
        enemy.OnDied -= HandleEnemyDied;
    }
    private void HandleEnemyReachedGoal(EnemyController enemy)
    {
        enemy.OnReachedGoal -= HandleEnemyReachedGoal;
        _baseHealth.TakeDamage(1);
    }
}