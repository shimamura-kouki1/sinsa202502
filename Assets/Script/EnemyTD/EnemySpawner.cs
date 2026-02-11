using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyMoveCon _enmeyPrefab;

    [SerializeField]
    private WaypointNode _startNode;

    public EnemyMoveCon Spawn()
    {
        EnemyMoveCon enemy =
            Instantiate(_enmeyPrefab,transform.position,Quaternion.identity);

        enemy.Initialize(_startNode);

        return enemy;
    }
}
