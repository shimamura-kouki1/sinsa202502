using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform _escortTarget;

    [Header("Score Settings")]
    [SerializeField] private float _distanceMultiplier = 1f;
    [SerializeField] private int _killScore = 100;

    private Vector3 _startPosition;
    private float _distanceScore;
    private int _enemyScore;

    void Start()
    {
        _startPosition = _escortTarget.position;
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
    }
    public void AddEnemyScore(int score)
    {
        _enemyScore += score;
    }
    public int GetTotalScore()
    {
        return Mathf.RoundToInt(_distanceScore) + _enemyScore;
    }
    public int GetEnemyScore()
    {
        return _enemyScore;
    }
    public int GetDistanceScore()
    {
        return Mathf.RoundToInt(_distanceScore);
    }
}
