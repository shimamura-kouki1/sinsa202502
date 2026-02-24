using UnityEngine;

public class WweaponSE : MonoBehaviour
{
    private Weapon _weapon;
    private AudioSource _audioSource;

    [Header("発射SE")]
    [SerializeField] private AudioClip _fireSE;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _weapon.OnFire += FireSE;
    }

    private void OnDisable()
    {
        _weapon.OnFire -= FireSE;
    }

    /// <summary>
    /// 弾発射SE再生
    /// </summary>
    private void FireSE()
    {
        if (_fireSE != null)
        {
            _audioSource.PlayOneShot(_fireSE);
        }
    }
}
