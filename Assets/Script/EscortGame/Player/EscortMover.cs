using System;
using UnityEngine;
/// <summary>
/// 護衛対象の移動制御
/// </summary>
public class EscortMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private WaypointNode _startNode;

    private WaypointNode _currentNode;
    private Transform _tr;
    private Health _health;

    public event Action OnReachedGoal;
    public event Action OnEscortDead;

    /// <summary>
    /// 最終ノード到達時に通知するイベント
    /// </summary>
    private void Awake()
    {
        _tr = transform;
        _currentNode = _startNode;
        _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        _health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _health.OnDeath -= HandleDeath;
    }

    private void Update()
    {
        if (_currentNode == null) return;

        MoveToNode();
    }

    /// <summary>
    /// 現在のノードに向かって移動する
    /// </summary>
    private void MoveToNode()
    {
        Vector3 targetPos = _currentNode.transform.position;

        // ノード方向を計算
        Vector3 direction = (targetPos - _tr.position).normalized;

        // 移動
        _tr.position += direction * _moveSpeed * Time.deltaTime;

        // ノードに十分近づいたら次へ
        float distance = Vector3.Distance(_tr.position, targetPos);
        if (distance < 0.2f)
        {
            WaypointNode nextNode = _currentNode.GetNextNode();

            // 次がない＝ゴール
            if (nextNode == null)
            {
                OnReachedGoal?.Invoke();
                _currentNode = null;
            }
            else
            {
                _currentNode = nextNode;
            }
        }
    }
    /// <summary>
    /// HPが0になったとき
    /// </summary>
    private void HandleDeath()
    {
        OnEscortDead?.Invoke();
    }
}
