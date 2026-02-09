using UnityEngine;

/// <summary>
/// 敵が到達したかを判定するゴール
/// </summary>
public class Goal : MonoBehaviour
{
    [SerializeField] private BaseHealth _baseHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        Debug.Log("enemyゴールに到達");

        _baseHealth.TakeDamage(1);
        other.gameObject.transform.position = Vector3.zero;//Vector3.zeroを将来的に待機場所のアンカーにしたい
    }
}
