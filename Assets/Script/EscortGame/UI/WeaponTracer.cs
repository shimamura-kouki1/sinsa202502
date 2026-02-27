using UnityEngine;

public class WeaponTracer : MonoBehaviour
{
    private Weapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        _weapon.OnFire += Tracer;
    }
    private void OnDisable()
    {
        _weapon.OnFire -= Tracer;
    }
    void Update()
    {
        
    }
    private void Tracer()
    {
        Debug.Log("トレーサー再生");
    }
}
