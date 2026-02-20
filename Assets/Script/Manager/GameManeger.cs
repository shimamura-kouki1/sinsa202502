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

    private void HandleGameClear()
    {
        Debug.Log("GAME CLEAR");
        Time.timeScale = 0f;
    }

    private void HandleGameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }

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