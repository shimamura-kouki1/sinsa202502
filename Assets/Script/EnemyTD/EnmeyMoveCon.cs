using System;
using UnityEngine;

/// <summary>
/// Enemyの移動制御
/// </summary>
public class EnemyMoveCon : MonoBehaviour
{
    [SerializeField] private float _movespeed = 2f;

    private WaypointNode _currentNode;
    private Health _health;
    private IDamageable _damageable;
    
    private Transform _tr;
    public event Action<EnemyMoveCon> OnReachedGoal;
    public event Action<EnemyMoveCon> OnDied;
    public int CurrentNodeIndex {  get; private set; }
    public IDamageable Damageable => _damageable;
    private void OnEnable()
    {
        Debug.Log("Enabled: " + gameObject.GetInstanceID());
    }
    private void Start()
    {
        EnemyManager.Instance.Register(this);
        _health.OnDeath += Die;//後々、登録するのはspawner側からにしたい
    }
    private void Awake()
    {
        _tr = transform;
        _health = GetComponent<Health>();
        _damageable = GetComponent<IDamageable>();
    }

    private void OnDisable()
    {
        Debug.Log("Disabled: " + gameObject.GetInstanceID());
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.Unregister(this);
        _health.OnDeath -= Die;
    }

    /// <summary>
    /// スポーン時に最初のノードを設定
    /// </summary>
    /// <param name="stratNode"></param>
    public void Initialize(WaypointNode stratNode)
    {
        _currentNode = stratNode;
        _tr.position = stratNode.transform.position;
    }


    void Update()
    {
        if (_currentNode == null) return;

        MoveToCurrentNode();
    }

    /// <summary>
    /// 現在のノードへ移動
    /// </summary>
    private void MoveToCurrentNode()
    {
        Vector3 traget = _currentNode.transform.position;

        //tragetまで_movespeed * Time.deltaTimeかけて移動
        _tr.position = Vector3.MoveTowards(_tr.position, traget, _movespeed * Time.deltaTime);

        if(Vector3.Distance(transform.position,traget) < 0.2f)
        {
            MoveToNextNode();
        }
    }

    /// <summary>
    /// 次のノードへ進む
    /// </summary>
    private void MoveToNextNode()
    {

        if (_currentNode.NextNodes())
        {
            _currentNode = _currentNode.GetNextNode();
            CurrentNodeIndex++;
        }
        else
        {
            ReachGoal();
        }
    }

    /// <summary>
    /// 死亡したときの処理
    /// </summary>
    private void Die()
    {
        OnDied?.Invoke(this);
        gameObject.SetActive(false); // MVP
    }
    private void ReachGoal()
    {
        OnReachedGoal?.Invoke(this);
        gameObject.SetActive(false);
    }
}
