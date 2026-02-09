using UnityEngine;

/// <summary>
/// 拠点のHP管理
/// </summary>
public class BaseHealth : MonoBehaviour
{
    [SerializeField]private int _maxHP = 10;
    [SerializeField] private int _currentHP;
    void Awake()
    {
        _currentHP = _maxHP;
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
        _currentHP = Mathf.Max(_currentHP, 0);
        Debug.Log($"のこりHP{_currentHP}");

        if(_currentHP <= 0)
        {
            OnGameOver();
        }
    }
    private void OnGameOver()
    {
        Debug.Log("ゲームオーバー");
    }
}
