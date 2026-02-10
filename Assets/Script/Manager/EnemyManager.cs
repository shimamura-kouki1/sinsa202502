using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    private readonly List<EnemyMoveCon> _enemies = new();

    public IReadOnlyList<EnemyMoveCon> Enemys => _enemies;

    private void Awake()
    {
        instance = this;
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
