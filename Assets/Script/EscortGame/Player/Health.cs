using System;
using UnityEngine;
/// <summary>
/// HPの管理、ダメージと死亡の通知をする
/// </summary>
public class Health : MonoBehaviour,IDamageable
{
    [SerializeField] private float _maxHp = 100f;

    private float _currentHp;//現在のHP

    /// <summary>
    /// 死亡時に呼ばれる
    /// EnemySpwaner、EscortMover,UIManagerが購読
    /// </summary>
    public event Action OnDeath;//死んだ通知
    /// <summary>
    /// ダメージを受けたときに呼ばれる
    /// （引数１：現在のHP,引数2：最大HP ）
    /// UIManager、HealthBarが購読
    /// </summary>
    public event Action<float,float> OnHealthDamaged;//ダメージ量の通知　UI用

    private void Awake()
    {
        _currentHp = _maxHp;
        OnHealthDamaged?.Invoke(_currentHp, _maxHp);
    }

    /// <summary>
    /// ダメージを受け取る
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        if (_currentHp <= 0f) return;//0以下ならリターン

        _currentHp -= damage;
        
        //HPが0未満にならないよう調整
        _currentHp = Mathf.Max(_currentHp, 0f);

        OnHealthDamaged?.Invoke(_currentHp,_maxHp);//ダメージの通知
        
        //HPが0以下で通知
        if (_currentHp <= 0f)
        {
            OnDeath?.Invoke();//HPが0になったら通知
        }
    }
}
