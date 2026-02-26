using UnityEngine;
/// <summary>
/// 発射用のSE再生クラス　
/// 現在使用していない
/// </summary>
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
        //イベント登録
        _weapon.OnFire += FireSE;
    }

    private void OnDisable()
    {
        //イベント登録解除
        _weapon.OnFire -= FireSE;
    }

    /// <summary>
    /// 弾発射SE再生
    /// </summary>
    private void FireSE()
    {
        if (_fireSE != null)
        {
            //再生
            _audioSource.PlayOneShot(_fireSE);
        }
    }
}
