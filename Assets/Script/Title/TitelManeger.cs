using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitelUI : MonoBehaviour
{
    //ボタン設定
    [SerializeField] private GameObject firstSelectButton;

    //シーン
    [SerializeField] private string _mainGame;
    private void Start()
    {
        // タイトル画面が表示された瞬間に
        // コントローラーの選択先を設定する
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene(_mainGame);
    }
    public void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
}
