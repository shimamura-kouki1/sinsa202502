using UnityEngine;
/// <summary>
/// SE.BGMの管理
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [Header("AudioSource")]
    [SerializeField] private AudioSource _seSource;
    [SerializeField] private AudioSource _bgmSource;

    [SerializeField,Range(0f,1f)] private float _seVolume;
    [SerializeField,Range(0f, 1f)] private float _bgmVolume;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySE(AudioClip clip)
    {
        if (clip == null) return;

        _seSource.PlayOneShot(clip,_seVolume);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        _bgmSource.clip = clip;
        _bgmSource.volume = _bgmVolume;
        _bgmSource.Play();
    }
}
