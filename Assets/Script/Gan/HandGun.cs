using UnityEngine;

public class HandGun : IGun
{
    private GunData _gunData;//銃のステータスを持っている

    public HandGun(GunData gunDate)
    {
        this._gunData = gunDate;
    }

    public void Shoot(Transform shootPoint)
    {
        //レイキャスト・ダメージ計算・適応、射程
        Debug.Log($"ハンドガン発射：ダメージ {_gunData.Damage}");
        Vector3 directoin = shootPoint.forward + Random.insideUnitSphere * _gunData.Diffusion;
        if(Physics.Raycast(shootPoint.position,directoin,out RaycastHit hit,_gunData.Range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit");
                //ダメージ処理
            }
        }
    }
}
