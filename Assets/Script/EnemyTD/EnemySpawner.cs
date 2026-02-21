using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyMoveCon _enmeyPrefab;

    [SerializeField] private WaypointNode _startNode;
    [SerializeField] private GameManager _gameManager;

    public EnemyMoveCon Spawn()
    {
        EnemyMoveCon enemy =
            Instantiate(_enmeyPrefab, transform.position, Quaternion.identity);

        enemy.Initialize(_startNode);

        //_gameManager.RegisterEnemy(enemy);

        return enemy;
    }
}
