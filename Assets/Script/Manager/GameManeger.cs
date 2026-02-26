using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームクリア/ゲームオーバーの判定
/// 敵のイベント管理
/// スコア表示管理
/// 後々この３つはスクリプト分けたい
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("参照")]
    [SerializeField] private BaseHealth _baseHealth;
    [SerializeField] private EscortMover _escort;
    [SerializeField] private ScoreManager _scoreManager;

    [Header("クリアUI")]//これ後々分けたい
    [SerializeField] private GameObject _clearPanel;
    [SerializeField] private TextMeshProUGUI _clearScoreText;
    [SerializeField] private TextMeshProUGUI _clearEnemyScoreText;
    [SerializeField] private TextMeshProUGUI _clearDistanceScoreText;

    [Header("ゲームオーバーUI")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverEnemyScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverDistanceScoreText;

    [Header("ゲーム中スコアUI")]
    [SerializeField] private TextMeshProUGUI _inGameTotalScoreText;

    [Header("シーン遷移設定")]
    [SerializeField] private float _returnToTitleDelay = 2f; // タイトルに戻るまでの秒数
    [SerializeField] private string _titleSceneName = "Tetel"; 


    private bool _isGameEnd;//護衛対象の死亡とゴールが同時になることを防ぐ

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }
    private void OnEnable()
    {
        // 護衛対象イベント登録
        _escort.OnReachedGoal += HandleGameClear;
        _escort.OnEscortDead += HandleGameOver;

        // スコア変化イベント登録
        _scoreManager.OnTotalScoreChanged += UpdateInGameScore;
    }

    private void OnDisable()
    {
        //イベント解除
        _escort.OnReachedGoal -= HandleGameClear;
        _escort.OnEscortDead -= HandleGameOver;
        _scoreManager.OnTotalScoreChanged -= UpdateInGameScore;
    }

    /// <summary>
    /// 敵生成時の登録
    /// </summary>
    /// <param name="enemy"></param>
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

    // =========================
    // ゲーム終了処理
    // =========================

    private void HandleGameClear()
    {
        if (_isGameEnd) return;

        _isGameEnd = true;
        EndGame();
        _clearPanel.SetActive(true);
        ShowScore(_clearEnemyScoreText,_clearDistanceScoreText,_clearScoreText);

        StartCoroutine(ReturnToTitleAfterDelay());
    }

    private void HandleGameOver()
    {
        if (_isGameEnd) return;

        _isGameEnd = true;
        EndGame();
        _gameOverPanel.SetActive(true);
        ShowScore(_gameOverEnemyScoreText,_gameOverDistanceScoreText,_gameOverScoreText);

        StartCoroutine(ReturnToTitleAfterDelay());
    }

    // =========================
    // 敵関連イベント
    // =========================
    /// <summary>
    /// 敵死亡時のイベント解除
    /// </summary>
    /// <param name="enemy"></param>
    private void HandleEnemyDied(EnemyController enemy)
    {
        enemy.OnReachedGoal -= HandleEnemyReachedGoal;
        enemy.OnDied -= HandleEnemyDied;
        enemy.OnDiedWithScore -= HandleEnemyScore;
    }

    /// <summary>
    /// 敵が護衛対象に到達
    /// </summary>
    /// <param name="enemy"></param>
    private void HandleEnemyReachedGoal(EnemyController enemy)
    {
        enemy.OnReachedGoal -= HandleEnemyReachedGoal;
        _baseHealth.TakeDamage(1);
    }

    /// <summary>
    /// 敵撃破スコア加算
    /// </summary>
    private void HandleEnemyScore(int score)
    {
        _scoreManager.AddEnemyScore(score);
    }

    // =========================
    // スコア表示処理
    // =========================
    private void ShowScore(TextMeshProUGUI enemyText,TextMeshProUGUI distanceText,TextMeshProUGUI totalText)
    {
        int totalScore = _scoreManager.GetTotalScore();
        int enemyScore = _scoreManager.GetEnemyScore();
        int distanceScore = _scoreManager.GetDistanceScore();

        enemyText.text = "ENEMY : " + enemyScore;
        distanceText.text = "DISTANCE : " + distanceScore;
        totalText.text = "SCORE : " + totalScore;
    }

    private void UpdateInGameScore(int totalScore)
    {
        if (_isGameEnd) return;

        _inGameTotalScoreText.text = "SCORE : " + totalScore;
    }
    /// <summary>
    /// ゲーム終了時での共通動作
    /// </summary>
    private void EndGame()
    {
        _inGameTotalScoreText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private IEnumerator ReturnToTitleAfterDelay()
    {
        // Time.timeScaleの影響を受けないらしい
        yield return new WaitForSecondsRealtime(_returnToTitleDelay);

        // 一応あったほうがいいらしい
        Time.timeScale = 1f;

        // タイトルシーンへ遷移
        SceneManager.LoadScene(_titleSceneName);
    }
}