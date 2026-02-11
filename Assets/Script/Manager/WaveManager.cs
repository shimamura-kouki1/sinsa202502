using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    [SerializeField] private int _spawnCount = 5;
    [SerializeField] private float _interval = 1f;

    private int _spawned;
    private bool _isSpawning;
    private bool _cleared;

    void Start()
    {
        StartCoroutine(SpawnWave());        
    }

    private IEnumerator SpawnWave()
    {
        _isSpawning = true;

        while(_spawned < _spawnCount)
        {
            _spawner.Spawn();
            _spawned++;
            yield return new WaitForSeconds(_interval);
        }

        _isSpawning = false;
    }

    void Update()
    {
        if (_cleared) return;
        if (!_isSpawning && _spawnCount <= _spawned && EnemyManager.Instance.Enemies.Count == 0)
        {
            _cleared = true;
            Debug.Log("クリア");
        }
    }

}
