using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform _escortTarget;

    [Header("Score Settings")]
    [SerializeField] private float _distanceMultiplier = 1f;//距離の倍率

    private Vector3 _startPosition;//開始位置
    private float _distanceScore;//距離スコア
    private int _enemyScore;//撃破スコア

    private int _lastNotifiedScore;
    public event Action<int> OnTotalScoreChanged;//スコア更新時のイベント

    void Start()
    {
        _startPosition = _escortTarget.position;

        NotifyScoreChanged();
    }

    void Update()
    {
        UpdateDistanceScore();
    }

    /// <summary>
    /// 進行距離からスコアを計算
    /// </summary>
    private void UpdateDistanceScore()
    {
        float distance = _escortTarget.position.z - _startPosition.z;
        _distanceScore = Mathf.Max(0, distance) * _distanceMultiplier; // マイナス防止

        int total = GetTotalScore();

        NotifyScoreChanged(); 
    }

    /// <summary>
    /// 撃破スコアの加算
    /// </summary>
    /// <param name="score"></param>
    public void AddEnemyScore(int score)
    {
        _enemyScore += score;
    }

    /// <summary>
    /// 合計スコアの取得
    /// </summary>
    /// <returns></returns>
    public int GetTotalScore()
    {
        return Mathf.RoundToInt(_distanceScore) + _enemyScore;
    }

    /// <summary>
    /// 撃破スコアの取得
    /// </summary>
    /// <returns></returns>
    public int GetEnemyScore()
    {
        return _enemyScore;
    }

    /// <summary>
    /// 距離スコアの取得
    /// </summary>
    /// <returns></returns>
    public int GetDistanceScore()
    {
        return Mathf.RoundToInt(_distanceScore);
    }

    private void NotifyScoreChanged()
    {
        int currentTotal = GetTotalScore();

        // 前回と違う場合のみ通知
        if (currentTotal != _lastNotifiedScore)
        {
            _lastNotifiedScore = currentTotal;

            // イベント通知
            OnTotalScoreChanged?.Invoke(currentTotal);
        }
    }
}
