using UnityEngine;

public class AssaultRifle : IGun
{
    private GunData _gunData;
    public void Shoot(Transform shootPoint)
    {
        Vector3 directoin = shootPoint.forward + Random.insideUnitSphere * _gunData.Diffusion;
        if (Physics.Raycast(shootPoint.position, directoin, out RaycastHit hit, _gunData.Range))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_gunData.Damage);//ダメージ処理を実行
            }
        }
    }
}
