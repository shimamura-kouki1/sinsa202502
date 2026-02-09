using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyMoveCon _enmeyPrefab;

    [SerializeField]
    private WaypointNode _startNode;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        EnemyMoveCon enemy =
            Instantiate(_enmeyPrefab);

        enemy.Initialize(_startNode);
    }
}
