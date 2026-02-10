using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¶‘¶‚µ‚Ä‚¢‚éEnemyŠÇ—
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private readonly List<EnemyMoveCon> _enemies = new();

    public IReadOnlyList<EnemyMoveCon> Enemies => _enemies;

    private void Awake()
    {
        Instance = this;
    }

    public void Register(EnemyMoveCon enemy)
    {
        if (!_enemies.Contains(enemy))
            _enemies.Add(enemy);
    }

    public void Unregister(EnemyMoveCon enemy)
    {
        _enemies.Remove(enemy);
    }
}
