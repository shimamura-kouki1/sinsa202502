using UnityEngine;

/// <summary>
/// ダメージを受けるものにつける
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(float damage);
}
