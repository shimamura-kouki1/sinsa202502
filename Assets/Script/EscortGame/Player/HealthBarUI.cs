using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// HPバーの表示管理
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health _targetHealth;  // 監視するHealth
    [SerializeField] private Image _hpFillImage;　　//HPバーの減少部分

    private void OnEnable()
    {
        _targetHealth.OnHealthDamaged += UpdateHPBar;
    }

    private void OnDisable()
    {
        _targetHealth.OnHealthDamaged -= UpdateHPBar;
    }

    /// <summary>
    /// HPバー更新
    /// </summary>
    private void UpdateHPBar(float current, float max)
    {
        //現在のHPを最大値で割って0～1に変換し、表示量を変更
        _hpFillImage.fillAmount = current / max;
    }
}
