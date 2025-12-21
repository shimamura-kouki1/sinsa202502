using UnityEngine;

public class GunManager : MonoBehaviour
{
    public IGun UsingGun { get; private set; }

    [SerializeField] private GunData _handGunData;
    [SerializeField] private Transform _shootPoint;

    private void Start()
    {
        UsingGun = new HandGun(_handGunData);
    }
    public void PlayerShoot()
    {
        UsingGun?.Shoot(_shootPoint);
    }
}
