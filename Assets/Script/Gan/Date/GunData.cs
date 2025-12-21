using UnityEngine;

[CreateAssetMenu(menuName ="Weapon/BulletDate")]
public class GunData : ScriptableObject
{
    [Header("Šî–{")]
    [SerializeField] private float _renge;
    [SerializeField] private float _damage;

    [Header("ŠgŽU")]
    [SerializeField] private float _diffusion;

    [Header("ŠÑ’Ê")]
    [SerializeField] private int _maxHitCount = 1;

    public float Range => _renge; //“Ç‚ÝŽæ‚èê—p
    public float Damage => _damage;
    public float Diffusion => _diffusion;
    public int MaxHitCount => _maxHitCount;
}
