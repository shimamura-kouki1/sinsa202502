using UnityEngine;

public class HndoGun : IGun
{
    private GunDate gunDate;

    public HndoGun(GunDate gunDate)
    {
        this.gunDate = gunDate;
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
