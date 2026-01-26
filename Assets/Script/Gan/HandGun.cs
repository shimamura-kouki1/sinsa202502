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
        Debug.Log($"発射");
        Vector3 directoin = (shootPoint.forward + Random.insideUnitSphere * _gunData.Diffusion).normalized;
        if(Physics.Raycast(shootPoint.position,directoin,out RaycastHit hit,_gunData.Range))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_gunData.Damage);//ダメージ処理を実行
            }
        }
    }
    public void FullyAout(Transform shootPoint)
    {
        Vector3 directoin = (shootPoint.forward + Random.insideUnitSphere * _gunData.Diffusion).normalized;
        if (Physics.Raycast(shootPoint.position, directoin, out RaycastHit hit, _gunData.Range))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_gunData.Damage);
                Debug.Log("Hit");
                //ダメージ処理
            }
        }
    }
}
