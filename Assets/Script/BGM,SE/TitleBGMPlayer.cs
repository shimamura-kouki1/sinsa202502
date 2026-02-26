using UnityEngine;

/// <summary>
/// タイトル用のBGM再生
/// </summary>
public class TitleBGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _titleBGM;//タイトル用のBGM
    void Start()
    {
        AudioManager.Instance.PlayBGM(_titleBGM);//BGM再生
    }
}
