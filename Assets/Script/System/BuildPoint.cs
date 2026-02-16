using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public bool IsPossession { get; private set; }//占有されているかどうか

    public bool BuildCheck()
    {
        return !IsPossession;
    }

    public void Build(GameObject towerPrefab)
    {
        if (IsPossession) return;

        Instantiate(towerPrefab, transform.position, Quaternion.identity);
        IsPossession = true;
    }
}
