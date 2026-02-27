using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面UI
/// </summary>
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

        Cursor.visible = true;//マウス表示

        Cursor.lockState = CursorLockMode.None;//カーソルの画面固定解除
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
