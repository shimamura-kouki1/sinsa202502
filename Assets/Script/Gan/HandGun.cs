using UnityEngine;

public class HandGun : IGun
{
    private GunData _gunData;//銃のステータスを持っている

    public HandGun(GunData gunDate)
    {
        this._gunData = gunDate;
    }

    public void Shoot()
    {
        Debug.Log($"ハンドガン発射：ダメージ {_gunData.Damage}");
    }
}
