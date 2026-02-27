using System;
using UnityEngine;
/// <summary>
/// 護衛対象の移動制御
/// </summary>
public class EscortMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private WaypointNode _startNode;//移動開始の最初のウェイポイント

    private WaypointNode _currentNode;//今向かってるウェイポイント
    private Transform _tr;
    private Health _health;

    /// <summary>
    /// ゴール到達時に呼ばれるイベント
    /// GameManagerが購読
    /// </summary>
    public event Action OnReachedGoal;
    /// <summary>
    /// 死亡時に呼ばれるイベント
    /// GameManagerが購読
    /// </summary>
    public event Action OnEscortDead;

    private void Awake()
    {
        _tr = transform;

        //開始ノードを現在のノードに
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

        // ノード方向の単位ベクトルを計算
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
                _currentNode = nextNode;//次のノードに移動
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
