using System;
using UnityEngine;

/// <summary>
/// 敵のHP管理
/// </summary>
public class EnemyHealth : MonoBehaviour,IDamageable
{
    private float _currentHp;           //現在のHP
    private EnemyDataEscortGame _data;　//データ
    private ObjectPool _pool;           //オブジェクトプール
    /// <summary>
    /// 敵が死亡したときのイベント
    /// EnemyControllerが購読
    /// </summary>
    public event Action OnDeathEnemy;　 //死んだ通知

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="maxHp"></param>
    /// <param name="data"></param>
    /// <param name="pool"></param>
    public void Initialize(float maxHp,EnemyDataEscortGame data,ObjectPool pool)
    {
        _data = data;
        _pool = pool;
        _currentHp = maxHp;
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damege"></param>
    public void TakeDamage(float damege)
    {
        _currentHp -= damege;//HP減少

        if(_currentHp <= 0f)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    private void Die()
    {
        OnDeathEnemy?.Invoke();
        //オブジェクトプールに返還
        _pool.Despawn(gameObject, _data.prefab);
    }
}
