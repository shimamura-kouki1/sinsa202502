using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private LayerMask _hitLayer;

    [SerializeField] private GameObject _hitEffect;
    /// <summary>
    /// レイを飛ばす
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    public void Fire(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);

        if(Physics.Raycast(ray, out RaycastHit hit ,_range,_hitLayer))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                Debug.Log("hit");
                damageable.TakeDamage(_damage);
            }
            if (_hitEffect != null)
            {
                Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }
    }
}
