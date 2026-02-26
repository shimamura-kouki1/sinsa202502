using UnityEngine;
/// <summary>
/// SE.BGMの管理
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [Header("AudioSource")]
    [SerializeField] private AudioSource _seSource;//SE用
    [SerializeField] private AudioSource _bgmSource;//BGM用

    [SerializeField,Range(0f,1f)] private float _seVolume;//SE音量
    [SerializeField,Range(0f, 1f)] private float _bgmVolume;//BGM音量

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //シーン切り替えでも残す
        }
        else
        {
            Destroy(gameObject);//すでにAudioManagerがある場合は破壊
        }
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySE(AudioClip clip)
    {
        if (clip == null) return;

        //取得したSEを再生
        _seSource.PlayOneShot(clip,_seVolume);
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="loop"></param>
    public void PlayBGM(AudioClip clip,bool loop = true)
    {
        if (clip == null) return;

        //BGMのセット
        _bgmSource.clip = clip;

        //ボリュームのセット
        _bgmSource.volume = _bgmVolume;

        //ループ設定
        _bgmSource.loop = loop;

        //再生
        _bgmSource.Play();
    }
    /// <summary>
    /// BGM停止
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }
}
