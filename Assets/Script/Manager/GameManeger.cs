using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BaseHealth _baseHealth;
    [SerializeField] private EscortMover _escort;
    [SerializeField] private ScoreManager _scoreManager;

    [SerializeField] private GameObject _clearPanel;
    [SerializeField] private GameObject _gameOverPanel; 
    [SerializeField] private TextMeshProUGUI _clearScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;


    private bool _isGameEnd;//護衛対象の死亡とゴールが同時になることを防ぐ

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }
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
        enemy.OnDiedWithScore -= HandleEnemyScore;

        enemy.OnReachedGoal += HandleEnemyReachedGoal;
        enemy.OnDied += HandleEnemyDied;
        enemy.OnDiedWithScore += HandleEnemyScore;
    }
    private void HandleGameClear()
    {
        if (_isGameEnd) return;

        _isGameEnd = true;
        _clearPanel.SetActive(true);
        Time.timeScale = 0f;
        ShowScore();
        Debug.Log("GAME CLEAR");
    }

    private void HandleGameOver()
    {
        if (_isGameEnd) return;

        _isGameEnd = true;
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        ShowScore();
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
        enemy.OnDiedWithScore -= HandleEnemyScore;
    }

    private void HandleEnemyReachedGoal(EnemyController enemy)
    {
        enemy.OnReachedGoal -= HandleEnemyReachedGoal;
        _baseHealth.TakeDamage(1);
    }

    private void HandleEnemyScore(int score)
    {
        _scoreManager.AddEnemyScore(score);
    }
    private void ShowScore()
    {
        int totalScore = _scoreManager.GetTotalScore();

        _clearScoreText.text = "SCORE : " + totalScore;
        _gameOverScoreText.text = "SCORE : " + totalScore;
    }
}