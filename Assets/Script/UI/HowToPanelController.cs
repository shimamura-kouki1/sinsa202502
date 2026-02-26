using UnityEngine;

public class HowToPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _panel; // 操作説明パネル

    // パネルを表示する
    public void ShowPanel()
    {
        _panel.SetActive(true);
    }

    // パネルを非表示にする
    public void HidePanel()
    {
        _panel.SetActive(false);
    }
}
