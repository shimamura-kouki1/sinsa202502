using System;
using UnityEngine;
/// <summary>
/// HPの管理、ダメージと死亡の通知をする
/// </summary>
public class Health : MonoBehaviour,IDamageable
{
    [SerializeField] private float _maxHp = 100f;

    private float _currentHp;

    public event Action OnDeath;//死んだ通知
    public event Action<float> OnDamage;//ダメージ量の通知　UI用

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(float damege)
    {
        if (_currentHp < 0f) return;//0以下ならリターン

        _currentHp -= damege;
        _currentHp = Math.Max(_currentHp, 0f);//

        OnDamage?.Invoke(damege);//ダメージの通知

        if(_currentHp <= 0f)
        {
            OnDeath?.Invoke();//HPが0になったら通知
        }

    }
}
