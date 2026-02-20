using System;
using UnityEngine;
/// <summary>
/// HPの管理、ダメージと死亡の通知をする
/// </summary>
public class Health : MonoBehaviour,IDamageable
{
    [SerializeField] private float _maxHp = 100f;

    private float _currentHp;//現在のHP

    public event Action OnDeath;//死んだ通知
    public event Action<float,float> OnHealthDamaged;//ダメージ量の通知　UI用

    private void Awake()
    {
        _currentHp = _maxHp;
        OnHealthDamaged?.Invoke(_currentHp, _maxHp);
    }

    public void TakeDamage(float damage)
    {
        if (_currentHp <= 0f) return;//0以下ならリターン

        _currentHp -= damage;
        _currentHp = Mathf.Max(_currentHp, 0f);//
        Debug.Log(_currentHp);

        OnHealthDamaged?.Invoke(_currentHp, 0f);//ダメージの通知
        

        if (_currentHp <= 0f)
        {
            OnDeath?.Invoke();//HPが0になったら通知
        }
    }
}
