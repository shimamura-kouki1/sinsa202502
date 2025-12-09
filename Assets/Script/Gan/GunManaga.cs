using UnityEngine;

public class GunManaga : MonoBehaviour
{
    public IGun usingGun;

    public void PlayerShoot()
    {
        usingGun?.Shoot();
    }
}
