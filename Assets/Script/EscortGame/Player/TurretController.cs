using UnityEngine;
/// <summary>
/// 固定砲台
/// </summary>
public class TurretController : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    /// <summary>
    /// 射撃
    /// </summary>
    public void Fire(Vector3 origin, Vector3 direction)
    {
        if (!_weapon) return;
        _weapon.Fire(origin,direction);
    }
}
