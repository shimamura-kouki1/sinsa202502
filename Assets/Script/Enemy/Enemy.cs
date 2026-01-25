using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDate _enemyDate;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        _health.OnDeath += Die;
    }
    private void OnDisable()
    {
        _health.OnDeath -= Die;
    }
 
    private void Die()
    {
        transform.position =  _enemyDate._despoilAnchor.position;
        Debug.Log("Dei");

        //今後死亡してから、リスポーンのサイクル処理を作る
    }
}
