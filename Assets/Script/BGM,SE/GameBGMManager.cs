using UnityEngine;

/// <summary>
/// ゲームシーン用のBGM再生
/// </summary>
public class GameBGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip _gameBGM;//再生したいBGM

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM(_gameBGM);//再生BGM
        }
    }
}
