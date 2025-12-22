using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GunManager _gunManager;


    [Header("Gan Setting")]
    [SerializeField]private float _renge = 100f;

    public void Fire()
    {
        _gunManager.PlayerShoot(_shootPoint);
    }
}
