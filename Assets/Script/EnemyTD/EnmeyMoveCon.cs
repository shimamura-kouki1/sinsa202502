using UnityEngine;

/// <summary>
/// Enemyの移動制御
/// </summary>
public class EnemyMoveCon : MonoBehaviour
{
    [SerializeField] private float _movespeed = 2f;

    private WaypointNode _currentNode;
    private Health _health;

    private Transform _tr;


    private void OnEnable()
    {
        EnemyManager.Instance.Register(this);
        _health.OnDeath += Die;
    }
    private void Awake()
    {
        _tr = transform;
        _health = GetComponent<Health>();
}

    private void OnDisable()
    {
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

        if(Vector3.Distance(transform.position,traget) < 0.05f)
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
        }
        else
        {
            // 次が無い = ゴール
            // → ゴール判定はTriggerに任せる
            enabled = false;
        }
    }

    /// <summary>
    /// 死亡したときの処理
    /// </summary>
    private void Die()
    {
        Debug.Log("Enemy Dead");
        gameObject.SetActive(false); // MVP
    }
}
