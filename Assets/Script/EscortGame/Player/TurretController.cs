using UnityEngine;
/// <summary>
/// 固定砲台
/// </summary>
public class TurretController : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;//砲台が使用する武器

    [Header("攻撃設定")]
    [SerializeField] private float _attackInterval = 0.5f; //発射間隔

    private float _coolTimer;   //クールタイム

    private void Update()
    {
        //クールタイムを減らす
        if (_coolTimer > 0f)
        {
            _coolTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 発射可能かどうか
    /// </summary>
    public bool CanFire()
    {
        //０以下ならture
        return _coolTimer <= 0f;

        //UIで使えるかも、発射可能表示とかで
    }

    /// <summary>
    /// 射撃
    /// </summary>
    public void Fire(Vector3 origin, Vector3 direction)
    {

        if (!CanFire()) return;
        if (!_weapon) return;

        //クールタイムリセット
        _coolTimer = _attackInterval;

        //武器に実行刺せる
        _weapon.Fire(origin,direction);

    }
}
